using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInicial : MonoBehaviour
{
    [SerializeField] GameObject cerrarSesion, testear, administrar, cambiarCOntra;
    UserManager userManager;

    

    private void OnEnable()
    {
        userManager = UserManager.instance;
        
        Actualizar();
    }

    public void Actualizar()
    {
        
        if (userManager.GetUsuarioLogueadoActual() != null)
        {
            
            cerrarSesion.SetActive(true);
            cambiarCOntra.SetActive(true);
            if (userManager.GetUsuarioLogueadoActual().GetGrupos().Find(x => x.GetNombre().ToUpper() == "ADMIN") != null )
                administrar.SetActive(true);
            else
                administrar.SetActive(false);
            if (userManager.GetUsuarioLogueadoActual().GetGrupos().Find(x => x.GetNombre().ToUpper() == "TESTER") != null)
            {
                testear.SetActive(true);
                
            }
                
            else
            {
                testear.SetActive(false);
                
            }
        }

        else
        {
            cerrarSesion.SetActive(false);
            cambiarCOntra.SetActive(false);
            testear.SetActive(false);
            administrar.SetActive(false);
        }
    }
}
