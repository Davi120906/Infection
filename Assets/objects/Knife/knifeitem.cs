using UnityEngine;

public class KnifeItem : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 7.5f;
    public float deceleration = 3f;
    public string knifePrefabName = "Knife";

    public bool foiJogada = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (!foiJogada)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        rb.linearVelocity = direction * speed;
    }

    void FixedUpdate()
    {
        if (foiJogada)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject knifePrefab = Resources.Load<GameObject>(knifePrefabName);

            if (knifePrefab != null)
            {
                GameObject novaKnife = Instantiate(knifePrefab, transform.position, Quaternion.identity);
                KnifeScript knfs = novaKnife.GetComponent<KnifeScript>();
                if (knfs != null)
                    knfs.player = collision.gameObject.transform;
            }

            Destroy(gameObject);
        }
    }
}
