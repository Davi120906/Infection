using UnityEngine;

public class hideblockscript : MonoBehaviour
{
    void Start()
    {
 
        Renderer r = GetComponent<Renderer>();
        if (r != null)
            r.enabled = false;
    }
}
