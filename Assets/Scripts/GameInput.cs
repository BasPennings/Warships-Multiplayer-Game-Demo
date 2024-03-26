using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Debug.LogError("There can only be one GameInput instance! Access it with GameInput.Instance");

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.MouseLook.Enable();
        playerInputActions.Player.Movement.Enable();
        playerInputActions.Player.MouseScroll.Enable();
        playerInputActions.Player.Shoot.Enable();
    }

    public Vector2 GetMouseMovement()
    {
        return playerInputActions.Player.MouseLook.ReadValue<Vector2>();
    }

    public Vector2 GetMouseScroll()
    {
        return playerInputActions.Player.MouseScroll.ReadValue<Vector2>();
    }

    public Vector2 GetKeyMovement()
    {
        return playerInputActions.Player.Movement.ReadValue<Vector2>();
    }

    public bool isLeftClicking()
    {
        return playerInputActions.Player.Shoot.ReadValue<float>() > 0.5;
    }
}
