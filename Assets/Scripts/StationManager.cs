using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManager : MonoBehaviour
{
    public static StationManager Instance { get; private set; }

    [field: SerializeField] public int StationRating { get; private set; } = 1;

    public event Action StationRatingChanged;

    private void Awake()
    {
        Instance = this;
    }

    public void IncreaseStationRating(int amount = 1)
    {
        StationRating += amount;
        StationRatingChanged?.Invoke();
    }
}
