using UnityEngine;

public class scripptmusicacidade : MonoBehaviour
{
    void Start()
    {
        AudioSource a = GetComponent<AudioSource>();
        AudioClip musica = Resources.Load<AudioClip>("audio/musicacidade");
        a.clip = musica;
        a.loop = true;
        a.Play();
    }
}
