using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminarGrupo : MonoBehaviour
{
    

    DatosGrupoUI grupoSeleccionado;
    public void Eliminar()
    {
        if(grupoSeleccionado != null)
        {
            UserManager.EliminarGrupo(grupoSeleccionado.GetGrupo());
            Destroy(grupoSeleccionado.gameObject);
        }
        

    }

    public void ActualizarGrupoSeleccionado(DatosGrupoUI grupo)
    {

        grupoSeleccionado = grupo;
    }
}
