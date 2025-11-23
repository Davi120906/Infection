using UnityEngine;

public class ScriptGarrafa : MonoBehaviour
{
    public Rigidbody2D rb;
    public ScriptSede sede;

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            sede.bebeu();
            Destroy(gameObject);
        }
    }
}
