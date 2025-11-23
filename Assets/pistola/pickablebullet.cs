using UnityEngine;

public class pickablebullet : MonoBehaviour
{
    public int quantidade = 1; // quantas balas adiciona

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se colidiu com o Player
        PlayerMovementScript player = other.GetComponent<PlayerMovementScript>();
        if (player != null)
        {
            // Aumenta a munição de pistola
            player.balasPistola += quantidade;

            // Destroi o item
            Destroy(gameObject);
        }
    }
}
