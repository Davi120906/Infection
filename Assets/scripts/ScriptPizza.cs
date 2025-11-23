using UnityEngine;

public class ScriptPizza : MonoBehaviour
{
    public Rigidbody2D rb;
    public ScriptFome fome;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se colidiu com o player
        if (collision.gameObject.CompareTag("Player"))
        {
            fome.comeu();
            Destroy(gameObject);
        }
    }
}
