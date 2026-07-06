using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BuildButtonsHolder : MonoBehaviour
{
    [SerializeField] private float yPositionOffset;
    [SerializeField] private float openAnimationDuration = .1f;

    private bool isBuildMenuActive;
    private UI_Animator uiAnim;

    private UI_BuildButtonOnHoverEffect[] buildButtonsEffects;
    private UI_BuildButton[] buildButtons;

    private void Awake()
    {
        uiAnim = GetComponentInParent<UI_Animator>();
        buildButtonsEffects = GetComponentsInChildren<UI_BuildButtonOnHoverEffect>();
        buildButtons = GetComponentsInChildren<UI_BuildButton>();
    }

    public UI_BuildButton[] GetBuildButtons() => buildButtons;

    public void ShowBuildButtons(bool showButton)
    {
        isBuildMenuActive = showButton;

        float yOffset = isBuildMenuActive ? yPositionOffset : -yPositionOffset;
        float methodDelay = isBuildMenuActive ? openAnimationDuration : 0;

        uiAnim.ChangePosition(transform, new Vector3(0, yOffset), openAnimationDuration);

        Invoke(nameof(ToggleButtonMovement), methodDelay);
    }

    private void ToggleButtonMovement()
    {
        foreach (var button in buildButtonsEffects)
        {
            button.ToggleMovement(isBuildMenuActive);
        }
    }
}
