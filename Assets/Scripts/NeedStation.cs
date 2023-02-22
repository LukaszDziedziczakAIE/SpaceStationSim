using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedStation : MonoBehaviour
{
    [SerializeField] protected Transform visitorLocation;
    //[SerializeField] protected float operationTime;
    [field: SerializeField] public int CostToUse { get; private set; }
    public float TimeToComplete;

    protected int timesUsed;
    [SerializeField] protected int stationRatingIncreaseCap;
    [field: SerializeField] public string AnimationName {get; private set;}
    [field: SerializeField] public ENeed Need { get; private set; }
    public Visitor currentVisitor;

    public Vector3 Location { get { return visitorLocation.position; } }

    public Quaternion VisitorRotation { get { return visitorLocation.rotation; } }

    public bool Occupied { get { return currentVisitor != null; } }

    public void Used()
    {
        timesUsed++;
        if (timesUsed > 10)
        {
            timesUsed = 0;
            if (StationManager.Instance.StationRating < stationRatingIncreaseCap)
            {
                StationManager.Instance.IncreaseStationRating();
            }
        }
    }
}
