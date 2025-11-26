using UnityEngine;

public class ScriptPizza : MonoBehaviour
{
    public Rigidbody2D rb;
    public ScriptFome fome;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fome.comeu();
            TocarSomComeu();
            Destroy(gameObject);
        }
    }

    void TocarSomComeu()
    {
        AudioClip som = Resources.Load<AudioClip>("audio/comeu");

    

        GameObject audioObj = new GameObject("SomComeuTemp");
        AudioSource source = audioObj.AddComponent<AudioSource>();
        source.clip = som;
        source.Play();

        Destroy(audioObj, som.length);
    }
}
