using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeihnachtsmannController : MonoBehaviour
{
    [Range(1, 10)]
    public float maxSpeed;
    [Range(1, 10)]
    public float movementScalar;
    [Range(1, 100)]
    public float jumpScalar;
    public float jumpCooldown;
    public float paralyzeDelay;
    public int maxPresents = 3;
    public Rigidbody2D rb;
    [HideInInspector]
    public int numPresents;
    [HideInInspector]
    public bool paralyzed = false;
    public static WeihnachtsmannController instance;

    //Value between 0 and 1, corresponding to the angle of collision
    public float directionMultiplier;

    private bool canJump = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        jumpScalar *= 100;
        maxSpeed *= 100;
        movementScalar *= 100;
    }

    private void FixedUpdate()
    {
        if (paralyzed)
        {
            return;
        }

        float xMovement = Input.GetAxis("Horizontal");

        if(rb.velocity.magnitude < maxSpeed)
        {
            Vector2 movement = new Vector2(xMovement, 0);
            rb.AddForce(movementScalar * movement);
        }

        if (Input.GetKey(KeyCode.UpArrow) && directionMultiplier != 0 && canJump)
        {
            Vector2 jumpForce = new Vector2(0, jumpScalar * directionMultiplier);
            rb.AddForce(jumpForce);
            StartCoroutine(JumpCooldown());
        }
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

        directionMultiplier = Mathf.Sin(CollisionRadiant(collision));
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        directionMultiplier = Mathf.Sin(CollisionRadiant(collision));
    }

    private float CollisionRadiant(Collision2D collision)
    {
        foreach(ContactPoint2D contactPoint in collision.contacts)
        {
            Vector2 collisionDirection = contactPoint.point - rb.position;
            if(collisionDirection.y < 0)
            {
                Vector2 normal = contactPoint.normal;
                Vector2 velocity = rb.velocity;

                return Mathf.Clamp(Vector2.Angle(velocity, normal) * Mathf.PI / 180, 0, Mathf.PI);
            }
        }
        return 0;
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

    private IEnumerator Paralyze()
    {
        YieldInstruction instruction = new WaitForEndOfFrame();

        paralyzed = true;
        numPresents = 0;
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

    private IEnumerator JumpCooldown()
    {
        YieldInstruction instruction = new WaitForEndOfFrame();
        float time = jumpCooldown;
        canJump = false;

        while (true)
        {
            time -= Time.deltaTime;
            if(time < 0)
            {
                canJump = true;
                break;
            }
            yield return instruction;
        }
    }
}
