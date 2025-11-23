using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public static bool gameOver = false;
    public GameObject telaEscura;
    public GameObject textoGameOver;
    public float delayMenu = 2f;

    public void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0f;
        telaEscura.SetActive(true);
        textoGameOver.SetActive(true);
        StartCoroutine(VoltarMenu());
    }

    System.Collections.IEnumerator VoltarMenu()
    {
        yield return new WaitForSecondsRealtime(delayMenu);
        gameOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("menuprincipalscene");
    }
}
