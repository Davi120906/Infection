using UnityEngine;

public class pickablebullet : MonoBehaviour
{
    public int quantidade = 1;

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
            player.balasPistola += quantidade;

            AudioSource.PlayClipAtPoint(somRecarga, transform.position);

            Destroy(gameObject);
        }
    }
}
