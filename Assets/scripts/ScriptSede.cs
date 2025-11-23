using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ScriptSede : MonoBehaviour
{
    public int tims;
    public Slider slider;

    public ScriptVida vida;       
    public int danoPorTick = 1;   
    private int timerDano = 0;
    public int intervaloDano = 120; 

    void Start()
    {
        tims = 0;
        slider.maxValue = 8;
    }

    void FixedUpdate()
    {
        tims++;

        if (tims == 400)
        {
            slider.value -= 1;
            tims = 0;
        }

      
        if (slider.value <= 0)
        {
            timerDano++;

            if (timerDano >= intervaloDano)
            {
                vida.setHealth((int)vida.slider.value - danoPorTick);
                timerDano = 0;
            }
        }
        else
        {
            timerDano = 0;
        }
    }

    public void bebeu()
    {
        if (slider.value + 4 > slider.maxValue)
        {
            slider.value = slider.maxValue;
            tims = -300;
            tims -= 200;
        }
        else
        {
            slider.value += 4;
            tims = -100;
        }
    }
}
