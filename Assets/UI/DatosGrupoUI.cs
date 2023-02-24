using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DatosGrupoUI : MonoBehaviour, ISelectHandler
{
    Grupo grupo;
    [SerializeField] Text nombre, descipcion, estado;
    

    EliminarGrupo eliminarGrupo; 
    CargarGrupoModificar modificarGrupo; 

    private void Start()
    {
        
        eliminarGrupo = FindObjectOfType<EliminarGrupo>();
        
        modificarGrupo = FindObjectOfType<CargarGrupoModificar>();
    }

    public void OnSelect(BaseEventData eventData)
    {

        eliminarGrupo.ActualizarGrupoSeleccionado(this);
        
        modificarGrupo.ActualizarGrupoSeleccionado(this);
    }

    public Grupo GetGrupo()
    {
        return grupo;
    }

    public void CargarDatos(Grupo grupo)
    {

        this.grupo = grupo;
        nombre.text = grupo.GetNombre();
        descipcion.text = grupo.GetDescipcion();
        
        estado.text = grupo.GetEstado() ? "ACTIVO" : "INACTIVO";
        
        
    }


}
