using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Visitor : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer body_MeshRenderer;
    [SerializeField] SkinnedMeshRenderer head_MeshRenderer;

    [SerializeField] Vector3 targetLocation;
    [SerializeField] float movementThreshold;
    [SerializeField] Material[] bodyMaterials;
    [SerializeField] Material[] headMaterials;


    NavMeshAgent navMeshAgent;
    CharacterController characterController;
    public Animator Animator { get; private set; }
    WonderingArea wonderingArea;
    VisitorNeeds visitorNeeds;
    public VisitorStatusBar StatusBar { get; private set; }

    Vector3 lastFrame;
    [SerializeField] Vector3 testingLocation;
    Vector3 spawnPosition;

    bool hasMovedAwayFromSpawn; // for debug replace with needs met later

    public event Action ArrivedAtLocationEvent;
    public bool Moving;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        Animator = GetComponentInChildren<Animator>();
        wonderingArea = FindObjectOfType<WonderingArea>();
        visitorNeeds = GetComponent<VisitorNeeds>();
        StatusBar = GetComponentInChildren<VisitorStatusBar>();

        lastFrame = Vector3.zero;
    }

    private void Start()
    {
        VisitorManager.Instance.VisitorList.Add(this);
        spawnPosition = transform.position;
        //targetLocation = transform.position;
        ApplyRandomMaterials();

        KeepWondering();
        visitorNeeds.SetNewState(VisitorNeeds.EState.Arived);

        StatusBar.Hide();
    }

    private void Update()
    {
        Arrived();
        Movement();
        UpdateAnimator();
        lastFrame = transform.position;
    }

    private void Arrived()
    {
        if (visitorNeeds.State == VisitorNeeds.EState.Arived && IsNearTargetLocation)
        {
            visitorNeeds.SetNewState(VisitorNeeds.EState.Waiting);
        }
    }

    private void UpdateAnimator()
    {
        if (IsMoving())
        {
            Animator.SetFloat("Speed", 1f);
        }
        else
        {
            Animator.SetFloat("Speed", 0f);
        }
    }

    private void Movement()
    {
        if (!Moving) return;
        /*if (transform.position == targetLocation) return;

        if (navMeshAgent.destination != targetLocation) navMeshAgent.SetDestination(targetLocation);

        if (Vector3.Distance(transform.position, targetLocation) < movementThreshold) transform.position = targetLocation;

        if (nearSpawnPoint && hasMovedAwayFromSpawn) Destroy(gameObject);
        if (!nearSpawnPoint) hasMovedAwayFromSpawn = true;*/

        if (IsNearTargetLocation) ArrivedAtLocation();
    }

    public void MoveTo(Vector3 target)
    {
        targetLocation = target;
        navMeshAgent.SetDestination(targetLocation);
        Moving = true;

        
    }

    private void ArrivedAtLocation()
    {
        Moving = false;
        ArrivedAtLocationEvent?.Invoke();

        if (visitorNeeds.State == VisitorNeeds.EState.Arived) visitorNeeds.SetNewState(VisitorNeeds.EState.Waiting);

        visitorNeeds.FufillNeeds();
        //visitorNeeds.CompleteNeed();


        if (visitorNeeds.State == VisitorNeeds.EState.Exiting)
        {
            //print("destroying " + name);
            Destroy(gameObject);
        }
    }

    private void ApplyRandomMaterials()
    {
        body_MeshRenderer.material = bodyMaterials[Random.Range(0, bodyMaterials.Length)];
        head_MeshRenderer.material = headMaterials[Random.Range(0, headMaterials.Length)];
    }

    public bool IsMoving()
    {
        return lastFrame != transform.position;
    }

    private bool nearSpawnPoint { get { return Vector3.Distance(transform.position, spawnPosition) < movementThreshold; } }

    private float distanceToTargetLocation { get { return Vector3.Distance(transform.position, targetLocation); } }

    private float distanceToTestingLocation { get { return Vector3.Distance(transform.position, testingLocation); } }

    public bool IsNearTargetLocation { get { return distanceToTargetLocation < movementThreshold; } }

    public void SetUsingTouchscreen(bool usingTouchscreen)
    {
        Animator.SetBool("UsingTouchscreen", usingTouchscreen);
    }

    public void KeepWondering()
    {
        MoveTo(wonderingArea.RandomPoint());
    }

    public void ExitStation()
    {
        MoveTo(spawnPosition);
    }

    private void OnDestroy()
    {
        VisitorManager.Instance.VisitorList.Remove(this);
    }
}
