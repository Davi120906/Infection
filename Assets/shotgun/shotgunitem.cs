using UnityEngine;

public class shotgunitem : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 15f;           // Velocidade inicial do arremesso
    public float deceleration = 2.5f;   // Taxa de desaceleração
    public string shotgunPrefabName = "shotgun"; // Nome do prefab da arma que o player segura (em Resources)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Calcula direção do mouse no momento do lançamento
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        // Aplica velocidade inicial
        rb.linearVelocity = direction * speed;
    }

    void FixedUpdate()
    {
        // Desacelera gradualmente o item
        if (rb.linearVelocity.magnitude > 0)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Se colidir com o player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Carrega o prefab da shotgun que o player segura
            GameObject shotgunPrefab = Resources.Load<GameObject>(shotgunPrefabName);

            if (shotgunPrefab != null)
            {
                // Instancia a shotgun no jogador
                shotgunPrefab = Instantiate(shotgunPrefab, transform.position, Quaternion.identity);
                shotgunmovementscript sgScript = shotgunPrefab.GetComponent<shotgunmovementscript>();
                if (sgScript != null)
                {
                    sgScript.player = collision.gameObject.transform;
                }
            }
            else
            {
                Debug.LogError("Prefab '" + shotgunPrefabName + "' não encontrado em Resources!");
            }

            // Destroi o item arremessado
            Destroy(gameObject);
        }
    }
}
