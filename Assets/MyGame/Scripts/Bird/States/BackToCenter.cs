using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToCenter : State
{
    public BackToCenter(Bird bird) : base(bird)
    {
    }

    public override IEnumerator Enter()
    {
        OnFlightFinished += Exit;

        Vector2 origin = Bird.transform.position;
        Vector2 destination = new Vector2(0, 0);
        float speed = Random.Range(Bird.minSpeed, Bird.maxSpeed);

        currentFlight = Bird.StartCoroutine(FlightAnimation(origin, destination, speed, Bird.standardCurve));
        yield break;
    }

    public override IEnumerator Exit()
    {
        OnFlightFinished -= Exit;
        Bird.SetState(StateMachine.BirdState.Searching, Bird);
        yield return null;
    }
}
