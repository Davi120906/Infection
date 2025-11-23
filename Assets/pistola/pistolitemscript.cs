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
        // Verifica se colidiu com o player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Carrega o prefab da pistola
            GameObject pistolPrefab = Resources.Load<GameObject>(pistolPrefabName);

            if (pistolPrefab != null)
            {
                // Instancia a pistola na posição atual
                pistolPrefab = Instantiate(pistolPrefab, transform.position, Quaternion.identity);
                pistolscript pistolScript = pistolPrefab.GetComponent<pistolscript>();
                if (pistolScript != null)
                {
                    pistolScript.player = collision.gameObject.transform;
                }
            }
            else
            {
                Debug.LogError("Prefab '" + pistolPrefabName + "' não encontrado em Resources!");
            }

            // Destroi o item arremessado
            Destroy(gameObject);
        }
    }
}
