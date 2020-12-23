using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeihnachtsmannController : MonoBehaviour
{
    [Range(50, 150)]
    public float speed = 100f;
    public float fallDelay;
    public int maxPresents = 3;

    private int numPresents;
    
    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.position += new Vector3(moveHorizontal, 0, 0);
        //Trigger movement animation / sound
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Present":
                CarryPresent(collision.gameObject);                
                break;
            case "Stone":
                FallOver();
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
    }

    private void FallOver()
    {
        //Trigger animation / sound
        numPresents = 0;

        while (true)
        {
            float time = Time.deltaTime;
            if(time >= fallDelay)
            {
                break;
            }
        }        
    }
}
