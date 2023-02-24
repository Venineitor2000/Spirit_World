using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecuperarContraseña : MonoBehaviour
{
    [SerializeField] InputField usuario, mail;
    UserManager userManager;
    [SerializeField] MailSender mailSender;
    [SerializeField] CanvasManager canvasManager;
    private void Start()
    {
        userManager = UserManager.instance; 
    }
    public void VaciarCasillas()
    {
        usuario.text = "";
        mail.text = "";
    }

    Usuario GetUsuario()
    {
       return userManager.GetUsuariosDisponibles().Find(x => x.GetNombreUsuario() == usuario.text && x.GetEmail().ToUpper() == mail.text.ToUpper());
    
       
    }

    public void Confirmar()
    {
        if (GetUsuario() != null)
        {
            string contra = UserManager.ContraRandom();
            mailSender.EnviarMail(mail.text, contra);
            GetUsuario().EstablecerContraseña(contra);
            VaciarCasillas();
            canvasManager.AbrirCanvas();
            canvasManager.CerrarCanvas();
        }
            
        else 
            VaciarCasillas();
    }
}
