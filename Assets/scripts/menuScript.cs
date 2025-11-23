using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuPrincipal : MonoBehaviour
{
    public float tempoEspera = 2f;
    AudioSource a;
    AudioClip som;
    AudioClip musicaMenu;

    void Start()
    {
        a = GetComponent<AudioSource>();
        som = Resources.Load<AudioClip>("audio/botaoapertado");
        musicaMenu = Resources.Load<AudioClip>("audio/musicamenu");
        a.clip = musicaMenu;
        a.loop = true;
        a.Play();
    }

    public void Jogar()
    {
        a.PlayOneShot(som);
        StartCoroutine(CarregarCena());
    }

    IEnumerator CarregarCena()
    {
        yield return new WaitForSeconds(tempoEspera);
        SceneManager.LoadScene("prologoscene");
    }

    public void Sair()
    {
        a.PlayOneShot(som);
        Application.Quit();
    }
}
