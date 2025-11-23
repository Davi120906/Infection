using UnityEngine;

public class shotgunmunition : MonoBehaviour
{
    public int quantidade = 3; // quantas balas adiciona

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Tenta pegar o script do player
        PlayerMovementScript player = other.GetComponent<PlayerMovementScript>();
        if (player != null)
        {
            // Aumenta a munição da 12
            player.balaDoze += quantidade;

            // Destrói o item
            Destroy(gameObject);
        }
    }
}
