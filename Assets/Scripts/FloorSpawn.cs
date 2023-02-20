using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawn : MonoBehaviour
{
    [SerializeField] GameObject floorPrefab;
    [SerializeField] float cellSize;
    [SerializeField] int length;
    [SerializeField] int width;
    [SerializeField] List<GameObject> floorTiles;


    private void Start()
    {
        SpwanFloor();
    }

    private void SpwanFloor()
    {
        floorTiles = new List<GameObject>();

        for (int x = 0; x < length; x++)
        {
            for (int z = 0; z < width; z++)
            {
                Vector3 spawnPosition = new Vector3(x * cellSize, 0, z * cellSize);
                Quaternion spawnRotation = Quaternion.Euler(0 , 90 , 0);
                GameObject tile = Instantiate(floorPrefab, spawnPosition, spawnRotation);
                floorTiles.Add(tile);
            }
        }
    }
}
