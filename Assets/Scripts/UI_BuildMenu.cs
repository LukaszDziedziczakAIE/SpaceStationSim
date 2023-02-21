using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BuildMenu : MonoBehaviour
{
    [SerializeField] Button CancelButton;
    [SerializeField] Button buildRefuelStationButton;

    private void Start()
    {
        CancelButton.onClick.AddListener(OnCancelButtonPress);
        buildRefuelStationButton.onClick.AddListener(OnRefuelStationButtonPress);

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
        int refuelStationCost = FindObjectOfType<RefuelStation>().CostToBuild;
        buildRefuelStationButton.interactable = Currency.Instance.CanAfford(refuelStationCost) && BuildManager.Instance.CanBuildRefuelStation;
    }

    private void OnCancelButtonPress()
    {
        Hide();
    }

    private void OnRefuelStationButtonPress()
    {
        int refuelStationCost = FindObjectOfType<RefuelStation>().CostToBuild;
        Currency.Instance.RemoveMoney(refuelStationCost);
        BuildManager.Instance.BuildRefuelStation();
    }
}
