using UnityEngine;
using TMPro;

public class AmmoUI_TMP : MonoBehaviour
{
    public PlayerMovementScript player;
    public TextMeshProUGUI ammoText;

    void Start()
    {
        if (player == null)
            player = FindFirstObjectByType<PlayerMovementScript>();

        if (ammoText != null)
        {
            ammoText.fontSize = 22; // tamanho menor
            ammoText.alignment = TextAlignmentOptions.TopRight;

            // tenta carregar uma fonte 8-bit chamada "PixelFont"
            TMP_FontAsset pixelFont = Resources.Load<TMP_FontAsset>("PixelFont");
            if (pixelFont != null)
                ammoText.font = pixelFont;
        }
    }

    void Update()
    {
        if (player == null || ammoText == null) return;

        ammoText.text =
            "Pistola: " + player.balasPistola + "\n" +
            "Doze: " + player.balaDoze;
    }
}
