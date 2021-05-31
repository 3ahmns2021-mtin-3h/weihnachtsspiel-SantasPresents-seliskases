using System.Collections;
using UnityEngine;

public class CaughtPresents : State
{
    public CaughtPresents(Bird bird) : base(bird)
    {
    }

    public override IEnumerator Enter()
    {
        OnFlightFinished += Exit;

        Vector2 origin = Bird.transform.position;
        Vector2 destination = new Vector2(Bird.transform.position.x - 1000, Bird.transform.position.y);
        float currentSpeed = Random.Range(Bird.minSpeed, Bird.maxDistance);

        currentFlight = Bird.StartCoroutine(FlightAnimation(origin, destination, currentSpeed, Bird.standardCurve));
        yield break;
    }

    public override IEnumerator Exit()
    {
        OnFlightFinished -= Exit;
        Object.Destroy(Bird.gameObject);
        yield return null;
    }
}
