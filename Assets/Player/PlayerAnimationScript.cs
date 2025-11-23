using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    // Sprites por direção
    public Sprite[] downSprites;  // frames andando para baixo
    public Sprite[] rightSprites; // frames andando para direita
    public Sprite[] upSprites;    // frames andando para cima

    public float animationSpeed = 0.2f; // tempo entre frames

    private Vector2 moveDirections;
    private float timer;
    private int currentFrame;

    // Direção atual (para saber qual idle mostrar)
    private Sprite[] currentDirectionSprites;
    private bool facingRight = true;

    void Update()
    {
        // Recebe input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirections = new Vector2(moveX, moveY).normalized;

        // Escolhe direção atual
        if (moveDirections.y > 0)
        {
            currentDirectionSprites = upSprites;
            facingRight = true; // não espelha na vertical
        }
        else if (moveDirections.y < 0)
        {
            currentDirectionSprites = downSprites;
            facingRight = true;
        }
        else if (moveDirections.x != 0)
        {
            currentDirectionSprites = rightSprites;
            facingRight = moveDirections.x > 0;
        }

        // Atualiza flipX se estiver horizontal
        if (currentDirectionSprites == rightSprites)
        {
            spriteRenderer.flipX = !facingRight;
        }

        // Atualiza animação por frame se estiver se movendo
        if (moveDirections != Vector2.zero)
        {
            timer += Time.deltaTime;
            if (timer >= animationSpeed)
            {
                timer = 0f;
                currentFrame = (currentFrame + 1) % currentDirectionSprites.Length;
                spriteRenderer.sprite = currentDirectionSprites[currentFrame];
            }
        }
        else
        {
 
            currentFrame = 0;
            spriteRenderer.sprite = currentDirectionSprites[currentFrame];
        }
    }
}
