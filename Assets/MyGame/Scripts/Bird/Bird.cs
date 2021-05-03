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
    public float scareBirdRadius;
    public KeyCode scareBirdKey;

    private void Start()
    {
        SetState(States.BackToCenter, this);
    }

    private void Update()
    {
        if (IsScared())
        {
            SetState(States.Scared, this);
        }
    }

    private bool IsScared()
    {
        if(currentState != States.Targeting)
        {
            return false;
        }

        if (!Input.GetKeyDown(scareBirdKey))
        {
            return false;
        }

        if (Vector2.Distance(transform.position, GameManager.currentWeihnachtsmann.transform.position) > scareBirdRadius)
        {
            return false;
        }

        if (!GameManager.currentWeihnachtsmann.gameObject.GetComponent<WeihnachtsmannController>().paralyzed)
        {
            return false;
        }

        return true;
    }
}
