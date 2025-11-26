using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour
{
    public float moveSpeed;
    public bool segurandoArma;
    public Rigidbody2D rb;
    public int vidaMaxima = 20;
    public int vidaAtual;
    public int danoPorColisao = 3;
    public float tempoInvencivel = 3f;
    public int balasPistola = 3;
    public int balaDoze = 3;
    public int seringascoletadas = 0;

    private Vector2 moveDirections;
    public ScriptVida scriptVida;
    private bool invencivel = false;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private AudioClip audioDano;
    private void Start()
    {
        segurandoArma = true;
        vidaAtual = vidaMaxima;
        scriptVida.setMaxHealth(vidaMaxima);
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioDano = Resources.Load<AudioClip>("audio/playerdano");
        
    }

    void Update()
    {
        if(vidaAtual > 0)
        {
            recebeInput();
        }
    }

    void tomarDano(int dano)
    {
        if (invencivel) return;

        vidaAtual -= dano;
        scriptVida.setHealth(vidaAtual);
        audioSource.PlayOneShot(audioDano);
        StartCoroutine(InvencivelTemp());
        
    }

    IEnumerator InvencivelTemp()
    {
        invencivel = true;
        float tempoPiscando = 1f / 4f;
        float tempoTotal = 0f;

        while (tempoTotal < tempoInvencivel)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(tempoPiscando);
            tempoTotal += tempoPiscando;
        }

        spriteRenderer.enabled = true;
        invencivel = false;
    }

    void FixedUpdate()
    {
        OnAnimatorMove();
    }

    void recebeInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirections = new Vector2(moveX, moveY).normalized;
    }

    private void OnAnimatorMove()
    {
        rb.linearVelocity = new Vector2(moveDirections.x * moveSpeed, moveDirections.y * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            tomarDano(danoPorColisao);
            
        }
        if (collision.gameObject.CompareTag("biginimigo"))
        {
            tomarDano(danoPorColisao +2 );

        }

    }
}
