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
    [SerializeField] TextMeshProUGUI visitor;
    [SerializeField] TextMeshProUGUI stationRating;
    [SerializeField] Button buildButton;

    private void Start()
    {
        buildButton.onClick.AddListener(OnBuildButtonPress);

        GameTime.Instance.HourTick += GameTime_HourTick;
        Currency.Instance.CurrencyChanged += Currency_CurrencyChanged;
        FindObjectOfType<VisitorSpawn>().VisitorSpawned += VisitorSpawn_VisitorSpawned;
        StationManager.Instance.StationRatingChanged += StationManager_StationRatingChanged;


        UpdateTimeDate();
        UpdateCurrency();
        UpdateVisitorCount();
        UpdateStationRating();
    }

    private void StationManager_StationRatingChanged()
    {
        UpdateStationRating();
    }

    private void VisitorSpawn_VisitorSpawned()
    {
        UpdateVisitorCount();
    }

    private void Update()
    {
        timer.text = (Time.time / 60f).ToString("F0") + ":" + (Time.time % 60f).ToString("F0");
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

    public void UpdateVisitorCount()
    {
        visitor.text = "Visitors: " + VisitorManager.Instance.NumberOfVisitors;
    }

    public void UpdateStationRating()
    {
        stationRating.text = "Station Rating: " + StationManager.Instance.StationRating;
    }

}
