using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : State
{
    public Targeting(Bird bird) : base(bird)
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
        Vector3 destination = GameManager.sack.transform.position;
        Vector3 currentPos;

        float currentSpeed = Random.Range(Bird.minSpeed, Bird.maxSpeed);
        float duration = Distance(origin, destination) / currentSpeed;

        float currentLerpTime = 0;
        float clampLerpTime = 0;

        while (true)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= duration)
            {
                GameManager.sack.presents = 0;

                Bird.SetState(new CaughtPresents(Bird));
                break;
            }

            clampLerpTime = Mathf.Clamp01(currentLerpTime / duration);
            currentPos = Vector3.Lerp(origin, destination, Bird.targetCurve.Evaluate(clampLerpTime));

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