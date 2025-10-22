using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElements;

    private UI_Settings ui_settings;
    private UI_MainMenu ui_mainMenu;

    private void Awake()
    {
        ui_settings = GetComponentInChildren<UI_Settings>(true);
        ui_mainMenu = GetComponentInChildren<UI_MainMenu>(true);

        SwitchTo(ui_settings.gameObject);
        SwitchTo(ui_mainMenu.gameObject);
    }

    public void SwitchTo(GameObject uiToEnable)
    {
        foreach(GameObject uiElement in uiElements)
        {
            uiElement.SetActive(false);
        }

        uiToEnable.SetActive(true);

    }

    public void QuitButton()
    {
        if(EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
