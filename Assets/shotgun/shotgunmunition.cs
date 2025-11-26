using UnityEngine;

public class shotgunmunition : MonoBehaviour
{
    public int quantidade = 3;

    private AudioClip somRecarga;

    private void Start()
    {
        somRecarga = Resources.Load<AudioClip>("audio/recarga");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovementScript player = other.GetComponent<PlayerMovementScript>();

        if (player != null)
        {
            player.balaDoze += quantidade;

            AudioSource.PlayClipAtPoint(somRecarga, transform.position);

            Destroy(gameObject);
        }
    }
}
