using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargarUsuarioModificar : MonoBehaviour
{
    [SerializeField] CanvasManager canvasManager;
    [SerializeField] ModificarUsuarioCanvas modificarUsuario;
    Usuario usuarioSeleccionado;
    public void ActualizarUsuarioSeleccionado(DatosUsuarioUI usuario)
    {
        usuarioSeleccionado = usuario.GetUsuario();
        
    }

    public void CambiarCanvas()
    {
        canvasManager.AbrirCanvas();
        canvasManager.CerrarCanvas();
    }

    public void CargarUsuario()
    {
        if(usuarioSeleccionado != null)
        {
            modificarUsuario.ActualizarUsuarioSeleccionado(usuarioSeleccionado);
            CambiarCanvas();
        }
        
    }
}
