using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : State
{
    public Targeting(Bird bird) : base(bird)
    {
    }

    public override IEnumerator Enter()
    {
        OnFlightFinished += Exit;

        Vector2 origin = Bird.transform.position;
        Vector2 destination = GameManager.currentSack.transform.position;
        float speed = Random.Range(Bird.minSpeed, Bird.maxSpeed);

        currentFlight = Bird.StartCoroutine(FlightAnimation(origin,destination, speed, Bird.targetCurve));
        yield break;
    }

    public override IEnumerator Exit()
    {
        OnFlightFinished -= Exit;

        GameManager.numPresentsStored = 0;
        Bird.SetState(StateMachine.BirdState.CaughtPresents, Bird);

        yield return null;
    }
}