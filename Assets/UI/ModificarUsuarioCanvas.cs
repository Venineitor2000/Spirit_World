using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModificarUsuarioCanvas : MonoBehaviour
{
    Usuario usuarioSeleccionado;

    [SerializeField] InputField usuario, apellido, nombre, email;
    [SerializeField] Toggle estado;
    [SerializeField] Transform content;
    [SerializeField] GameObject grupoPrefab;
    UserManager userManager;
    List<Toggle> gruposCheckBox = new List<Toggle>();

    private void Start()
    {
        userManager = UserManager.instance;
    }

    public void ActualizarUsuarioSeleccionado(Usuario usuario)
    {

        usuarioSeleccionado = usuario;
        
    }

    
    private void OnEnable()
    {
        ActualizarListaMostrada(UserManager.getGruposDisponibles());
        
        ActualizarDatosMostrados();
        
        ActualizarValorGrupos();
        
    }

    void ActualizarDatosMostrados()
    {
        usuario.text = usuarioSeleccionado.GetNombreUsuario();
        nombre.text = usuarioSeleccionado.GetNombre();
        apellido.text = usuarioSeleccionado.GetApellido();
        email.text = usuarioSeleccionado.GetEmail();
        estado.isOn = usuarioSeleccionado.GetEstado();

    }
    void ActualizarValorGrupos()
    {
        foreach (var grupo in usuarioSeleccionado.GetGrupos())
        {
            foreach (var grupoCheckBox in gruposCheckBox)
            {
                
                if (grupo.GetNombre() == grupoCheckBox.GetComponentInChildren<Text>().text)
                {
                    grupoCheckBox.isOn = true;
                    Debug.Log("Verdadero cuando: "+ grupo.GetNombre() +" y " + grupoCheckBox.GetComponentInChildren<Text>().text);
                }
                    
                



            }
        }
    }
    
    public void ActualizarListaMostrada(List<Grupo> grupos)
    {
        gruposCheckBox = new List<Toggle>();
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);

        }
        if (grupos != null)
            foreach (Grupo grupo in grupos)
            {
                GameObject g = Instantiate(grupoPrefab, content);
                g.GetComponentInChildren<Text>().text = grupo.GetNombre();
                g.GetComponent<GrupoUI>().SetGrupo(grupo);
                
                gruposCheckBox.Add(g.GetComponent<Toggle>());
            }

    }

    List<Grupo> GetSelectedGrupos()
    {
        List<Grupo> grupos = new List<Grupo>();
        foreach (var item in gruposCheckBox)
        {
            if (item.isOn)
                grupos.Add(item.GetComponent<GrupoUI>().GetGrupo());
        }
        return grupos;
    }

    public void Modificar()
    {
        if(usuarioSeleccionado != null)
        usuarioSeleccionado.ActualizarCampos(usuario.text, nombre.text, apellido.text, email.text, GetSelectedGrupos(), estado.isOn);//borra esta

    }

   


}
