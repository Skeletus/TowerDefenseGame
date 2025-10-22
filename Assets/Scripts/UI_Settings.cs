using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
    private CameraController cameraController;

    [Header("Keyboard Sensitivity")]
    [SerializeField] private Slider keyboardSensitivity;
    [SerializeField] private string keyboardSenseParameter = "keyboardSense";

    [SerializeField] private float minKeyboardSens = 60;
    [SerializeField] protected float maxKeyboardSens = 240;

    private void Awake()
    {
        cameraController = FindFirstObjectByType<CameraController>();
    }

    public void KeyboardSensitivity(float value)
    {
        float mouseSensetivity = Mathf.Lerp(minKeyboardSens, maxKeyboardSens, value);

        cameraController.AdjustKeyboardSensitivity(mouseSensetivity);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(keyboardSenseParameter, keyboardSensitivity.value);
    }

    private void OnEnable()
    {
        keyboardSensitivity.value = PlayerPrefs.GetFloat(keyboardSenseParameter, .5f);
    }
}
