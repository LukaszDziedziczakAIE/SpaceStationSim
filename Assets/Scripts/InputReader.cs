using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    Controls controls;

    public Vector2 CameraMovement { get; private set; }
    public float CameraRotation { get; private set; }
    public float CameraZoom { get; private set; }

    public event Action TestingAction;


    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();

    }

    public void OnCameraMovement(InputAction.CallbackContext context)
    {
        CameraMovement = context.ReadValue<Vector2>();
    }

    public void OnCameraRotation(InputAction.CallbackContext context)
    {
        CameraRotation = context.ReadValue<float>();
    }

    public void OnCameraZoom(InputAction.CallbackContext context)
    {
        CameraZoom = Mathf.Clamp(context.ReadValue<float>(), -1f, +1f);
    }

    public void OnTesting(InputAction.CallbackContext context)
    {
        if (context.performed) TestingAction?.Invoke();
    }
}
