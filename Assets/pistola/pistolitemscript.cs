using UnityEngine;

public class PistolItem : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 15f;           // velocidade inicial
    public float deceleration = 2.5f;   // taxa de desaceleração
    public string pistolPrefabName = "pistol"; // nome do prefab da pistola na pasta Resources

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Calcula direção do mouse
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        // Aplica velocidade inicial
        rb.linearVelocity = direction * speed;
    }

    void FixedUpdate()
    {
        // Desacelera gradualmente
        if (rb.linearVelocity.magnitude > 0)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {
           
            PlayerMovementScript playerScript = collision.gameObject.GetComponent<PlayerMovementScript>();

           
            if (playerScript != null && playerScript.segurandoArma)
                return;

           
            GameObject pistolPrefab = Resources.Load<GameObject>(pistolPrefabName);

            if (pistolPrefab != null)
            {
               
                GameObject novaPistol = Instantiate(pistolPrefab, transform.position, Quaternion.identity);
                pistolscript pistolScript = novaPistol.GetComponent<pistolscript>();

                if (pistolScript != null)
                    pistolScript.player = collision.gameObject.transform;
            }
            else
            {
                Debug.LogError("Prefab '" + pistolPrefabName + "' não encontrado em Resources!");
            }

            playerScript.segurandoArma = true;
            Destroy(gameObject);
        }
    }
}
