using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    [SerializeField] List<RefuelStation> refuelStations;

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
                return;
            }
        }
    }
}
