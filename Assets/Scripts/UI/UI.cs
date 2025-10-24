using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElements;

    private UI_Settings ui_settings;
    private UI_MainMenu ui_mainMenu;
    private UI_InGame ui_inGame;

    private void Awake()
    {
        ui_settings = GetComponentInChildren<UI_Settings>(true);
        ui_mainMenu = GetComponentInChildren<UI_MainMenu>(true);
        ui_inGame = GetComponentInChildren<UI_InGame>(true);

        SwitchTo(ui_settings.gameObject);
        //SwitchTo(ui_mainMenu.gameObject);
        SwitchTo(ui_inGame.gameObject);
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
