using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxHP;
    [SerializeField] private int currentHP;

    private UI_InGame inGameUI;

    private void Awake()
    {
        inGameUI = FindFirstObjectByType<UI_InGame>();
    }

    private void Start()
    {
        currentHP = maxHP;
        inGameUI.UpdateHealthPointsUI(currentHP, maxHP);
    }

    public void UpdateHP(int value)
    {
        currentHP += value;
        inGameUI.UpdateHealthPointsUI(currentHP, maxHP);
    }
}
