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
        Vector3 destination = GameManager.currentSack.transform.position;
        Vector3 currentPos;

        float currentSpeed = Random.Range(Bird.minSpeed, Bird.maxSpeed);
        float duration = Vector3.Distance(origin, destination) / currentSpeed;

        float currentLerpTime = 0;
        float clampLerpTime = 0;

        while (true)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= duration)
            {
                GameManager.numPresentsStored = 0;

                Bird.SetState(StateMachine.BirdState.CaughtPresents, Bird);
                break;
            }

            clampLerpTime = Mathf.Clamp01(currentLerpTime / duration);
            currentPos = Vector3.Lerp(origin, destination, Bird.targetCurve.Evaluate(clampLerpTime));

            Bird.transform.position = currentPos;
            yield return instruction;
        }
    }
}