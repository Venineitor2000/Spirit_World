using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReporteEspiritus : MonoBehaviour
{
    [SerializeField] Text texto;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        texto.text = "ESPIRITUS EN EL NIVEL:" + FindObjectsOfType<ControladorEspiritu>().Length;
    }
}
