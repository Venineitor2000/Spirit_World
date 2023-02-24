using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reporte : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GenerarReporte(List<string> lines,string espirituName)
    {
        System.IO.Directory.CreateDirectory(Application.persistentDataPath.ToString()+ "/REPORTES");
        System.IO.File.WriteAllLines(Application.persistentDataPath.ToString() + "/REPORTES/"+espirituName+"_" + System.DateTime.Now.ToString("dd-MM-yyyy-(HH;mm;ss)") + ".txt", lines);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
