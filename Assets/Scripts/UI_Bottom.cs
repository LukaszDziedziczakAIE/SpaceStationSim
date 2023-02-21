using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bottom : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI inGameTime;
    [SerializeField] TextMeshProUGUI inGameDay;
    [SerializeField] TextMeshProUGUI currency;
    [SerializeField] Button buildButton;

    private void Start()
    {
        buildButton.onClick.AddListener(OnBuildButtonPress);

        GameTime.Instance.HourTick += GameTime_HourTick;
        Currency.Instance.CurrencyChanged += Currency_CurrencyChanged;
        
        UpdateTimeDate();
        UpdateCurrency();
    }

    

    private void Update()
    {
        timer.text = Time.time.ToString("f0");
    }

    private void GameTime_HourTick()
    {
        UpdateTimeDate();
    }

    private void Currency_CurrencyChanged()
    {
        UpdateCurrency();
    }

    private void UpdateCurrency()
    {
        currency.text = "$" + Currency.Instance.Money;
    }

    private void UpdateTimeDate()
    {
        inGameTime.text = GameTime.Instance.Hour.ToString() + ":00";
        inGameDay.text = "Day " + GameTime.Instance.Day.ToString();
    }
    
    private void OnBuildButtonPress()
    {
        UI_HUD.Instance.BuildMenuUI.Show();
    }

}
