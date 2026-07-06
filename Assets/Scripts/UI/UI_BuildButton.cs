using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_BuildButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private BuildManager buildManager;
    private CameraEffects cameraEffects;
    private GameManager gameManager;
    private UI ui;
    private UI_BuildButtonsHolder buildButonsHolder;
    private UI_BuildButtonOnHoverEffect onHoverEffect;

    [SerializeField] private string towerName;
    [SerializeField] private int towerPrice = 50;

    [SerializeField] private GameObject towerToBuild;
    [SerializeField] private float towerCenterY = .5f;

    [Header("Text UI Component")]
    [SerializeField] private TextMeshProUGUI towerNameText;
    [SerializeField] private TextMeshProUGUI towerPriceText;

    private TowerPreview towerPreview;
    public bool buttonUnlocked { get; private set; }


    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        onHoverEffect = GetComponent<UI_BuildButtonOnHoverEffect>();
        buildButonsHolder = GetComponentInParent<UI_BuildButtonsHolder>();
        buildManager = FindFirstObjectByType<BuildManager>();
        cameraEffects = FindFirstObjectByType<CameraEffects>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void Start()
    {
        CreateTowerPreview();
    }

    private void CreateTowerPreview()
    {
        GameObject newPreview = Instantiate(towerToBuild, Vector3.zero, Quaternion.identity);

        towerPreview = newPreview.AddComponent<TowerPreview>();
        towerPreview.gameObject.SetActive(false);
    }

    public void SelectButton(bool select)
    {
        BuildSlot slotToUse = buildManager.GetSelectedSlot();

        if (slotToUse == null)
        {
            return;
        }

        Vector3 previewPosition = slotToUse.GetBuildPosition(1);

        towerPreview.gameObject.SetActive(select);
        towerPreview.ShowPreview(select, previewPosition);
        onHoverEffect.ShowcaseButton(select);
        buildButonsHolder.SetLastSelected(this);
    }

    public void UnlockTowerIfNeeded(string towerNameToCheck, bool unlockStatus)
    {
        if (towerNameToCheck != towerName)
        {
            return;
        }

        buttonUnlocked = unlockStatus;
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

        if (ui.buildButtonsUI.GetLastSelectedButton() == null)
        {
            return;
        }

        BuildSlot slotToUse = buildManager.GetSelectedSlot();
        buildManager.CancelBuildAction();

        slotToUse.SnapToDefaultPositionInmidiatly();
        slotToUse.SetSlotAvaliableTo(false);

        ui.buildButtonsUI.SetLastSelected(null);

        cameraEffects.ScreenShake(.15f, .02f); 

        GameObject newTower = Instantiate(towerToBuild, slotToUse.GetBuildPosition(towerCenterY), Quaternion.identity);
    }

    private void OnValidate()
    {
        towerNameText.text = towerName;
        towerPriceText.text = towerPrice + "";
        gameObject.name = "BuildButton_UI - " + towerName;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buildManager.MouseOverUI(true);

        foreach(var button in buildButonsHolder.GetBuildButtons())
        {
            if(button.gameObject.activeSelf)
            {
                button.SelectButton(false);
            }
        }

        SelectButton(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buildManager.MouseOverUI(false);
    }
}
