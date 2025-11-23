using Unity.Mathematics;
using UnityEngine;

public class knifeanimationscript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public Sprite idlespr;
    public int i = 0;
    public int cooldown = 0;
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sprites = new Sprite[4];
        sprites[0] = Resources.Load <Sprite>("spf_faca1");
        sprites[1] = Resources.Load<Sprite>("spr_faca2");
        sprites[2] = Resources.Load<Sprite>("spr_faca3");
        sprites[3] = Resources.Load<Sprite>("spr_faca4");
        idlespr = Resources.Load<Sprite>("spr_facaidle");
    }
    public bool animarAtaque()
    {
        if (cooldown == 0)
        {
            if (i + 1 < sprites.Length)
            {
                spriteRenderer.sprite = sprites[i];
                i++;
                
                cooldown = 60;
                return true;
            }
            else
            {
                i = 0;
                spriteRenderer.sprite = idlespr;
                return false;
            }
        }
        else
        {
            cooldown--;
            return true;
        }
    }
}
