using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReporteEspiritus : MonoBehaviour
{
    [SerializeField] Text texto;
    [SerializeField] bool onlyAlive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!onlyAlive)
        texto.text = "ESPIRITUS TOTALES:" + FindObjectsOfType<ControladorEspiritu>().Length;
        else
        {
            int cantidad = 0;
            foreach (var espiritu in FindObjectsOfType<ControladorEspiritu>())
            {
                if (!espiritu.GetIsDead())
                    cantidad++;
            }
            texto.text = "ESPIRITUS VIVOS:" + cantidad;
        }
    }
}
