using UnityEngine;

public class shotgunitem : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 15f;
    public float deceleration = 2.5f;
    public string shotgunPrefabName = "shotgun";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector2 direction = (mouseWorldPos - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > 0)
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovementScript playerScript = collision.gameObject.GetComponent<PlayerMovementScript>();
            if (playerScript != null && playerScript.segurandoArma)
                return;

            GameObject shotgunPrefab = Resources.Load<GameObject>(shotgunPrefabName);

            if (shotgunPrefab != null)
            {
                GameObject novaShotgun = Instantiate(shotgunPrefab, transform.position, Quaternion.identity);
                shotgunmovementscript sgScript = novaShotgun.GetComponent<shotgunmovementscript>();
                if (sgScript != null)
                    sgScript.player = collision.gameObject.transform;
            }
            playerScript.segurandoArma = true;
            Destroy(gameObject);
        }
    }
}
