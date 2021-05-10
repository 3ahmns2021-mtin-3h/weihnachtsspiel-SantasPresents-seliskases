using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeihnachtsmannController : MonoBehaviour
{
    public GameObject sack;
    [Range(50, 150)]
    public float speed = 100f;
    public float paralyzeDelay;
    public int maxPresents = 3;
    public float maxDistance;
    public KeyCode keyCode;
    [HideInInspector]
    public bool paralyzed;

    private int numPresentsCarrying;

    private void Update()
    {
        if (paralyzed)
        {
            return;
        }        

        if (Vector3.Distance(transform.position, sack.transform.position) <= maxDistance && Input.GetKeyDown(keyCode))
        {
            PlacePresents();
        }

        Move();
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.position += new Vector3(moveHorizontal, 0, 0);
    }

    private void PlacePresents()
    {
        GameManager.numPresentsStored += numPresentsCarrying;
        numPresentsCarrying = 0;
    }

    private void CarryPresent(GameObject present)
    {
        if (numPresentsCarrying >= maxPresents)
        {
            return;
        }

        numPresentsCarrying += 1;
        Destroy(present);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Present":
                CarryPresent(collision.gameObject);
                Destroy(collision.gameObject);
                break;
            case "Rock":
                StartCoroutine(Paralyze());
                Destroy(collision.gameObject);
                break;
        }
    }

    private IEnumerator Paralyze()
    {
        YieldInstruction instruction = new WaitForEndOfFrame();

        paralyzed = true;
        numPresentsCarrying = 0;
        float time = 0;

        while (true)
        {
            time += Time.deltaTime;

            if (time >= paralyzeDelay)
            {
                paralyzed = false;
                break;
            }
            yield return instruction;
        }
    }
}