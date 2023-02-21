using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorSpawn : MonoBehaviour
{
    [SerializeField] Visitor visitorMalePrefab;
    [SerializeField] Visitor visitorFemalePrefab;
    [SerializeField] Transform spawnPoint;

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
    }

    public Vector3 SpawnPoint() => spawnPoint.position;
}
