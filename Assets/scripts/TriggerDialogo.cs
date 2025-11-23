using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDialogo : MonoBehaviour
{
    public DialogoTriggerado dialogo;
    private bool jaAtivou = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (jaAtivou) return;
        if (!other.CompareTag("Player")) return;

        if (dialogo == null)
        {
            Debug.LogError("Faltando referência do DialogoTriggerado!");
            return;
        }

        jaAtivou = true;
        dialogo.IniciarDialogo();
    }
}
