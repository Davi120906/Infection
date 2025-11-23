using System;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    public Transform player;
    private Camera mainCamera;
    private GameObject knifePrefab;
    public bool ataque;
    private knifeanimationscript animation;
    private bool privez = false;
    public GameObject hitbox;

    private AudioSource audioSource;
    private AudioClip facaSom;

    void Start()
    {
        knifePrefab = Resources.Load<GameObject>("knifeitem");
        mainCamera = Camera.main;
        ataque = false;
        animation = GetComponent<knifeanimationscript>();

        audioSource = gameObject.AddComponent<AudioSource>();
        facaSom = Resources.Load<AudioClip>("audio/facasom");
    }

    void Update()
    {
        if (Time.timeScale == 0) return;

        transform.position = player.position;
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector3 direction = mousePos - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90f;

        if (!ataque)
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (Input.GetMouseButtonDown(1))
        {
            GameObject k = Instantiate(knifePrefab, transform.position, transform.rotation);
            KnifeItem ki = k.GetComponent<KnifeItem>();
           if (ki != null) ki.foiJogada = true;
            Destroy(gameObject);
        }

        //if (Input.GetMouseButtonDown(0))
        //{
          //  ataque = true;
            //if (facaSom != null) audioSource.PlayOneShot(facaSom, 0.9f);
        //}

        if (ataque)
        {
            if (!privez)
            {
                transform.eulerAngles = new Vector3(0f, 0f, angle + 180f);
                privez = true;
            }
            ataque = animation.animarAtaque();
            if (!ataque)
                privez = false;
        }
    }
}
