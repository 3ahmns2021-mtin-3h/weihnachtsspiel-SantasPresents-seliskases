using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtPresents : State
{
    public CaughtPresents(Bird bird) : base(bird)
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
        Vector3 destination = new Vector3(Bird.transform.position.x - 1000, Bird.transform.position.y, Bird.transform.position.z);

        float currentSpeed = Random.Range(Bird.minSpeed, Bird.maxSpeed);
        float duration = Vector3.Distance(origin, destination) / currentSpeed;

        Vector3 currentPos;
        float currentLerpTime = 0;
        float clampLerpTime = 0;

        while (true)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= duration)
            {
                Object.Destroy(Bird.gameObject);
                break;
            }

            clampLerpTime = Mathf.Clamp01(currentLerpTime / duration);
            currentPos = Vector3.Lerp(origin, destination, Bird.standardCurve.Evaluate(clampLerpTime));

            Bird.transform.position = currentPos;
            yield return instruction;
        }
    }
}
