using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int currency;
    [SerializeField] private int maxHP;
    [SerializeField] private int currentHP;

    private UI_InGame inGameUI;

    private void Awake()
    {
        inGameUI = FindFirstObjectByType<UI_InGame>(FindObjectsInactive.Include);
    }

    private void Start()
    {
        currentHP = maxHP;
        inGameUI.UpdateHealthPointsUI(currentHP, maxHP);
        inGameUI.UpdateCurrencyUI(currency);
    }

    public void UpdateHP(int value)
    {
        currentHP += value;
        inGameUI.UpdateHealthPointsUI(currentHP, maxHP);
    }

    public void UpdateCurrency(int value)
    {
        currency += value;
        inGameUI.UpdateCurrencyUI(currency);
    }

    public bool HasEnoughCurrency(int price)
    {
        if ( price < currency)
        {
            currency -= price;
            inGameUI.UpdateCurrencyUI(currency);
            return true;
        }
        return false;
    }
}
