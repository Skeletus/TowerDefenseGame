using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_BuildButton : MonoBehaviour
{
    private BuildManager buildManager;
    private CameraEffects cameraEffects;
    private GameManager gameManager;
    private UI ui;

    [SerializeField] private string towerName;
    [SerializeField] private int towerPrice = 50;

    [SerializeField] private GameObject towerToBuild;
    [SerializeField] private float towerCenterY = .5f;

    [Header("Text UI Component")]
    [SerializeField] private TextMeshProUGUI towerNameText;
    [SerializeField] private TextMeshProUGUI towerPriceText;


    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        buildManager = FindFirstObjectByType<BuildManager>();
        cameraEffects = FindFirstObjectByType<CameraEffects>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void UnlockTowerIfNeeded(string towerNameToCheck, bool unlockStatus)
    {
        if (towerNameToCheck != towerName)
        {
            return;
        }

        gameObject.SetActive(unlockStatus);
    }

    public void BuildTower()
    {
        if(gameManager.HasEnoughCurrency(towerPrice) == false)
        {
            ui.ui_inGame.ShakeCurrencyUI();
            return;
        }

        if (towerToBuild == null)
        {
            Debug.LogWarning("you did not assign tower to this button");
            return;
        }

        BuildSlot slotToUse = buildManager.GetSelectedSlot();
        buildManager.CancelBuildAction();

        slotToUse.SnapToDefaultPositionInmidiatly();
        slotToUse.SetSlotAvaliableTo(false);

        cameraEffects.ScreenShake(.15f, .02f); 

        GameObject newTower = Instantiate(towerToBuild, slotToUse.GetBuildPosition(towerCenterY), Quaternion.identity);
    }

    private void OnValidate()
    {
        towerNameText.text = towerName;
        towerPriceText.text = towerPrice + "";
        gameObject.name = "BuildButton_UI - " + towerName;
    }
}
