using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;  // necessário para trocar de cena

public class CutsceneTypewriterAdvance : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public float typingSpeed = 0.03f;

    [TextArea(3, 10)]
    public string[] pages;

    public string audioName = "teclabarulho";
    private AudioSource typeSound;

    public string nextSceneName = "SampleScene";
    public float fadeOutDuration = 1.5f;

    void Start()
    {
        LoadTypingSound();
        StartCoroutine(PlayCutscene());
    }

    void LoadTypingSound()
    {
        typeSound = gameObject.AddComponent<AudioSource>();

        AudioClip clip = Resources.Load<AudioClip>("Audio/" + audioName);

        if (clip == null)
        {
            Debug.LogError("❌ Áudio não encontrado em Resources/Audio/" + audioName);
            return;
        }

        typeSound.clip = clip;
        typeSound.playOnAwake = false;
        typeSound.loop = true;
    }

    IEnumerator PlayCutscene()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            yield return StartCoroutine(TypePage(pages[i]));

            // Última página → faz fade e troca de cena
            if (i == pages.Length - 1)
            {
                yield return StartCoroutine(FadeOutText());
                SceneManager.LoadScene(nextSceneName);
                yield break;
            }

            // páginas normais → espera tecla
            yield return StartCoroutine(WaitForNextPage());
            textUI.text = "";
        }
    }

    IEnumerator TypePage(string pageText)
    {
        textUI.text = "";

        // inicia o áudio contínuo
        if (typeSound != null && !typeSound.isPlaying)
            typeSound.Play();

        foreach (char c in pageText)
        {
            textUI.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        // terminou a página → parar som
        typeSound.Stop();
    }

    IEnumerator WaitForNextPage()
    {
        while (!Input.GetKeyDown(KeyCode.Space))
            yield return null;
    }

    IEnumerator FadeOutText()
    {
        float t = 0f;
        Color original = textUI.color;

        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeOutDuration);
            textUI.color = new Color(original.r, original.g, original.b, alpha);
            yield return null;
        }

        textUI.color = new Color(original.r, original.g, original.b, 0f);
    }
}
