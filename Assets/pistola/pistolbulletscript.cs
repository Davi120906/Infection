using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Tags que a bala deve destruir ao colidir
        if (collision.collider.CompareTag("parede") ||
            collision.collider.CompareTag("Inimigo") ||
            collision.collider.CompareTag("biginimigo") ||
            collision.collider.CompareTag("chefe"))
        {
            Destroy(gameObject);
        }
        else
        {
            // Ignora colisão (não destrói a bala)
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
