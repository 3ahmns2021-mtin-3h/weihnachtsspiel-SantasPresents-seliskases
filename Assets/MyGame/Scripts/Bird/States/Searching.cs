using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searching : State
{
    public Searching(Bird bird) : base(bird)
    {
    }

    public override IEnumerator Enter()
    {
        OnFlightFinished += Exit;

        Vector2 origin = Bird.transform.position;
        Vector2 destination = Destination();
        float speed = Random.Range(Bird.minSpeed, Bird.maxSpeed);

        currentFlight = Bird.StartCoroutine(FlightAnimation(origin, destination, speed, Bird.standardCurve));
        yield break;
    }

    public override IEnumerator Exit()
    {
        OnFlightFinished -= Exit;

        if (Vector3.Distance(Bird.transform.position, GameManager.currentSack.transform.position) < Bird.detectionRadius)
        {
            Bird.SetState(StateMachine.BirdState.Targeting, Bird);
        }
        else
        {
            Bird.SetState(StateMachine.BirdState.Searching, Bird);
        }

        yield return null;
    }

    private Vector3 Destination()
    {
        float left = GameManager.canvas.pixelRect.width / -2;
        float right = GameManager.canvas.pixelRect.width / 2;
        float top = GameManager.canvas.pixelRect.height / 2;
        float bottom = GameManager.canvas.pixelRect.height / -2;

        float x = Random.Range(left, right);
        float y = Random.Range(bottom, top);

        return new Vector3(x, y, 0);
    }
}
