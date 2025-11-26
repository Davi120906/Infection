using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class zombiemovement : MonoBehaviour
{
    public Transform player;
    public float wanderRadius = 10f;
    public float visionRadius = 3f;
    public float speed = 1.5f;
    public float waitTime = 2f;
    public float invencivelDuracao = 1f;
    public float forcaRecuo = 1.5f;

    private Vector2 spawnPoint;
    private Vector2 targetPosition;
    private bool isWandering = false;
    private float scaleFactor = 4.7f;
    private Rigidbody2D rb;

    private int life = 5;
    private bool invencivel = false;
    private SpriteRenderer sr;

    private Dictionary<string, Sprite[]> anims = new Dictionary<string, Sprite[]>();
    private float animTimer = 0f;
    private int animIndex = 0;
    private float animSpeed = 0.15f;

    private string currentAnim = "down";

    private AudioSource audioSource;
    private AudioClip barulhoZumbi;
    private AudioClip barulhoDano;
    private float audioCooldown = 0f;
    private float audioInterval = 4f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        spawnPoint = transform.position;

        wanderRadius = 3f * scaleFactor;
        visionRadius = 2f * scaleFactor;
        speed = 1.5f * scaleFactor;
        waitTime = 2f;
        invencivelDuracao = 1f;
        forcaRecuo = 1.5f;
        life = 5;

        LoadAnimations();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.5f;

        barulhoZumbi = Resources.Load<AudioClip>("audio/zumbibarulho");
        barulhoDano = Resources.Load<AudioClip>("audio/zumbidano");

        SetNewTarget();
    }

    void LoadAnimations()
    {
        anims["down"] = new Sprite[]
        {
            Resources.Load<Sprite>("zumbi/spr_zombiedown1"),
            Resources.Load<Sprite>("zumbi/spr_zombiedown2"),
            Resources.Load<Sprite>("zumbi/spr_zombiedown3")
        };

        anims["up"] = new Sprite[]
        {
            Resources.Load<Sprite>("zumbi/spr_zombieup1"),
            Resources.Load<Sprite>("zumbi/spr_zombieup2"),
            Resources.Load<Sprite>("zumbi/spr_zombieup3")
        };

        anims["left"] = new Sprite[]
        {
            Resources.Load<Sprite>("zumbi/spr_zombieleft1"),
            Resources.Load<Sprite>("zumbi/spr_zombieleft2"),
            Resources.Load<Sprite>("zumbi/spr_zombieleft3")
        };
    }

    void Update()
    {
        if (invencivel) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (audioCooldown > 0)
            audioCooldown -= Time.deltaTime;

        if (distanceToPlayer <= visionRadius)
        {
            MoveTowardPlayerAxisBased();

            if (audioCooldown <= 0f)
            {
                audioSource.PlayOneShot(barulhoZumbi, 0.5f);
                audioCooldown = audioInterval;
            }
        }
        else
        {
            WanderBehaviour();
        }
    }

    void MoveTowardPlayerAxisBased()
    {
        float distX = player.position.x - transform.position.x;
        float distY = player.position.y - transform.position.y;

        // Movimentação suave e sem acelerar na diagonal
        Vector2 movement = new Vector2(distX, distY).normalized;

        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

        UpdateDirectionAnimation(distX, distY);
    }

    void WanderBehaviour()
    {
        if (!isWandering)
            StartCoroutine(Wander());
        else
        {
            float distX = targetPosition.x - transform.position.x;
            float distY = targetPosition.y - transform.position.y;

            float dirX = Mathf.Sign(distX);
            float dirY = Mathf.Sign(distY);

            Vector2 movement = new Vector2(dirX, dirY).normalized;

            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);

            UpdateDirectionAnimation(distX, distY);

            if (Vector2.Distance(transform.position, targetPosition) < 0.3f * scaleFactor)
                isWandering = false;
        }
    }

    IEnumerator Wander()
    {
        isWandering = true;
        yield return new WaitForSeconds(waitTime);
        SetNewTarget();
    }

    void SetNewTarget()
    {
        Vector2 randomCircle = Random.insideUnitCircle * wanderRadius;
        targetPosition = spawnPoint + randomCircle;
    }

    void UpdateDirectionAnimation(float distX, float distY)
    {
        float absX = Mathf.Abs(distX);
        float absY = Mathf.Abs(distY);

        if (absX > absY)
        {
            currentAnim = "left";
            sr.flipX = distX > 0;
        }
        else
        {
            currentAnim = (distY > 0) ? "up" : "down";
            sr.flipX = false;
        }

        PlayAnimation();
    }

    void PlayAnimation()
    {
        animTimer += Time.deltaTime;

        if (animTimer >= animSpeed)
        {
            animTimer = 0f;
            animIndex = (animIndex + 1) % anims[currentAnim].Length;
            sr.sprite = anims[currentAnim][animIndex];
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (!other.collider.CompareTag("Parede"))
        {
            Collider2D zCol = GetComponent<Collider2D>();
            Collider2D oCol = other.collider;

            if (zCol != null && oCol != null)
                Physics2D.IgnoreCollision(zCol, oCol, true);
        }


        if (other.collider.name.Contains("Knife"))
        {
            Collider2D zCol = GetComponent<Collider2D>();
            Collider2D kCol = other.collider;

            if (zCol != null && kCol != null)
                Physics2D.IgnoreCollision(zCol, kCol, true);

            return;
        }

        if (invencivel) return;

        if (other.collider.name.Contains("pistolbullet"))
            tomarDano(3, other.transform.position);


        if (other.collider.CompareTag("arma jogada"))
        {
            Rigidbody2D armaRB = other.collider.attachedRigidbody;

            if (armaRB != null && armaRB.linearVelocity.magnitude > 0.1f)
            {
                tomarDano(2, other.transform.position);
            }
        }
    }
    void tomarDano(int dano, Vector3 origem)
    {
        life -= dano;

        if (barulhoDano != null)
            audioSource.PlayOneShot(barulhoDano, 0.5f);

        Vector2 knock = ((Vector2)transform.position - (Vector2)origem).normalized;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(knock * forcaRecuo, ForceMode2D.Impulse);

        if (life <= 0)
        {
            StartCoroutine(MorrerDepoisDoKnock());
            return;
        }

        StartCoroutine(InvencivelCoroutine());
    }

    IEnumerator MorrerDepoisDoKnock()
    {
        yield return new WaitForSeconds(0.12f);
        Destroy(gameObject);
    }

    IEnumerator InvencivelCoroutine()
    {
        invencivel = true;
        float timer = 0f;

        while (timer < invencivelDuracao)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }

        sr.enabled = true;
        invencivel = false;
    }
}
