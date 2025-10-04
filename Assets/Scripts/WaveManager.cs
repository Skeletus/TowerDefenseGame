using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class WaveDetails
{
    public GridBuilder nextGrid;
    public EnemyPortal[] newPortals;
    public int basicEnemy;
    public int fastEnemy;
}

public class WaveManager : MonoBehaviour
{
    public bool waveCompleted;
    public float timeBetweenWaves = 10;
    public float waveTimer;

    private float checkInterval = .5f;
    private float nextCheckTime = 0;

    private List<EnemyPortal> enemyPortals;
    [Header("Wave Configuration")]
    [SerializeField] private WaveDetails[] LevelWaves;
    private int waveIndex = 0;

    [Header("Enemies Prefab")]
    [SerializeField] private GameObject basicEnemyPrefab;
    [SerializeField] private GameObject fastEnemyPrefab;

    [Header("Grid layout")]
    [SerializeField] private GridBuilder currentGrid;

    private void Awake()
    {
        enemyPortals = new List<EnemyPortal>(FindObjectsOfType<EnemyPortal>());
    }

    private void Start()
    {
        SetUpNextWave();
    }

    private void Update()
    {
        HandleWaveCompletition();

        HandleWaveTiming();
    }

    private void HandleWaveCompletition()
    {
        if(ReadyToCheck() == false)
        {
            return;
        }

        if (waveCompleted == false && AllEnemiesDefeated())
        {
            CheckForNewLevelLayout();

            waveCompleted = true;
            waveTimer = timeBetweenWaves;
        }
    }

    private void HandleWaveTiming()
    {
        if (waveCompleted)
        {
            waveTimer -= Time.deltaTime;

            if (waveTimer <= 0)
            {
                SetUpNextWave();
            }
        }
    }

    public void ForceNextWave()
    {
        if(AllEnemiesDefeated() == false)
        {
            Debug.LogWarning("Cant force while there are enemies in the game");
            return;
        }
        SetUpNextWave();
    }

    [ContextMenu("Setup next wave")]
    private void SetUpNextWave()
    {
        List<GameObject> newEnemies = NewEnemyWave();
        int portalIndex = 0;

        if (newEnemies == null)
        {
            return;
        }

        for(int i=0; i < newEnemies.Count; i++)
        {
            GameObject enemyToAdd = newEnemies[i];
            EnemyPortal portalToReceiveEnemy = enemyPortals[portalIndex];

            portalToReceiveEnemy.AddEnemy(enemyToAdd);

            portalIndex++;

            if (portalIndex >= enemyPortals.Count)
            {
                portalIndex = 0;
            }
        }

        waveCompleted = false;
    }

    private List<GameObject> NewEnemyWave()
    {
        if(waveIndex >= LevelWaves.Length)
        {
            Debug.LogWarning("you have no more waves");
            return null;
        }

        List<GameObject> newEnemyList = new List<GameObject>();

        for(int i =0; i < LevelWaves[waveIndex].basicEnemy; i++)
        {
            newEnemyList.Add(basicEnemyPrefab);
        }

        for (int i = 0; i < LevelWaves[waveIndex].fastEnemy; i++)
        {
            newEnemyList.Add(fastEnemyPrefab);
        }

        waveIndex++;

        return newEnemyList;
    }

    private void CheckForNewLevelLayout()
    {
        if( waveIndex >= LevelWaves.Length)
        {
            return;
        }

        WaveDetails nextWave = LevelWaves[waveIndex];

        if(nextWave.nextGrid != null)
        {
            Debug.Log("level should be updated");
            UpdateLevelTiles(nextWave.nextGrid);
            EnableNewPortals(nextWave.newPortals);
        }

        currentGrid.UpdateNavMesh();
    }

    private void EnableNewPortals(EnemyPortal[] newPortals)
    {
        foreach (EnemyPortal portal in newPortals)
        {
            portal.gameObject.SetActive(true);
            enemyPortals.Add(portal);
        }
    }

    private void UpdateLevelTiles(GridBuilder nextGrid)
    {
        List<GameObject> grid = currentGrid.GetTileSetup();
        List<GameObject> newGrid = nextGrid.GetTileSetup();

        for (int i = 0; i < grid.Count; i++)
        {
            TileSlot currentTile = grid[i].GetComponent<TileSlot>();
            TileSlot newTile = newGrid[i].GetComponent<TileSlot>();

            bool shouldBeUpdated = currentTile.GetMesh() != newTile.GetMesh() ||
                                  currentTile.GetMaterial() != newTile.GetMaterial() ||
                                  currentTile.GetAllChildren().Count != newTile.GetAllChildren().Count ||
                                  currentTile.transform.rotation != newTile.transform.rotation;

            if (shouldBeUpdated)
            {
                currentTile.gameObject.SetActive(false);
                newTile.gameObject.SetActive(true);
                newTile.transform.parent = currentGrid.transform;

                grid[i] = newTile.gameObject;
                Destroy(currentTile.gameObject);
            }
        }
    }

    private bool AllEnemiesDefeated()
    {
        foreach(EnemyPortal portal in enemyPortals)
        {
            if(portal.GetActiveEnemies().Count > 0)
            {
                return false;
            }
        }

        return true;
    }

    private bool ReadyToCheck()
    {
        if(Time.time >= nextCheckTime)
        {
            nextCheckTime = Time.time + checkInterval;
            return true;
        }
        return false;
    }
}
