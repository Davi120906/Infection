using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour
{
    public float moveSpeed;
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

    private void Start()
    {
        vidaAtual = vidaMaxima;
        scriptVida.setMaxHealth(vidaMaxima);
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Inimigo"))
        {
            tomarDano(danoPorColisao);
        }
    }
}
