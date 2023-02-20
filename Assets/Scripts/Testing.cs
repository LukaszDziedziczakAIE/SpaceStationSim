using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    InputReader inputReader;
    VisitorSpawn visitorSpawn;

    private void Awake()
    {
        inputReader = FindObjectOfType<InputReader>();
        visitorSpawn = FindObjectOfType<VisitorSpawn>();
    }

    private void Start()
    {
        inputReader.TestingAction += inputReader_TestingAction;
    }

    private void inputReader_TestingAction()
    {
        visitorSpawn.SpawnVisitor();
    }
}
