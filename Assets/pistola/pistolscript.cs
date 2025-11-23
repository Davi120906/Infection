using UnityEngine;

public class pistolscript : MonoBehaviour
{
    public Transform player;
    private Camera mainCamera;
    private GameObject pistolPrefab;
    private GameObject bulletPrefab;

    public float bulletSpeed = 15f;
    public float spawnOffset = 1.1f;
    public float bulletLifetime = 3f;
    public float shootCooldown = 0.25f;
    private float lastShootTime = 0f;

    private AudioSource audioSource;
    private AudioClip somTiro;

    void Start()
    {
        mainCamera = Camera.main;
        pistolPrefab = Resources.Load<GameObject>("PistolItem");
        bulletPrefab = Resources.Load<GameObject>("pistolbullet");

        somTiro = Resources.Load<AudioClip>("audio/tiropistola");
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = 0.5f;
    }

    void Update()
    {
        if (GerenciadorPausa.pausado) return;

        transform.position = player.position;
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector3 direction = (mousePos - transform.position).normalized;
        float angleDeg = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angleDeg - 2f);

        if (Input.GetMouseButtonDown(0)) TryShoot(direction, angleDeg);
        if (Input.GetMouseButtonDown(1)) JogaArma();
    }

    void TryShoot(Vector3 direction, float angleDeg)
    {
        PlayerMovementScript playerScript = player.GetComponent<PlayerMovementScript>();
        if (playerScript == null || playerScript.balasPistola <= 0) return;

        if (Time.time - lastShootTime < shootCooldown) return;
        lastShootTime = Time.time;

        playerScript.balasPistola--;

        if (somTiro != null)
            audioSource.PlayOneShot(somTiro);

        Shoot(direction, angleDeg);
    }

    void Shoot(Vector3 direction, float angleDeg)
    {
        if (bulletPrefab == null) return;

        Vector3 spawnPos = transform.position + (Vector3)(direction * spawnOffset);
        Quaternion rot = Quaternion.Euler(0f, 0f, angleDeg - 90f);

        GameObject bullet = Instantiate(bulletPrefab, spawnPos, rot);

        Collider2D playerCol = player.GetComponent<Collider2D>();
        Collider2D bulletCol = bullet.GetComponent<Collider2D>();
        if (playerCol != null && bulletCol != null)
            Physics2D.IgnoreCollision(bulletCol, playerCol);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = direction * bulletSpeed;

        Destroy(bullet, bulletLifetime);
    }

    void JogaArma()
    {
        if (pistolPrefab != null)
            Instantiate(pistolPrefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
