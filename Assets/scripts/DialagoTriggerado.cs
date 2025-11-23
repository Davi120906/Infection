using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogoTriggerado : MonoBehaviour
{
    public GameObject caixaDialogo;
    public TMP_Text textoDialogo;
    public GameObject antidotoui;

    public Image imagemP1;
    public Image imagemP2;

    public float velocidadeTexto = 0.06f;
    public float velocidadeBoca = 0.12f;

    public string[] falasP1;
    public string[] falasP2;

    private int indiceP1 = 0;
    private int indiceP2 = 0;

    private bool dialogoAtivo = false;
    private bool escrevendo = false;
    private bool vezDoP1 = true;

    private Sprite p1_bocaFechada;
    private Sprite p1_bocaAberta;
    private Sprite p2_bocaFechada;
    private Sprite p2_bocaAberta;

    private float timerBoca = 0f;

    private AudioSource audioSource;
    private AudioClip somFala;

    private void Awake()
    {
        p1_bocaFechada = Resources.Load<Sprite>("cabecasfala/brandem1");
        p1_bocaAberta = Resources.Load<Sprite>("cabecasfala/brandem2");

        p2_bocaFechada = Resources.Load<Sprite>("cabecasfala/lp1");
        p2_bocaAberta = Resources.Load<Sprite>("cabecasfala/lp2");

        somFala = Resources.Load<AudioClip>("audio/barulhofala");

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        caixaDialogo.SetActive(false);
        imagemP1.gameObject.SetActive(false);
        imagemP2.gameObject.SetActive(false);
    }

    public void IniciarDialogo()
    {
        dialogoAtivo = true;
        caixaDialogo.SetActive(true);
        Time.timeScale = 0f;

        IniciarProximaFala();
    }

    void IniciarProximaFala()
    {
        string frase = "";

        if (vezDoP1)
        {
            if (indiceP1 >= falasP1.Length) { FecharDialogo(); return; }

            imagemP1.gameObject.SetActive(true);
            imagemP2.gameObject.SetActive(false);

            imagemP1.sprite = p1_bocaFechada;
            frase = falasP1[indiceP1];
        }
        else
        {
            if (indiceP2 >= falasP2.Length) { FecharDialogo(); return; }

            imagemP2.gameObject.SetActive(true);
            imagemP1.gameObject.SetActive(false);

            imagemP2.sprite = p2_bocaFechada;
            frase = falasP2[indiceP2];
        }

        StartCoroutine(DigitarTexto(frase));
    }

    IEnumerator DigitarTexto(string frase)
    {
        escrevendo = true;
        textoDialogo.text = "";
        timerBoca = 0f;

        if (somFala != null)
        {
            audioSource.clip = somFala;
            audioSource.Play();
        }

        foreach (char c in frase.ToCharArray())
        {
            textoDialogo.text += c;

            timerBoca += velocidadeTexto;
            if (timerBoca >= velocidadeBoca)
            {
                if (vezDoP1)
                    imagemP1.sprite = (imagemP1.sprite == p1_bocaFechada) ? p1_bocaAberta : p1_bocaFechada;
                else
                    imagemP2.sprite = (imagemP2.sprite == p2_bocaFechada) ? p2_bocaAberta : p2_bocaFechada;

                timerBoca = 0f;
            }

            yield return new WaitForSecondsRealtime(velocidadeTexto);
        }

        escrevendo = false;

        if (audioSource.isPlaying) audioSource.Stop();

        if (vezDoP1) imagemP1.sprite = p1_bocaFechada;
        else imagemP2.sprite = p2_bocaFechada;
    }

    void Update()
    {
        if (!dialogoAtivo) return;

        if (Input.GetKeyDown(KeyCode.Space) && !escrevendo)
        {
            if (vezDoP1) indiceP1++;
            else indiceP2++;

            vezDoP1 = !vezDoP1;

            IniciarProximaFala();
        }
    }

    public void FecharDialogo()
    {
        dialogoAtivo = false;
        Time.timeScale = 1f;
        antidotoui.SetActive(true);
        caixaDialogo.SetActive(false);
        imagemP1.gameObject.SetActive(false);
        imagemP2.gameObject.SetActive(false);


        if (audioSource.isPlaying) audioSource.Stop();
    }
}
