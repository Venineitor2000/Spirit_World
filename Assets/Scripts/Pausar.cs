using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausar : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    private void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            
            canvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Despausar()
    {
        Time.timeScale = 1;
    }

    
}
