using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : StateMachine
{
    public AnimationCurve standardCurve;
    public AnimationCurve targetCurve;
    public float minSpeed;
    public float maxSpeed;
    public float detectionRadius;
    public float maxDistance;
    public float scareBirdDistance;
    public KeyCode scareBirdKey;

    //Only used for debugging
    public string currentState;

    private void Start()
    {
        currentState = "Back To Center";
        SetState(new BackToCenter(this));
    }

    private void Update()
    {
        if (Input.GetKeyDown(scareBirdKey))
        {
            currentState = "Scared";
            SetState(new Scared(this));
        }
    }
}
