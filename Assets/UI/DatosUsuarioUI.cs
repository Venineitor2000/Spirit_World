using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DatosUsuarioUI : MonoBehaviour, ISelectHandler
{
    Usuario usuario;
    [SerializeField] Text nombreUsuario, apellidoNombre, grupos, email, estado;
    
    EliminarUsuario eliminarUsuario;
    CargarUsuarioModificar modificarUsuario;
    RestablecerClave restablecerClave;

    private void Start()
    {
        eliminarUsuario = FindObjectOfType<EliminarUsuario>();
        modificarUsuario= FindObjectOfType<CargarUsuarioModificar>();
        restablecerClave = FindObjectOfType<RestablecerClave>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        
        eliminarUsuario.ActualizarUsuarioSeleccionado(this);
        modificarUsuario.ActualizarUsuarioSeleccionado(this);
        restablecerClave.ActualizarUsuarioSeleccionado(this);
    }

    public Usuario GetUsuario()
    {
        return usuario;
    }

    public void CargarDatos(Usuario usuario)
    {
        
        this.usuario = usuario;
        nombreUsuario.text = usuario.GetNombreUsuario();
        apellidoNombre.text = usuario.GetApellidoNombre();
        email.text = usuario.GetEmail();
        estado.text = usuario.GetEstado() ? "ACTIVO" : "INACTIVO";
        grupos.text = "";
        for (int i = 0; i < usuario.GetGrupos().Count; i++)
        {
            
            if (i < usuario.GetGrupos().Count - 1)
                grupos.text += (usuario.GetGrupos()[i].GetNombre() + ", ");
            else
                grupos.text += usuario.GetGrupos()[i].GetNombre();

        }
    }

    
}
