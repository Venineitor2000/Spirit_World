using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsuarioUI : MonoBehaviour
{
    Usuario usuario;



    public void SetUsuario(Usuario usuario)
    {
        this.usuario = usuario;
    }

    public Usuario GetUsuario()
    {
        return usuario;
    }
}
