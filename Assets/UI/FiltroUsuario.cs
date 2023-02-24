using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class FiltroUsuario : MonoBehaviour
{
    [SerializeField] InputField apellidoNombre;
    [SerializeField] Dropdown estado, grupo;
    UserManager userManager;
    [SerializeField] GestionUsuarios gestionUsuarios;
    List<Usuario> usuariosEncontrados = new List<Usuario>();
    // Start is called before the first frame update

    
    private void OnEnable()
    {
        userManager = UserManager.instance;
        Iniciar();
    }

    public void Iniciar()
    {
        apellidoNombre.text = "";
        estado.value = 0;
        grupo.value = 0;
        usuariosEncontrados.Clear();
    }
    public void Filtrar()
    {
        usuariosEncontrados = new List<Usuario>();
        List<Usuario> usuariosCoincidentes1 = BuscarApellidoNombre();
        List<Usuario> usuariosCoincidentes2 = BuscarEstado();
        List<Usuario> usuariosCoincidentes3 = BuscarGrupo();
        IEnumerable<Usuario> usuariosAux = ((usuariosCoincidentes1.Intersect(usuariosCoincidentes2)).Intersect(usuariosCoincidentes3));
        usuariosEncontrados.AddRange(usuariosAux);
        gestionUsuarios.ActualizarListaMostrada(usuariosEncontrados);
        
    }

    

    List<Usuario> BuscarApellidoNombre()
    {
        if(apellidoNombre.text == "")
            return userManager.GetUsuarios();
        return userManager.GetUsuarios().FindAll(x => x.GetApellidoNombre().ToUpper() == apellidoNombre.text.ToUpper());  
        
    }

    List<Usuario> BuscarEstado()
    {
        if (estado.options[estado.value].text.ToUpper() == "TODOS")
            return userManager.GetUsuarios();
        else if(estado.options[estado.value].text.ToUpper() == "ACTIVO")
            return userManager.GetUsuarios().FindAll(x => x.GetEstado() == true);
        else 
            return userManager.GetUsuarios().FindAll(x => x.GetEstado() == false);
    }

    List<Usuario> BuscarGrupo()
    {
        if (grupo.options[grupo.value].text.ToUpper() == "TODOS")
            return userManager.GetUsuarios();
        else
            return userManager.GetUsuarios().FindAll(x => x.GetGrupos().Find(y => y.GetNombre().ToUpper() == grupo.options[grupo.value].text.ToUpper()) != null);
        
    }
}

