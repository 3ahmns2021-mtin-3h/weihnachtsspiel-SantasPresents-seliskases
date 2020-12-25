using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sack : MonoBehaviour
{
    public float maxDistance;
    public KeyCode keyCode;
    [HideInInspector]
    public int presents;

    private void Update()
    {
        Vector3 weihnachtsmannPos = GameManager.weihnachtsmann.transform.position;

        if (Distance(transform.position, weihnachtsmannPos) <= maxDistance && Input.GetKeyDown(keyCode))
        {
            presents++;
        }
    }

    private float Distance(Vector3 startPoint, Vector3 endPoint)
    {
        float xDistance = endPoint.x - startPoint.x;
        float zDistance = endPoint.z - startPoint.z;

        return Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(zDistance, 2));
    }
}
