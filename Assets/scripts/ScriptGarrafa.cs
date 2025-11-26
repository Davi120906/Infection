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
            TocarSomBebeu();
            Destroy(gameObject);
        }
    }

    void TocarSomBebeu()
    {
        AudioClip som = Resources.Load<AudioClip>("audio/bebeu");

     

        GameObject audioObj = new GameObject("SomBebeuTemp");
        AudioSource source = audioObj.AddComponent<AudioSource>();
        source.clip = som;
        source.Play();

        Destroy(audioObj, som.length);
    }
}
