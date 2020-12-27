using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sack : MonoBehaviour
{
    public float maxDistance;
    public KeyCode keyCode;
    [HideInInspector]
    public int presents = 0;

    private void Update()
    {
        Vector3 weihnachtsmannPos = GameManager.weihnachtsmann.transform.position;

        if (Distance(transform.position, weihnachtsmannPos) <= maxDistance && Input.GetKeyDown(keyCode))
        {
            int tempNumPresents = WeihnachtsmannController.instance.numPresents;
            presents += tempNumPresents;
            WeihnachtsmannController.instance.numPresents -= tempNumPresents;
        }
    }

    private float Distance(Vector3 startPoint, Vector3 endPoint)
    {
        float xDistance = endPoint.x - startPoint.x;
        float zDistance = endPoint.z - startPoint.z;

        return Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(zDistance, 2));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255, 0, 0, 250);
        //Gizmos.DrawSphere(transform.position, maxDistance);
    }
}
