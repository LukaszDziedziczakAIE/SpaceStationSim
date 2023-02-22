using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BuildMenu : MonoBehaviour
{
    [SerializeField] Button CancelButton;
    [SerializeField] Button buildRefuelStationButton;
    [SerializeField] Button buildCafeteriaButton;

    private void Start()
    {
        CancelButton.onClick.AddListener(OnCancelButtonPress);
        buildRefuelStationButton.onClick.AddListener(OnRefuelStationButtonPress);
        buildCafeteriaButton.onClick.AddListener(OnBuildCafeteriaButtonPress);

        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ButtonVisibility();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ButtonVisibility()
    {
        if (StationManager.Instance.StationRating >= 10) buildCafeteriaButton.gameObject.SetActive(true);
        else buildCafeteriaButton.gameObject.SetActive(false);

        buildRefuelStationButton.interactable = Currency.Instance.CanAfford(BuildManager.Instance.RefuelStationCost) && BuildManager.Instance.CanBuildRefuelStation;

        buildCafeteriaButton.interactable = Currency.Instance.CanAfford(BuildManager.Instance.CafeteriaCost) &&
            BuildManager.Instance.CanBuildCafeteria;
    }

    private void OnCancelButtonPress()
    {
        Hide();
    }

    private void OnRefuelStationButtonPress()
    {
        Currency.Instance.RemoveMoney(BuildManager.Instance.RefuelStationCost);
        BuildManager.Instance.BuildRefuelStation();
        Hide();
    }

    private void OnBuildCafeteriaButtonPress()
    {
        Currency.Instance.RemoveMoney(BuildManager.Instance.CafeteriaCost);
        BuildManager.Instance.BuildCafeteria();
        Hide();
    }
}
