using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player" && !collision.CompareTag("Room"))
        {
            Destroy(gameObject);
        }
    }
}