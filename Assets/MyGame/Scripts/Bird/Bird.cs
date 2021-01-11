using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public AnimationCurve standardCurve;
    public AnimationCurve targetCurve;
    public float minSpeed;
    public float maxSpeed;
    public float detectionRadius;

    private bool currentlyTargeting = false;
    private bool currentlySearching = false;

    private void Update()
    {
        float distance = Distance(transform.position, GameManager.sack.transform.position);

        if (distance <= detectionRadius && currentlyTargeting == false)
        {
            StopAllCoroutines();

            float duration = distance / maxSpeed;

            currentlyTargeting = true;
            StartCoroutine(MoveTowards(GameManager.sack.transform.position, targetCurve, duration));
        }
        else if(currentlySearching == false && currentlyTargeting == false)
        {
            StopAllCoroutines();

            float x = Random.Range(transform.position.x - 200, transform.position.x + 200);
            float y = Random.Range(transform.position.y - 200, transform.position.y + 200);
            Vector3 destination = new Vector3(x, y, 0);

            float destinationDistance = Distance(transform.position, destination);
            float currentSpeed = Random.Range(minSpeed, maxSpeed);
            float duration = destinationDistance / currentSpeed;

            currentlySearching = true;
            StartCoroutine(MoveTowards(destination, standardCurve, duration));
        }
    }

    private float Distance(Vector3 startPoint, Vector3 endPoint)
    {
        float xDistance = endPoint.x - startPoint.x;
        float zDistance = endPoint.z - startPoint.z;

        return Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(zDistance, 2));
    }

    private IEnumerator MoveTowards(Vector3 destination, AnimationCurve curve, float duration)
    {
        YieldInstruction instruction = new WaitForEndOfFrame();

        Vector3 origin = transform.position;

        Vector3 currentPos;
        float currentLerpTime = 0;
        float clampLerpTime = 0;

        while (true)
        {
            currentLerpTime += Time.deltaTime;
            if(currentLerpTime >= duration)
            {
                currentlySearching = false;
                currentlyTargeting = false;

                break;
            }   

            clampLerpTime = Mathf.Clamp01(currentLerpTime / duration);
            currentPos = Vector3.Lerp(origin, destination, curve.Evaluate(clampLerpTime));

            transform.position = currentPos;
            yield return instruction;
        }
    }
}
