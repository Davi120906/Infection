using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogoInicial : MonoBehaviour
{
    public GameObject caixaDialogo;
    public TMP_Text textoDialogo;
    public Image imagemRosto;
    public float velocidadeTexto = 0.06f;
    public float velocidadeBoca = 0.12f;

    public string[] falas;

    private int indiceFala = 0;
    private bool dialogoAtivo = false;
    private bool escrevendo = false;

    private Sprite bocaFechada;
    private Sprite bocaAberta;

    private float timerBoca = 0f;

    private AudioSource audioSource;
    private AudioClip somFala;

    private void Awake()
    {
        bocaFechada = Resources.Load<Sprite>("cabecasfala/brandem1");
        bocaAberta = Resources.Load<Sprite>("cabecasfala/brandem2");

        somFala = Resources.Load<AudioClip>("audio/barulhofala");

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        IniciarDialogo();
    }

    public void IniciarDialogo()
    {
        if (falas.Length == 0) return;

        indiceFala = 0;
        dialogoAtivo = true;

        caixaDialogo.SetActive(true);
        imagemRosto.enabled = true;
        imagemRosto.sprite = bocaFechada;

        Time.timeScale = 0f;

        StartCoroutine(DigitarTexto(falas[indiceFala]));
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

        foreach (char letra in frase.ToCharArray())
        {
            textoDialogo.text += letra;

            timerBoca += velocidadeTexto;
            if (timerBoca >= velocidadeBoca)
            {
                imagemRosto.sprite = (imagemRosto.sprite == bocaFechada) ? bocaAberta : bocaFechada;
                timerBoca = 0f;
            }

            yield return new WaitForSecondsRealtime(velocidadeTexto);
        }

        escrevendo = false;

        if (audioSource.isPlaying)
            audioSource.Stop();

        imagemRosto.sprite = bocaFechada;
    }

    void Update()
    {
        if (!dialogoAtivo) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (escrevendo) return;

            AvancarFala();
        }
    }

    void AvancarFala()
    {
        indiceFala++;

        if (indiceFala >= falas.Length)
        {
            FecharDialogo();
            return;
        }

        StartCoroutine(DigitarTexto(falas[indiceFala]));
    }

    public void FecharDialogo()
    {
        dialogoAtivo = false;
        Time.timeScale = 1f;

        caixaDialogo.SetActive(false);
        imagemRosto.enabled = false;

        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
