using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public static Currency Instance { get; private set; }
    [field: SerializeField] public int Money { get; private set; }

    public event Action CurrencyChanged;

    private void Awake()
    {
        Instance = this;


    }

    public void AddMoney(int amount)
    {
        Money += amount;
        CurrencyChanged?.Invoke();
    }

    public void RemoveMoney(int amount)
    {
        if (!CanAfford(amount)) return;
        Money -= amount;
        CurrencyChanged?.Invoke();
    }

    public bool CanAfford(int amount)
    {
        return Money - amount >= 0;
    }

}
