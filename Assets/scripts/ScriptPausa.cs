using UnityEngine;

public class GerenciadorPausa : MonoBehaviour
{
    public static bool pausado = false;
    public GameObject menuPausa; // arraste o menu de pausa no inspetor

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausado)
                Continuar();
            else
                Pausar();
        }
    }

    public void Pausar()
    {
        Time.timeScale = 0f;          // congela o tempo do jogo
        pausado = true;               // marca como pausado
        if (menuPausa != null)
            menuPausa.SetActive(true); // mostra menu
    }

    public void Continuar()
    {
        Time.timeScale = 1f;          // volta o tempo ao normal
        pausado = false;              // marca como despausado
        if (menuPausa != null)
            menuPausa.SetActive(false); // esconde menu
    }
}
    