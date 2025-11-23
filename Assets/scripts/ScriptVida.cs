using UnityEngine;
using UnityEngine.UI;

public class ScriptVida : MonoBehaviour
{
    public Slider slider;
    public GameOverController gameOver;

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void setHealth(int health)
    {
        slider.value = health;

        if (slider.value <= 0)
            gameOver.GameOver();
    }
}
