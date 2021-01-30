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

    [HideInInspector]
    public string currentState;

    private void Start()
    {
        currentState = "Back To Center";
        SetState(new BackToCenter(this));
    }

    private void Update()
    {
        Debug.Log(currentState);

        if (Input.GetKeyDown(scareBirdKey) && Distance(transform.position, GameManager.weihnachtsmann.transform.position) < scareBirdRadius)
        {
            if (!GameManager.weihnachtsmann.gameObject.GetComponent<WeihnachtsmannController>().paralyzed)
            {
                currentState = "Scared";
                SetState(new Scared(this));
            }            
        }
    }

    private float Distance(Vector3 startPoint, Vector3 endPoint)
    {
        float xDistance = endPoint.x - startPoint.x;
        float yDistance = endPoint.y - startPoint.y;

        return Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));
    }
}
