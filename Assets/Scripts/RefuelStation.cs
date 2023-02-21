using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelStation : MonoBehaviour
{
    [SerializeField] Transform visitorLocation;
    [SerializeField] float operationTime;
    [field: SerializeField] public int CostToRefuel { get; private set; }
    [field: SerializeField] public int CostToBuild { get; private set; }
    public float TimeToComplete;

    public Visitor currentVisitor;

    private void Start()
    {
        NeedsManager.Instance.refuelStations.Add(this);
    }

    public bool Occupied { get { return currentVisitor != null; } }

    public Vector3 Location { get { return visitorLocation.position; } }

    public Quaternion VisitorRotation { get { return visitorLocation.rotation; } }
}
