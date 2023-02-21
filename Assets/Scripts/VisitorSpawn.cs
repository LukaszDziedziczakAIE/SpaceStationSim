using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Random = UnityEngine.Random;

public class VisitorSpawn : MonoBehaviour
{
    [SerializeField] Visitor visitorMalePrefab;
    [SerializeField] Visitor visitorFemalePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnRate = 0.1f;

    float spawnTimer;
    int spawnQue;

    public event Action VisitorSpawned;

    private void Update()
    {
        SpawnLogic();
    }

    private void SpawnLogic()
    {
        if (spawnQue > 0)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0)
            {
                spawnTimer = spawnRate;
                spawnQue--;
                SpawnVisitor();
            }
        }
    }

    public void SpawnVisitor()
    {
        int random = Random.Range(0, 2);

        Visitor visitorPrefab;
        if (random == 0)
        {
            visitorPrefab = visitorMalePrefab;
        }
        else
        {
            visitorPrefab = visitorFemalePrefab;
        }

        Visitor visitor = Instantiate(visitorPrefab, spawnPoint.position, spawnPoint.rotation);
        visitor.name = "Visitor#" + Random.Range(1000, 10000);
        VisitorManager.Instance.VisitorList.Add(visitor);
        VisitorSpawned?.Invoke();
    }

    public Vector3 SpawnPoint() => spawnPoint.position;

    public void AddToSpawnQue(int amount = 1)
    {
        spawnQue += amount;
    }
}
