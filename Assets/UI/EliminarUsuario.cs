using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EliminarUsuario : MonoBehaviour
{
    UserManager userManager;
    
    DatosUsuarioUI usuarioSeleccionado;

    private void Start()
    {
        userManager = UserManager.instance;
    }

    public void Eliminar()
    {
        if(usuarioSeleccionado != null)
        {
            userManager.EliminarUsuario(usuarioSeleccionado.GetUsuario());
            Destroy(usuarioSeleccionado.gameObject);
        }
        
        
    }

    public void ActualizarUsuarioSeleccionado(DatosUsuarioUI usuario)
    {
        usuarioSeleccionado = usuario;
    }
}
