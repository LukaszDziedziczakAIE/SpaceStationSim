using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaffateriaStation : NeedStation
{
    [SerializeField] GameObject foodObject;

    private void Start()
    {
        foodObject.SetActive(false);
        NeedsManager.Instance.cafStations.Add(this);
    }
}
