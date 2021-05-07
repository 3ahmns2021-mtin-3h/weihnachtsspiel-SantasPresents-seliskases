using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searching : State
{
    public Searching(Bird bird) : base(bird)
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
        Vector3 destination = Destination();
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
                if(Vector3.Distance(Bird.transform.position, GameManager.currentSack.transform.position) < Bird.detectionRadius)
                {
                    Bird.SetState(StateMachine.BirdState.Targeting, Bird);
                    break;
                }
                else
                {
                    Bird.SetState(StateMachine.BirdState.Searching, Bird);
                    break;
                }                
            }

            clampLerpTime = Mathf.Clamp01(currentLerpTime / duration);
            currentPos = Vector3.Lerp(origin, destination, Bird.standardCurve.Evaluate(clampLerpTime));

            Bird.transform.position = currentPos;
            yield return instruction;
        }
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
