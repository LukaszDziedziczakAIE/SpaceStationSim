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
    RefuelStation refuelStation;

    private void Awake()
    {
        needsManager = FindObjectOfType<NeedsManager>();
        visitor = GetComponent<Visitor>();
    }

    private void Start()
    {
        Needs.Add(ENeed.Fuel);
        tickTimer = 1f;
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
            refuelStation = needsManager.nextAvailableRefuelStation;
            refuelStation.currentVisitor = visitor;
            visitor.MoveTo(refuelStation.Location);
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
            needsTimer = refuelStation.TimeToComplete;
            visitor.transform.LookAt(refuelStation.transform);
            visitor.Animator.SetBool("UsingTouchscreen",true);
        }


        if (State == EState.Fufilling)
        {
            
            visitor.StatusBar.UpdateBar(NeedsPercentage);

            if (needsTimer <= 0)
            {
                visitor.StatusBar.Hide();
                SetNewState(EState.Waiting);
                Needs.RemoveAt(0);
                visitor.Animator.SetBool("UsingTouchscreen", false);
                Currency.Instance.AddMoney(refuelStation.CostToRefuel);

                refuelStation.currentVisitor = null;
                refuelStation = null;
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
            if (refuelStation == null) return 0f;
            return needsTimer / refuelStation.TimeToComplete;
        }
    }
}
