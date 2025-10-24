using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
    private CameraController cameraController;

    [Header("Keyboard Sensitivity")]
    [SerializeField] private Slider keyboardSenseSlider;
    [SerializeField] private string keyboardSenseParameter = "keyboardSense";

    [SerializeField] private float minKeyboardSens = 60;
    [SerializeField] protected float maxKeyboardSens = 240;

    [Header("Mouse Sensetivity")]
    [SerializeField] private Slider mouseSenseSlider;
    [SerializeField] private string mouseSenseParameter = "mouseSense";

    [SerializeField] private float minMouseSense = 1;
    [SerializeField] private float maxMouseSense = 10;

    private void Awake()
    {
        cameraController = FindFirstObjectByType<CameraController>();
    }

    public void KeyboardSensitivity(float value)
    {
        float mouseSensetivity = Mathf.Lerp(minKeyboardSens, maxKeyboardSens, value);

        cameraController.AdjustKeyboardSensitivity(mouseSensetivity);
    }

    public void MouseSensetivity(float value)
    {
        float mouseSensetivity = Mathf.Lerp(minMouseSense, maxMouseSense, value);

        cameraController.AdjustMouseSensitivity(mouseSensetivity);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(keyboardSenseParameter, keyboardSenseSlider.value);
        PlayerPrefs.SetFloat(mouseSenseParameter, mouseSenseSlider.value);
    }

    private void OnEnable()
    {
        keyboardSenseSlider.value = PlayerPrefs.GetFloat(keyboardSenseParameter, .5f);
        mouseSenseSlider.value = PlayerPrefs.GetFloat(mouseSenseParameter, .6f);
    }
}
