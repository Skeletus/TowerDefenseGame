using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private UI ui;
    public BuildSlot selectedBuildSlot;

    private void Awake()
    {
        ui = FindFirstObjectByType<UI>();
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

    public void CancelBuildAction()
    {
        if (selectedBuildSlot == null)
        {
            return;
        }
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

        ui.buildButtons.ShowBuildButtons(true);
    }

    private void DisableBuildMenu()
    {
        ui.buildButtons.ShowBuildButtons(false);
    }
    
    public BuildSlot GetSelectedSlot() => selectedBuildSlot;
}
