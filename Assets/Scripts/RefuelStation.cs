using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelStation : MonoBehaviour
{
    [SerializeField] Transform visitorLocation;
    [SerializeField] float operationTime;

    public Visitor currentVisitor;

    public bool Occupied { get { return currentVisitor != null; } }

    public Vector3 Location { get { return visitorLocation.position; } }
}
