using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiarCOntrase├▒a : MonoBehaviour
{
    [SerializeField] InputField contraActual, contraNueva, confirmarContraNueva;
    UserManager userManager;
    [SerializeField] CanvasManager canvasManager;

    private void Start()
    {
        userManager = UserManager.instance;
    }

    public void CambiarCOntra()
    {
        if (userManager.GetUsuarioLogueadoActual() != null)
            if (userManager.GetUsuarioLogueadoActual().ValidarContrase├▒a(contraActual.text))
            {
                if (contraNueva.text == confirmarContraNueva.text)
                {
                    userManager.GetUsuarioLogueadoActual().EstablecerContrase├▒a(contraNueva.text);
                    canvasManager.AbrirCanvas();
                    VaciarCasillas();
                    canvasManager.CerrarCanvas();


                }

                else
                {

                    contraNueva.text = "";
                    confirmarContraNueva.text = "";
                }
            }

            else
            {
                VaciarCasillas();
            }

        else
            Debug.Log("No podes usar esto si nestar logueado");
    }
    public void VaciarCasillas()
    {
        contraActual.text = "";
        contraNueva.text = "";
        confirmarContraNueva.text = "";
    }
}
