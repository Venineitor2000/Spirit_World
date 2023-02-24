using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class FiltroGrupo : MonoBehaviour
{
    [SerializeField] InputField nombre;
    [SerializeField] Dropdown estado;
    
    [SerializeField] GestionGrupos gestionGrupos;
    List<Grupo> gruposEncontrados = new List<Grupo>();
    // Start is called before the first frame update

    private void OnEnable()
    {
        Iniciar();
    }

    public void Iniciar()
    {
        nombre.text = "";
        estado.value = 0;
        gruposEncontrados.Clear();
    }
    public void Filtrar()
    {
        gruposEncontrados = new List<Grupo>();
        List<Grupo> gruposCoincidentes1 = BuscarApellidoNombre();
        List<Grupo> gruposCoincidentes2 = BuscarEstado();
        IEnumerable<Grupo> gruposAux = gruposCoincidentes1.Intersect(gruposCoincidentes2);
        gruposEncontrados.AddRange(gruposAux);
        gestionGrupos.ActualizarListaMostrada(gruposEncontrados);

    }



    List<Grupo> BuscarApellidoNombre()
    {
        if (nombre.text == "")
            return UserManager.getGrupos();
        return UserManager.getGrupos().FindAll(x => x.GetNombre().ToUpper() == nombre.text.ToUpper());

    }

    List<Grupo> BuscarEstado()
    {
        if (estado.options[estado.value].text.ToUpper() == "TODOS")
            return UserManager.getGrupos();
        else if (estado.options[estado.value].text.ToUpper() == "ACTIVO")
            return UserManager.getGrupos().FindAll(x => x.GetEstado() == true);
        else
            return UserManager.getGrupos().FindAll(x => x.GetEstado() == false);
    }

    
}
