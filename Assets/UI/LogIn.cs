using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{
    [SerializeField] InputField usuario, contraseña;
    UserManager userManager;
    [SerializeField] CanvasManager canvasManager;
    
    // Start is called before the first frame update
    void Start()
    {
        userManager = UserManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VaciarCasillas()
    {
        usuario.text = "";
        contraseña.text = "";
    }

    public void Confirmar()
    {
        if (userManager.Validarusuario(usuario.text, contraseña.text))
        { 
            Debug.Log("Correcto");            
            canvasManager.AbrirCanvas();
            VaciarCasillas();
            canvasManager.CerrarCanvas();
        }
        else
        {
            Debug.Log("Fallo");
            VaciarCasillas();
        }
    }
}
