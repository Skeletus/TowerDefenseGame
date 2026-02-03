using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BuildButtons : MonoBehaviour
{
    [SerializeField] private float yPositionOffset;

    private bool isBuildMenuActive;
    private UI_Animator uiAnim;

    private UI_BuildButtonOnHoverEffect[] buildButtons;

    private void Awake()
    {
        uiAnim = GetComponentInParent<UI_Animator>();
        buildButtons = GetComponentsInChildren<UI_BuildButtonOnHoverEffect>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            ShowBuildButtons();
    }

    public void ShowBuildButtons()
    {
        isBuildMenuActive = !isBuildMenuActive;

        float yOffset = isBuildMenuActive ? yPositionOffset : -yPositionOffset;
        Vector3 offset = new Vector3(0, yOffset);

        uiAnim.ChangePosition(transform, offset);
        ToggleButtonMovement();
    }

    private void ToggleButtonMovement()
    {
        foreach (var button in buildButtons)
        {
            button.ToggleMovement(isBuildMenuActive);
        }
    }
}
