using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedStation : MonoBehaviour
{
    [SerializeField] protected Transform visitorLocation;
    //[SerializeField] protected float operationTime;
    [field: SerializeField] public int CostToRefuel { get; private set; }
    public float TimeToComplete;

    public Visitor currentVisitor;
    protected int timesUsed;

    public Vector3 Location { get { return visitorLocation.position; } }

    public Quaternion VisitorRotation { get { return visitorLocation.rotation; } }

    public bool Occupied { get { return currentVisitor != null; } }

    public void Used()
    {
        timesUsed++;
        if (timesUsed > 10)
        {
            timesUsed = 0;
            if (StationManager.Instance.StationRating < 10)
            {
                StationManager.Instance.IncreaseStationRating();
            }
        }
    }
}
