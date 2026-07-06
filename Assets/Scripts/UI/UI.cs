using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Image fadeImageUI;
    [SerializeField] private GameObject[] uiElements;

    private UI_Animator uiAnim;
    private UI_Settings ui_settings;
    private UI_MainMenu ui_mainMenu;
    private UI_InGame ui_inGame;

    public UI_BuildButtonsHolder buildButtonsUI { get; private set; }

    private void Awake()
    {
        buildButtonsUI = GetComponentInChildren<UI_BuildButtonsHolder>(true);
        ui_settings = GetComponentInChildren<UI_Settings>(true);
        ui_mainMenu = GetComponentInChildren<UI_MainMenu>(true);
        ui_inGame = GetComponentInChildren<UI_InGame>(true);
        uiAnim = GetComponent<UI_Animator>();

        //ActivateFadeEffect(true);

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

    public void ActivateFadeEffect(bool fadeIn)
    {
        if(fadeIn)
        {
            uiAnim.ChangeColor(fadeImageUI, 0, 2);
        }
        else
        {
            uiAnim.ChangeColor(fadeImageUI, 1, 2);
        }
    }
}
