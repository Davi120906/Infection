using UnityEngine;

public class shotgunmovementscript : MonoBehaviour
{
    public Transform player;
    private Camera mainCamera;
    private GameObject shotgunPrefab;
    private GameObject bulletPrefab;

    public float bulletSpeed = 12f;
    public float spawnOffset = 100f;
    public float bulletLifetime = 3f;
    public float shootCooldown = 0.8f;
    public float spreadAngle = 8f;

    private float lastShootTime = 0f;

    private AudioSource audioSource;
    private AudioClip somTiroDoze;

    void Start()
    {
        mainCamera = Camera.main;
        shotgunPrefab = Resources.Load<GameObject>("shotgunitem");
        bulletPrefab = Resources.Load<GameObject>("pistolbullet");
        somTiroDoze = Resources.Load<AudioClip>("audio/tirodoze");
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = 0.5f;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        transform.position = player.position;
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 direction = (mousePos - transform.position).normalized;
        float angleDeg = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float displayAngle = angleDeg - 82f;
        transform.rotation = Quaternion.Euler(0f, 0f, displayAngle + 80f);

        if (Input.GetMouseButtonDown(0))
            TryShoot(direction, angleDeg);

        if (Input.GetMouseButtonDown(1))
            ThrowShotgun();
    }

    void TryShoot(Vector3 direction, float angleDeg)
    {
        PlayerMovementScript playerScript = player.GetComponent<PlayerMovementScript>();
        if (playerScript == null || playerScript.balaDoze <= 0) return;
        if (Time.time - lastShootTime < shootCooldown) return;

        lastShootTime = Time.time;
        playerScript.balaDoze--;

        if (somTiroDoze != null)
            audioSource.PlayOneShot(somTiroDoze);

        float[] offsets = { 0f, -spreadAngle, spreadAngle };
        foreach (float offset in offsets)
        {
            Vector3 shotDir = Quaternion.Euler(0f, 0f, offset) * direction;
            float shotAngle = angleDeg + offset;
            Shoot(shotDir, shotAngle);
        }
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

    void ThrowShotgun()
    {
        if (shotgunPrefab != null)
            Instantiate(shotgunPrefab, transform.position, transform.rotation);

        PlayerMovementScript playerScript = player.GetComponent<PlayerMovementScript>();
        if (playerScript != null)
            playerScript.segurandoArma = false;

        Destroy(gameObject);
    }
}
