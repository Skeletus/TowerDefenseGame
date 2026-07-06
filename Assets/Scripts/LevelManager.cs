using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerUnlockData
{
    public string towerName;
    public bool unlocked;

    public TowerUnlockData(string newTowerName, bool newUnlockedStatus)
    {
        towerName = newTowerName;
        unlocked = newUnlockedStatus;
    }
}

public class LevelManager : MonoBehaviour
{
    public List<TowerUnlockData> towerUnlocks;

    private void Start()
    {
        UnlockAvailableTowers();
    }

    [ContextMenu("Initialize Tower Data")]
    private void InitializeTowerData()
    {
        towerUnlocks.Clear();

        towerUnlocks.Add(new TowerUnlockData("Crossbow", false));
        towerUnlocks.Add(new TowerUnlockData("Cannon", false));
        towerUnlocks.Add(new TowerUnlockData("Rapid fire gun", false));
        towerUnlocks.Add(new TowerUnlockData("Hammer", false));
        towerUnlocks.Add(new TowerUnlockData("Spider Nest", false));
        towerUnlocks.Add(new TowerUnlockData("AA Harpon", false));
        towerUnlocks.Add(new TowerUnlockData("Fan", false));
    }

    private void UnlockAvailableTowers()
    {
        UI ui = FindFirstObjectByType<UI>();

        foreach(var unlockData in towerUnlocks)
        {
            foreach (var buildButton in ui.buildButtonsUI.GetBuildButtons())
            {
                buildButton.UnlockTowerIfNeeded(unlockData.towerName, unlockData.unlocked);
            }
        }

        ui.buildButtonsUI.UpdateUnlockedButtons();
    }
}
