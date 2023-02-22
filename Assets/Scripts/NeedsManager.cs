using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsManager : MonoBehaviour
{
    public static NeedsManager Instance { get; private set; }

    public List<RefuelStation> refuelStations = new List<RefuelStation>();
    public List<CaffateriaStation> cafStations = new List<CaffateriaStation>();

    private void Awake()
    {
        Instance = this;
    }

    public bool RefuelStationAvailable
    {
        get
        {
            if (refuelStations.Count == 0) return false;

            foreach (RefuelStation refuelStation in refuelStations)
            {
                if (!refuelStation.Occupied) return true;
            }

            return false;
        }
    }

    public RefuelStation nextAvailableRefuelStation
    {
        get
        {
            if (!RefuelStationAvailable) return null;
            foreach (RefuelStation refuelStation in refuelStations)
            {
                if (!refuelStation.Occupied) return refuelStation;
            }
            return null;
        }
    }

    public bool CaffateriaStationAvailable
    {
        get
        {
            if (cafStations.Count == 0) return false;

            foreach (CaffateriaStation cafStation in cafStations)
            {
                if (!cafStation.Occupied) return true;
            }

            return false;
        }
    }

    public CaffateriaStation nextAvailableCaffateriaStation
    {
        get
        {
            if (!CaffateriaStationAvailable) return null;
            foreach (CaffateriaStation cafStation in cafStations)
            {
                if (!cafStation.Occupied) return cafStation;
            }
            return null;
        }
    }
}
