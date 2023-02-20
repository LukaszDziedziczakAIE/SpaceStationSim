using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorSpawn : MonoBehaviour
{


    [SerializeField] Visitor visitorPrefab;
    [SerializeField] Transform spawnPoint;

    public void SpawnVisitor()
    {
        Visitor visitor = Instantiate(visitorPrefab, spawnPoint.position, spawnPoint.rotation);
        visitor.name = "Visitor#" + Random.Range(1000, 10000);
    }

    public Vector3 SpawnPoint() => spawnPoint.position;
}
