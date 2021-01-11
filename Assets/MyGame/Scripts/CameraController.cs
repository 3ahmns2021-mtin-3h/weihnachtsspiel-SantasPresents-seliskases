using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 cameraOffset;
    public float followSpeed;
    public float xMin;

    private Vector3 velocity = Vector3.zero;
    private Transform target;
    private new Camera camera; 

    private void Start()
    {
        camera = Camera.main;
        target = GameManager.weihnachtsmann.transform;
    }
    private void FixedUpdate()
    {
        Vector3 targetPos = target.position + cameraOffset;
        Vector3 clampedPos = new Vector3(Mathf.Clamp(targetPos.x, xMin, float.MaxValue), targetPos.y, targetPos.z);
        Vector3 smoothPos = Vector3.SmoothDamp(camera.gameObject.transform.position, clampedPos, ref velocity, followSpeed * Time.deltaTime);

        camera.gameObject.transform.position = smoothPos;
    } 
}
