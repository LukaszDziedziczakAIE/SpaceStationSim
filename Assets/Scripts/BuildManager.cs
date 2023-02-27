using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    [SerializeField] List<RefuelStation> refuelStations;
    [SerializeField] Cafeteria cafeteria;

    [field: SerializeField, Header("Cost")] public int RefuelStationCost { get; private set; }
    [field: SerializeField] public int CafeteriaCost { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public bool CanBuildRefuelStation
    {
        get
        {
            foreach (RefuelStation refuelStation in refuelStations)
            {
                if (!refuelStation.gameObject.activeSelf) return true;
            }
            return false;
        }
    }

    public void BuildRefuelStation()
    {
        if (!CanBuildRefuelStation) return;

        foreach (RefuelStation refuelStation in refuelStations)
        {
            if (!refuelStation.gameObject.activeSelf)
            {
                refuelStation.gameObject.SetActive(true);
                
                if (StationManager.Instance.StationRating < 10)
                {
                    StationManager.Instance.IncreaseStationRating();
                }

                return;
            }
        }
    }

    public bool CanBuildCafeteria
    {
        get
        {
            if (!cafeteria.gameObject.activeSelf) return true;

            return cafeteria.CanBuildMoreTables;
        }
    }

    public void BuildCafeteria()
    {
        if (!CanBuildCafeteria) return;

        if (StationManager.Instance.StationRating < 20)
        {
            StationManager.Instance.IncreaseStationRating();
        }

        if (!cafeteria.gameObject.activeSelf)
        {
            cafeteria.gameObject.SetActive(true);
            return;
        }

        cafeteria.BuildTable();
    }

}
