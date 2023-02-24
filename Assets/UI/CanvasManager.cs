using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] Canvas objetivoAbrir, objetivoCerrar;
    // Start is called before the first frame update
   

    public void AbrirCanvas()
    {
        objetivoAbrir.gameObject.SetActive(true);
    }

    public void CerrarCanvas()
    {
        objetivoCerrar.gameObject.SetActive(false);
    }
}
