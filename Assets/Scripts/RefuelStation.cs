using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelStation : NeedStation
{
    private void Start()
    {
        NeedsManager.Instance.refuelStations.Add(this);
    }
    
}
