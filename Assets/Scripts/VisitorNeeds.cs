using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorNeeds : MonoBehaviour
{
    public enum EState
    {
        None,
        Waiting,
        Fufilling,
        Exiting
    }

    private Visitor visitor;
    public EState State;
    public List<ENeed> Needs = new List<ENeed>();

    private float needsTimer;
    private Vector3 needsLocation;

    private NeedsManager needsManager;

    private void Awake()
    {
        needsManager = FindObjectOfType<NeedsManager>();
        visitor = GetComponent<Visitor>();
    }

    private void Update()
    {
        FufillNeeds();

        if (needsTimer > 0)
        {
            needsTimer -= Time.deltaTime;

            if (needsTimer <= 0)
            {

            }
        }
    }

    public bool HasNeeds { get { return Needs.Count > 0; } }

    private void Start()
    {
        Needs.Add(ENeed.Fuel);
        State = EState.Waiting;
    }

    private void FufillNeeds()
    {
        if (State != EState.Waiting || visitor.IsMoving) return;

        if (Needs.Contains(ENeed.Fuel) && needsManager.RefuelStationAvailable)
        {
            RefuelStation refuelStation = needsManager.nextAvailableRefuelStation;
            refuelStation.currentVisitor = visitor;
            visitor.MoveTo(refuelStation.Location);
            State = EState.Fufilling;
        }
    }
}
