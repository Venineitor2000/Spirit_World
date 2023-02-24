using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgregarGrupoCanvas : MonoBehaviour
{
    [SerializeField] InputField nombre, descripcion;
    [SerializeField] Toggle estado;
    [SerializeField] Transform content;
    [SerializeField] GameObject usuarioPrefab;
    UserManager userManager;
    List<Toggle> usuariosCheckBox = new List<Toggle>();

   

    private void OnEnable()
    {
        userManager = UserManager.instance;
        ActualizarListaMostrada(userManager.GetUsuariosDisponibles());
    }

    public void ActualizarListaMostrada(List<Usuario> usuarios)
    {

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

    public void Agregar()
    {
        
        UserManager.AddGrupo(nombre.text, descripcion.text, estado.isOn, GetSelectedUsuarios());
    }
}
