using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToCenter : State
{
    public BackToCenter(Bird bird) : base(bird)
    {
    }

    public override IEnumerator Start()
    {
        Bird.StartCoroutine(Fly());
        yield break;
    }

    public override IEnumerator Fly()
    {
        YieldInstruction instruction = new WaitForEndOfFrame();

        Vector3 origin = Bird.transform.position;
        Vector3 destination = new Vector3(0, 0, 0);

        float currentSpeed = Random.Range(Bird.minSpeed, Bird.maxSpeed);
        float duration = Distance(origin, destination) / currentSpeed;

        Vector3 currentPos;
        float currentLerpTime = 0;
        float clampLerpTime = 0;

        while (true)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= duration)
            {
                Bird.currentState = "Searching";
                Bird.SetState(new Searching(Bird));
                break;
            }

            clampLerpTime = Mathf.Clamp01(currentLerpTime / duration);
            currentPos = Vector3.Lerp(origin, destination, Bird.standardCurve.Evaluate(clampLerpTime));

            Bird.transform.position = currentPos;
            yield return instruction;
        }
    }

    private float Distance(Vector3 startPoint, Vector3 endPoint)
    {
        float xDistance = endPoint.x - startPoint.x;
        float yDistance = endPoint.y - startPoint.y;

        return Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));
    }
}
