using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeihnachtsmannController : MonoBehaviour
{
    [Range(50, 150)]
    public float speed = 100f;
    public float fallDelay;
    public int maxPresents = 3;
    [HideInInspector]
    public int numPresents;
    public static WeihnachtsmannController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {       
        float moveHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.position += new Vector3(moveHorizontal, 0, 0);
        //Trigger movement animation and sound       

        if (Input.GetKeyDown(scareBirdKey))
        {

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision");

        switch (collision.gameObject.tag)
        {
            case "Present":
                CarryPresent(collision.gameObject);                
                Destroy(collision.gameObject);
                break;
            case "Rock":
                FallOver();
                Destroy(collision.gameObject);
                break;
        }
    }

    private void CarryPresent(GameObject present)
    {
        if(numPresents >= maxPresents)
        {
            return;            
        }

        numPresents += 1;
        Destroy(present);
        //Trigger carry-animation
        //Add a present to the Weihnachtsmann-illustration
    }

    private void FallOver()
    {
        //Trigger animation and sound
        numPresents = 0;
        float time = 0;

        while (true)
        {
            time += Time.deltaTime;

            if (time >= fallDelay)
            {
                break;
            }
        }        
    }
}
