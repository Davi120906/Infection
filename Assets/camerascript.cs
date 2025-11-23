using UnityEngine;

public class CameraTopDown : MonoBehaviour
{
    public Transform alvo;
    public Vector3 deslocamento = new Vector3(0, 0, -10f);

    
    private float limiteEsquerdo = -45f;
    private float limiteDireito = 45f;
    private float limiteSuperior = 45f;
    private float limiteInferior = -45f;

    private Camera cam;
    private float alturaCamera;
    private float larguraCamera;

    void Start()
    {
        cam = GetComponent<Camera>();
        CalcularTamanhoCamera();
    }

    void CalcularTamanhoCamera()
    {
       
        alturaCamera = cam.orthographicSize;
        larguraCamera = alturaCamera * cam.aspect;
    }

    void LateUpdate()
    {
        if (alvo == null) return;

        
        Vector3 posicaoDesejada = alvo.position + deslocamento;

        
        posicaoDesejada.x = Mathf.Clamp(
            posicaoDesejada.x,
            limiteEsquerdo + larguraCamera,
            limiteDireito - larguraCamera
        );

        
        posicaoDesejada.y = Mathf.Clamp(
            posicaoDesejada.y,
            limiteInferior + alturaCamera,
            limiteSuperior - alturaCamera
        );

        transform.position = posicaoDesejada;
    }
}