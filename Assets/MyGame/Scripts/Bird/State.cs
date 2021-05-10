using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Bird Bird;

    public delegate IEnumerator FlyAction();
    public static event FlyAction OnFlightFinished;
    public static Coroutine currentFlight;

    public State(Bird bird)
    {
        Bird = bird;
    }

    public virtual IEnumerator Enter()
    {
        yield break;
    }

    public virtual IEnumerator Exit()
    {
        yield break;
    }

    public virtual IEnumerator FlightAnimation(Vector3 origin, Vector3 destination, float speed, AnimationCurve curve)
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
                if (OnFlightFinished != null)
                {
                    Bird.StartCoroutine(OnFlightFinished());
                    break;
                }
            }

            clampLerpTime = Mathf.Clamp01(currentLerpTime / duration);
            currentPos = Vector3.Lerp(origin, destination, curve.Evaluate(clampLerpTime));

            Bird.transform.position = currentPos;
            yield return instruction;
        }
    }
}
