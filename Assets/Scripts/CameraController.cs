using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    InputReader inputReader;
    Camera cam;

    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] float movementSpeed;
    [SerializeField] float roatationSpeed;
    [SerializeField] float zoomSpeed;
    [SerializeField] float maxZoom;
    [SerializeField] float minZoom;
    [SerializeField] LayerMask floorLayer;

    private void Awake()
    {
        inputReader = GetComponent<InputReader>();
        cam = Camera.main;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleZoom()
    {
        if (inputReader.CameraZoom == 0) return;

        CinemachineTransposer transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        if (transposer == null)
        {
            Debug.LogError("CameraController: missing transposer referance");
            return;
        }

        Vector3 followOffset = transposer.m_FollowOffset;

        if (inputReader.CameraZoom > 0)
        {
            followOffset.y += zoomSpeed * Time.deltaTime;
        }
        else // inputReader.CameraZoom < 0
        {
            followOffset.y -= zoomSpeed * Time.deltaTime;
        }

        if (followOffset.y < minZoom) followOffset.y = minZoom;
        if (followOffset.y > maxZoom) followOffset.y = maxZoom;

        transposer.m_FollowOffset = followOffset;
    }

    private void HandleRotation()
    {
        if (inputReader.CameraRotation == 0) return;

        Vector3 rotation = transform.eulerAngles;

        if (inputReader.CameraRotation > 0)
        {
            rotation.y += roatationSpeed * Time.deltaTime;
        }
        else // inputReader.CameraRotation < 0
        {
            rotation.y -= roatationSpeed * Time.deltaTime;
        }

        transform.eulerAngles = rotation;
    }

    private void HandleMovement()
    {
        if (inputReader.CameraMovement.magnitude == 0) return;
        Vector3 origin = transform.position;
        Vector3 movement = transform.forward * inputReader.CameraMovement.y + transform.right * inputReader.CameraMovement.x;

        transform.position += movement * movementSpeed * Time.deltaTime;

        if (!AboveFloor) transform.position = origin;
    }

    private bool AboveFloor
    {
        get
        {
            Vector3 origin = transform.position;
            origin.y += 1;
            Ray ray = new Ray(origin, -transform.up);
            return Physics.Raycast(ray, 3, floorLayer);
        }
    }
}
