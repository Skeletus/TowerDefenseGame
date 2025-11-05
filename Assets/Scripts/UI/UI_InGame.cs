using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyPointsText;
    [SerializeField] private TextMeshProUGUI healthPointsText;

    public void UpdateHealthPointsUI(int value, int maxValue)
    {
        int newValue = maxValue - value;

        healthPointsText.text = "Threat: " + newValue + "/" + maxValue;
    }

    public void UpdateCurrencyUI(int value)
    {

        currencyPointsText.text = "Resources: " + value;
    }
}
