using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Bird Bird;

    public State(Bird bird)
    {
        Bird = bird;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual IEnumerator Fly()
    {
        yield break;
    }

    public virtual IEnumerator FlyRefactored(Vector3 origin, Vector3 destination, float speed)
    {
        YieldInstruction instruction = new WaitForEndOfFrame();
        Vector3 currentPos;

        float duration = Vector3.Distance(origin, destination) / speed;
        float currentLerpTime = 0;
        float clampLerpTime = 0;

        while (true)
        {
            currentLerpTime += Time.deltaTime;
            if(currentLerpTime > duration)
            {
                //Trigger Finished Event
            }

            clampLerpTime = Mathf.Clamp01(currentLerpTime / duration);
            currentPos = Vector3.Lerp(origin, destination, Bird.standardCurve.Evaluate(clampLerpTime));

            Bird.transform.position = currentPos;
            yield return instruction;
        }
    }
}
