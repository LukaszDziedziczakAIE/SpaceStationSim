using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    public static GameTime Instance { get; private set; }

    [SerializeField] float LengthOfHour; // IRL seconds

    public int Day { get; private set; } = 1;
    public int Hour { get; private set; } = 0;

    float timer;

    public event Action HourTick;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > LengthOfHour)
        {
            Hour++;

            if (Hour == 24)
            {
                Hour = 0;
                Day++;
            }

            HourTick?.Invoke();
            timer = 0;
        }
    }
}
