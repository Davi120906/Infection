using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SeringasUI : MonoBehaviour
{
    public PlayerMovementScript player;
    public TMP_Text textoSeringas;
    public int maxSeringas = 8;
    public string cenaFinal = "CenaFinal";

    void Update()
    {
        textoSeringas.text = player.seringascoletadas + "/" + maxSeringas;
        if (player.seringascoletadas >= maxSeringas)
        {
            SceneManager.LoadScene(cenaFinal);
        }
    }
}
