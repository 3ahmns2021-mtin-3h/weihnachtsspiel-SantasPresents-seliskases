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
    public float maxDistance;
    public float scareBirdDistance;
    public KeyCode scareBirdKey;

    private bool currentlyTargeting = false;
    private bool currentlySearching = false;
    private bool currentlyScared = false;

    private void Update()
    {
        float distance = Distance(transform.position, GameManager.sack.transform.position);

        //Targeting
        if (distance <= detectionRadius && currentlyTargeting == false && currentlyScared == false)
        {
            StopAllCoroutines();

            float duration = distance / maxSpeed;

            currentlyTargeting = true;
            StartCoroutine(MoveTowards(GameManager.sack.transform.position, targetCurve, duration, true));
            Destroy(gameObject, duration);
        }

        // Searching
        else if(currentlySearching == false && currentlyTargeting == false && distance < maxDistance && currentlyScared == false)
        {
            StopAllCoroutines();

            float left = GameManager.canvas.pixelRect.width / -2;
            float right = GameManager.canvas.pixelRect.width / 2;
            float top = GameManager.canvas.pixelRect.height / 2;
            float bottom = GameManager.canvas.pixelRect.height / -2;            

            float x = Random.Range(left, right);
            float y = Random.Range(bottom, top);
            
            Vector3 destination = new Vector3(x, y, 0);

            float destinationDistance = Distance(transform.position, destination);
            float currentSpeed = Random.Range(minSpeed, maxSpeed);
            float duration = destinationDistance / currentSpeed;

            currentlySearching = true;
            StartCoroutine(MoveTowards(destination, standardCurve, duration, false));
        }
        
        // Back to center, triggered at spawn of bird
        else if(currentlySearching == false && currentlyTargeting == false && distance >= maxDistance && currentlyScared == false)
        {
            StopAllCoroutines();

            Vector3 destination = new Vector3(0, 0, 0);

            float destinationDistance = Distance(transform.position, destination);
            float currentSpeed = Random.Range(minSpeed, maxSpeed);
            float duration = destinationDistance / currentSpeed;

            currentlySearching = true;
            StartCoroutine(MoveTowards(destination, standardCurve, duration, false));
        }

        //Scare off
        if (Input.GetKeyDown(scareBirdKey) && Distance(transform.position, GameManager.weihnachtsmann.transform.position) < scareBirdDistance)
        {
            if (!GameManager.weihnachtsmann.GetComponent<WeihnachtsmannController>().paralyzed)
            {
                StopAllCoroutines();
                currentlyScared = true;

                StartCoroutine(MoveTowards(new Vector3(transform.position.x - 1000, transform.position.y, transform.position.z), targetCurve, 2, false));
                Destroy(gameObject, 3f);
            }
        }
    }

    private float Distance(Vector3 startPoint, Vector3 endPoint)
    {
        float xDistance = endPoint.x - startPoint.x;
        float zDistance = endPoint.y - startPoint.y;

        return Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(zDistance, 2));
    }

    private IEnumerator MoveTowards(Vector3 destination, AnimationCurve curve, float duration, bool targetingSack)
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

                if (targetingSack)
                {
                    GameManager.sack.presents = 0;
                }

                break;
            }   

            clampLerpTime = Mathf.Clamp01(currentLerpTime / duration);
            currentPos = Vector3.Lerp(origin, destination, curve.Evaluate(clampLerpTime));

            transform.position = currentPos;
            yield return instruction;
        }
    }
}
