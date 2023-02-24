using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModificarGrupoCanvas : MonoBehaviour
{
    Grupo grupoSeleccionado;

    [SerializeField] InputField nombre, descripcion;
    [SerializeField] Toggle estado;
    [SerializeField] Transform content;
    [SerializeField] GameObject usuarioPrefab;
    UserManager userManager;
    List<Toggle> usuariosCheckBox = new List<Toggle>();

    
    public void ActualizarGrupoSeleccionado(Grupo grupo)
    {

        grupoSeleccionado = grupo;

    }


    private void OnEnable()
    {
        userManager = UserManager.instance;
        ActualizarListaMostrada(userManager.GetUsuariosDisponibles());

        ActualizarDatosMostrados();

        ActualizarValorUsuarios();

    }

    //Aca entero
    void ActualizarDatosMostrados()
    {
        nombre.text = grupoSeleccionado.GetNombre();        
        descripcion.text = grupoSeleccionado.GetDescipcion();
        estado.isOn = grupoSeleccionado.GetEstado();

    }
    void ActualizarValorUsuarios()
    {
        //if(grupoSeleccionado.GetUsuarios() != null)
        foreach (var usuario in grupoSeleccionado.GetUsuarios())
        {
            foreach (var usuarioCheckBox in usuariosCheckBox)
            {

                if (usuario.GetApellidoNombre() == usuarioCheckBox.GetComponentInChildren<Text>().text)
                {
                    usuarioCheckBox.isOn = true;
                }





            }
        }
    }

    public void ActualizarListaMostrada(List<Usuario> usuarios)
    {
        usuariosCheckBox = new List<Toggle>();
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);

        }
        if (usuarios != null)
            foreach (Usuario usuario in usuarios)
            {
                GameObject u = Instantiate(usuarioPrefab, content);
                u.GetComponentInChildren<Text>().text = usuario.GetApellidoNombre();
                u.GetComponent<UsuarioUI>().SetUsuario(usuario);

                usuariosCheckBox.Add(u.GetComponent<Toggle>());
            }

    }

    List<Usuario> GetSelectedUsuarios()
    {
        List<Usuario> usuarios = new List<Usuario>();
        foreach (var item in usuariosCheckBox)
        {
            if (item.isOn)
                usuarios.Add(item.GetComponent<UsuarioUI>().GetUsuario());
        }
        return usuarios;
    }

    
    public void Modificar()
    {
         if(grupoSeleccionado != null)
         {
            if(grupoSeleccionado.GetNombre().ToUpper() == "Admin".ToUpper())
            {
                if(GetSelectedUsuarios().Count > 0)
                    grupoSeleccionado.ActualizarCampos(grupoSeleccionado.GetNombre(), descripcion.text, true, GetSelectedUsuarios());
                else
                {
                    grupoSeleccionado.ActualizarCampos(grupoSeleccionado.GetNombre(), descripcion.text, true, grupoSeleccionado.GetUsuarios());
                }
            }
            else if (!estado.isOn && GetSelectedUsuarios().Count > 0)
                grupoSeleccionado.ActualizarCampos(nombre.text, descripcion.text, true, GetSelectedUsuarios());
            else
                grupoSeleccionado.ActualizarCampos(nombre.text, descripcion.text, estado.isOn, GetSelectedUsuarios());

        }


    }
   
}
