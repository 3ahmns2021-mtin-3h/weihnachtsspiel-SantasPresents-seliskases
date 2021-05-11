using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeihnachtsmannController : MonoBehaviour
{
    [Range(50, 150)]
    public float speed = 100f;
    public float paralyzeDelay;
    public int maxPresents = 3;
    public float placePresentRadius;
    public float scareBirdRadius;
    public KeyCode scareBirdKey;
    public KeyCode placePresentsKey;
    [HideInInspector]
    public bool paralyzed;

    public delegate void ScareBird();
    public static event ScareBird OnBirdScared;

    private int numPresentsCarrying;

    private void Update()
    {
        if (paralyzed)
        {
            return;
        }        

        if (Vector3.Distance(transform.position, GameManager.currentSack.transform.position) < placePresentRadius && Input.GetKeyDown(placePresentsKey))
        {
            PlacePresents();
        }

        if (Input.GetKeyDown(scareBirdKey))
        {
            if(Vector2.Distance(transform.position, GameManager.currentBird.transform.position) < scareBirdRadius)
            {
                OnBirdScared();
            }
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