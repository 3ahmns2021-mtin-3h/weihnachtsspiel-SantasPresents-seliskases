using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 0.5f);
    }
}
