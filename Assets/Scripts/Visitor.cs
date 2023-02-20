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
    Animator animator;
    WonderingArea wonderingArea;

    Vector3 lastFrame;
    [SerializeField] Vector3 testingLocation;
    Vector3 spawnPosition;

    bool hasMovedAwayFromSpawn; // for debug replace with needs met later

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        wonderingArea = FindObjectOfType<WonderingArea>();
    }

    private void Start()
    {
        spawnPosition = transform.position;
        targetLocation = transform.position;
        ApplyRandomMaterials();

        MoveTo(wonderingArea.RandomPoint());
    }

    private void Update()
    {
        print(distanceToTargetLocation);
        Movement();
        UpdateAnimator();
        lastFrame = transform.position;
    }

    private void UpdateAnimator()
    {
        if (IsMoving)
        {
            animator.SetFloat("Speed", 1f);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }

    private void Movement()
    {
        if (distanceToTestingLocation < movementThreshold) MoveTo(spawnPosition);

        if (transform.position == targetLocation) return;

        if (navMeshAgent.destination != targetLocation) navMeshAgent.SetDestination(targetLocation);

        if (Vector3.Distance(transform.position, targetLocation) < movementThreshold) transform.position = targetLocation;

        if (nearSpawnPoint && hasMovedAwayFromSpawn) Destroy(gameObject);
        if (!nearSpawnPoint) hasMovedAwayFromSpawn = true;
    }

    public void MoveTo(Vector3 target)
    {
        targetLocation = target;
    }

    private void Testing()
    {
        testingLocation = targetLocation;
        testingLocation.x += 10;
        MoveTo(testingLocation);
    }

    private void ApplyRandomMaterials()
    {
        body_MeshRenderer.material = bodyMaterials[Random.Range(0, bodyMaterials.Length)];
    }

    public bool IsMoving { get { return lastFrame != transform.position; } }

    private bool nearSpawnPoint { get { return Vector3.Distance(transform.position, spawnPosition) < movementThreshold; } }

    private float distanceToTargetLocation { get { return Vector3.Distance(transform.position, targetLocation); } }

    private float distanceToTestingLocation { get { return Vector3.Distance(transform.position, testingLocation); } }

    public void SetUsingTouchscreen(bool usingTouchscreen)
    {
        animator.SetBool("UsingTouchscreen", usingTouchscreen);
    }
}
