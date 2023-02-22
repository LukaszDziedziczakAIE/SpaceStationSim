using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisitorNeeds : MonoBehaviour
{
    public enum EState
    {
        None,
        Arived,
        Waiting,
        MovingToFufillmnet,
        Fufilling,
        Exiting
    }

    private Visitor visitor;
    public EState State;
    public List<ENeed> Needs = new List<ENeed>();

    private float tickTimer;
    private float needsTimer;
    private Vector3 needsLocation;

    private NeedsManager needsManager;
    //RefuelStation refuelStation;
    NeedStation needStation;

    private void Awake()
    {
        needsManager = FindObjectOfType<NeedsManager>();
        visitor = GetComponent<Visitor>();
    }

    private void Start()
    {
        AddNeeds();
        
        tickTimer = 1f;
    }

    private void AddNeeds()
    {
        Needs.Add(ENeed.Fuel);

        if (NeedsManager.Instance.cafStations.Count > 0)
        {
            Needs.Add(ENeed.CafeFood);
        }
    }

    private void Update()
    {
        CompleteNeed();

        if (needsTimer > 0) needsTimer -= Time.deltaTime;
    }

    public bool HasNeeds { get { return Needs.Count > 0; } }

    public void FufillNeeds()
    {
        //print(State);
        if (State != EState.Waiting) return;

        if (Needs.Contains(ENeed.Fuel) && needsManager.RefuelStationAvailable)
        {
            needStation = needsManager.nextAvailableRefuelStation;
            needStation.currentVisitor = visitor;
            visitor.MoveTo(needStation.Location);
            SetNewState(EState.MovingToFufillmnet);
            return;
        }

        if (Needs.Contains(ENeed.CafeFood) && needsManager.CaffateriaStationAvailable)
        {
            needStation = needsManager.nextAvailableCaffateriaStation;
            needStation.currentVisitor = visitor;
            visitor.MoveTo(needStation.Location);
            SetNewState(EState.MovingToFufillmnet);
            return;
        }

        if (HasNeeds) visitor.KeepWondering();
        else
        {
            SetNewState(EState.Exiting);
            visitor.ExitStation();
        }
    }

    public void CompleteNeed()
    {
        if (State == EState.MovingToFufillmnet && visitor.IsNearTargetLocation)
        {
            SetNewState(EState.Fufilling);
            visitor.StatusBar.Show();
            needsTimer = needStation.TimeToComplete;
            visitor.transform.position = needStation.Location;
            visitor.transform.rotation = needStation.VisitorRotation;
            visitor.Animator.SetBool(needStation.AnimationName, true);
        }


        if (State == EState.Fufilling)
        {
            
            visitor.StatusBar.UpdateBar(NeedsPercentage);

            if (needsTimer <= 0)
            {
                visitor.StatusBar.Hide();
                SetNewState(EState.Waiting);
                Needs.Remove(needStation.Need);
                visitor.Animator.SetBool(needStation.AnimationName, false);
                Currency.Instance.AddMoney(needStation.CostToUse);
                needStation.Used();
                needStation.currentVisitor = null;
                needStation = null;
                FufillNeeds();
            }
        }
    }

    public void SetNewState(EState newState)
    {
        if (State == newState) return;
        State = newState;
    }

    private float NeedsPercentage
    {
        get
        {
            if (needStation == null) return 0f;
            return needsTimer / needStation.TimeToComplete;
        }
    }
}
