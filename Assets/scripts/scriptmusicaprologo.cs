using UnityEngine;

public class scriptmusicaprologo : MonoBehaviour
{
    void Start()
    {
        AudioSource a = GetComponent<AudioSource>();
        AudioClip musica = Resources.Load<AudioClip>("audio/musicaprologo");
        a.clip = musica;
        a.Play();
    }
}
