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
    [SerializeField] int MaxVisitors;

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

        int randomInt = Random.Range(0, 4);

        visitorSpawn.AddToSpawnQue(randomInt);
    }

    public int NumberOfVisitors { get { return VisitorList.Count; } }
}
