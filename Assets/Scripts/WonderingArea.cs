using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderingArea : MonoBehaviour
{
    Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    public Vector3 RandomPoint()
    {
        return new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            0,
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
    }
}
