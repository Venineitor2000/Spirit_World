using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgregarUsuarioCanvas : MonoBehaviour
{
    [SerializeField] InputField usuario, apellido, nombre, email;
    [SerializeField] Toggle estado;
    [SerializeField] Transform content;
    [SerializeField] GameObject grupoPrefab;
    UserManager userManager;
    List<Toggle> gruposCheckBox = new List<Toggle>();

    

    private void OnEnable()
    {
        userManager = UserManager.instance;
        ActualizarListaMostrada(UserManager.getGruposDisponibles());
    }

    public void ActualizarListaMostrada(List<Grupo> grupos)
    {
        
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);

        }if(grupos != null)
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
        List < Grupo > grupos = new List<Grupo>();
        foreach (var item in gruposCheckBox)
        {
            if (item.isOn)
                grupos.Add(item.GetComponent<GrupoUI>().GetGrupo());
        }
        return grupos;
    }

    public void Agregar()
    {
        
        userManager.AddUsuario(usuario.text, nombre.text, apellido.text, email.text, GetSelectedGrupos(), estado.isOn);
    }
}
