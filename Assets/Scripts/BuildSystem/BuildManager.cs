using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private UI ui;
    public BuildSlot selectedBuildSlot;

    public WaveManager waveManager;
    public GridBuilder currentGrid;

    [Header("Build Materials")]
    [SerializeField] private Material attackRadiusMaterial;
    [SerializeField] private Material buildPreviewMaterial;

    private void Awake()
    {
        ui = FindFirstObjectByType<UI>();

        MakeBuildSlotNotAvailableIfNeeded(waveManager, currentGrid);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CancelBuildAction();
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                bool clickedNotOnBuildSlot = hit.collider.GetComponent<BuildSlot>() == null;

                if(clickedNotOnBuildSlot)
                {
                    CancelBuildAction();
                }
            }
        }
    }

    public void MakeBuildSlotNotAvailableIfNeeded(WaveManager wavemanager, GridBuilder currentGrid)
    {
        foreach(var wave in wavemanager.GetLevelWaves())
        {
            if(wave.nextGrid == null)
            {
                continue;                               
            }
            List<GameObject> grid = currentGrid.GetTileSetup();
            List<GameObject> nextWaveGrid = wave.nextGrid.GetTileSetup();

            for (int i = 0; i < grid.Count; i++)
            {
                TileSlot currentTile = grid[i].GetComponent<TileSlot>();
                TileSlot nextTile = nextWaveGrid[i].GetComponent<TileSlot>();

                bool tileNotTheSame = currentTile.GetMesh() != nextTile.GetMesh() ||
                              currentTile.GetMaterial() != nextTile.GetMaterial() ||
                              currentTile.GetAllChildren().Count != nextTile.GetAllChildren().Count;

                if (tileNotTheSame == false)
                {
                    continue;                     
                }

                BuildSlot buildslot = grid[i].GetComponent<BuildSlot>();

                if (buildslot != null)
                {
                    buildslot.SetSlotAvaliableTo(false);
                }
            }
        }
    }

    public void CancelBuildAction()
    {
        if (selectedBuildSlot == null)
        {
            return;
        }
        ui.buildButtonsUI.GetLastSelectedButton().SelectButton(false);
        selectedBuildSlot.UnselectTile();
        selectedBuildSlot = null;
        DisableBuildMenu();
    }

    public void SelectBuildSlot(BuildSlot newSlot)
    {
        if (selectedBuildSlot != null)
        {
            selectedBuildSlot.UnselectTile();
        }
        selectedBuildSlot = newSlot;
    }

    public void EnableBuildMenu()
    {
        if (selectedBuildSlot != null)
        {
            return;
        }

        ui.buildButtonsUI.ShowBuildButtons(true);
    }

    private void DisableBuildMenu()
    {
        ui.buildButtonsUI.ShowBuildButtons(false);
    }
    
    public BuildSlot GetSelectedSlot() => selectedBuildSlot;
    public Material GetAttackRadiusMaterial() => attackRadiusMaterial;
    public Material GetBuildPreviewMaterial() => buildPreviewMaterial;
}
