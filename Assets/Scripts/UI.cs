using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElements;

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
