using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipControl : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float minSpeed = -10F;
    [SerializeField] private float maxSpeed = 50F;
    [SerializeField] private float acceleration = 5F; // points/minute
    private int userSpeedInput = 1; // start with no speed
    private int previousSpeedInput = 10;
    private float currentSpeed = 0;

    [Header("Steering")]
    [SerializeField] private float turnSharpness = 10F; // turn sharpness in degrees
    [SerializeField] private float turnDuration = 200; // Time it takes to fully steer
    private float turnDegreesPerSecond;
    private float currentTurnAngle = 0;

    private void Start()
    {
        turnDegreesPerSecond = turnSharpness / turnDuration;
    }

    private void Update()
    {
        int userSteeringInput = (int)GameInput.Instance.GetKeyMovement().x;
        int steeringDirection = userSteeringInput != -1 && currentTurnAngle < 0 ? 1 // when moving left and left key is not pressed,move right.
            : userSteeringInput != 1 && currentTurnAngle > 0 ? -1 // when moving right and right key is not pressed, move left.
            : userSteeringInput; // when no other condition is met, do not turn.

        currentTurnAngle = Math.Clamp(currentTurnAngle + (turnDegreesPerSecond * steeringDirection), -turnSharpness, turnSharpness);
        transform.Rotate(Vector3.up, currentTurnAngle * Time.deltaTime);


        float[] speedModes =
        {
            minSpeed, // Back
            0, // Stop
            maxSpeed/4, // One fourth
            maxSpeed/2, // Half
            maxSpeed/4*3, // Three fourth 
            maxSpeed // Max
        };

        int speedInput = (int)GameInput.Instance.GetKeyMovement().y;
        if (speedInput != previousSpeedInput)
        {
            previousSpeedInput = speedInput;
            userSpeedInput = Mathf.Clamp(userSpeedInput + speedInput, 0, speedModes.Length - 1);
        }

        currentSpeed = Mathf.MoveTowards(currentSpeed, speedModes[userSpeedInput], acceleration * Time.deltaTime);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }
}
