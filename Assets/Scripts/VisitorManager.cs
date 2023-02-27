using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VisitorManager : MonoBehaviour
{
    public static VisitorManager Instance { get; private set; }

    [SerializeField] VisitorSpawn visitorSpawn;
    [SerializeField] int SpawnPercentageBase;
    [SerializeField] int MaxVisitorsBase;
    [SerializeField] int MaxVisitorsMultiplier;
    [SerializeField] int SpawnRateMultiplier = 1;
    [SerializeField] int SpawnRepeatsMax = 10;

    public List<Visitor> VisitorList;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameTime.Instance.HourTick += GameTime_HourTick;
    }

    private void GameTime_HourTick()
    {
        SpawnNewVisitor();
    }

    private void SpawnNewVisitor()
    {
        if (NumberOfVisitors >= MaxVisitors) return;

        if (NumberOfVisitors == 0)
        {
            visitorSpawn.AddToSpawnQue();
            return;
        }

        int repeats = Mathf.Clamp(StationManager.Instance.StationRating, 1 , SpawnRepeatsMax);

        while (repeats > 0)
        {
            int randomInt = Random.Range(0, 100);

            if (randomInt < StationManager.Instance.StationRating * SpawnRateMultiplier)
            {
                visitorSpawn.AddToSpawnQue();
            }

            repeats--;
        }
    }

    public int NumberOfVisitors { get { return VisitorList.Count; } }

    public int MaxVisitors
    {
        get
        {
            return MaxVisitorsBase + (StationManager.Instance.StationRating * MaxVisitorsMultiplier);
        }
    }
}
