using UnityEngine;

public class SeringaItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovementScript player = other.GetComponent<PlayerMovementScript>();
            if (player != null)
            {
                player.seringascoletadas += 1;
                Destroy(gameObject);
            }
        }
    }
}
