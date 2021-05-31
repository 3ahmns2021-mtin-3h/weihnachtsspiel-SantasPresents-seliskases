using System.Collections;
using UnityEngine;

public class Scared : State
{
    public Scared(Bird bird) : base(bird)
    {
    }

    public override IEnumerator Enter()
    {
        OnFlightFinished += Exit;

        Vector2 origin = Bird.transform.position;
        Vector2 destination = new Vector2(Bird.transform.position.x - 1000, Bird.transform.position.y);
        float speed = Bird.maxSpeed * 1.5f;

        currentFlight = Bird.StartCoroutine(FlightAnimation(origin, destination, speed, Bird.standardCurve));
        yield break;
    }

    public override IEnumerator Exit()
    {
        OnFlightFinished -= Exit;
        Object.Destroy(Bird.gameObject);
        yield return null;
    }
}
