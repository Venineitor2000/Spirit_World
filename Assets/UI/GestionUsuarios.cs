using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionUsuarios : MonoBehaviour
{
    
    UserManager userManager;
    [SerializeField] DatosUsuarioUI datosusuario;
    [SerializeField] Transform content;

    
    public void ActualizarListaMostrada(List<Usuario> usuarios)
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
            
        }
        if(usuarios != null)
        foreach (Usuario usuario in usuarios)
        {
            Instantiate(datosusuario, content).CargarDatos(usuario);
            
        }
        
    }

    private void OnEnable()
    {
        userManager = UserManager.instance;
        ActualizarListaMostrada(userManager.GetUsuarios());
    }
}
