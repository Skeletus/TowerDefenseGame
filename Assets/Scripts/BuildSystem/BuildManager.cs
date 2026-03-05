using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public BuildSlot selectedBuildSlot;

    public void SelectBuildSlot(BuildSlot newSlot)
    {
        if (selectedBuildSlot != null)
        {
            selectedBuildSlot.UnselectTile();
        }
        selectedBuildSlot = newSlot;
    }
}
