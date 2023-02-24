using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestablecerClave : MonoBehaviour
{
    DatosUsuarioUI usuarioSeleccionado;
    UserManager userManager;
    [SerializeField] MailSender mailSender;

    private void Start()
    {
        userManager = UserManager.instance;
    }



    public void ActualizarUsuarioSeleccionado(DatosUsuarioUI usuario)
    {
        usuarioSeleccionado = usuario;
    }

    public void Confirmar()
    {

        if(usuarioSeleccionado != null)
        {
            string contra = UserManager.ContraRandom();
            
            mailSender.EnviarMail(usuarioSeleccionado.GetUsuario().GetEmail(), contra);
            usuarioSeleccionado.GetUsuario().EstablecerContraseña(contra);
        }
        




    }
}
