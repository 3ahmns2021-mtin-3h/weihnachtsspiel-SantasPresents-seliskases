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
        float duration = Vector3.Distance(origin, destination) / currentSpeed;

        Vector3 currentPos;
        float currentLerpTime = 0;
        float clampLerpTime = 0;

        while (true)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= duration)
            {
                Bird.SetState(StateMachine.BirdState.Searching, Bird);
                break;
            }

            clampLerpTime = Mathf.Clamp01(currentLerpTime / duration);
            currentPos = Vector3.Lerp(origin, destination, Bird.standardCurve.Evaluate(clampLerpTime));

            Bird.transform.position = currentPos;
            yield return instruction;
        }
    }
}
