using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargarGrupoModificar : MonoBehaviour
{
    [SerializeField] CanvasManager canvasManager;
    [SerializeField] ModificarGrupoCanvas modificarGrupo;
    Grupo grupoSeleccionado;
    public void ActualizarGrupoSeleccionado(DatosGrupoUI grupo)
    {
        grupoSeleccionado = grupo.GetGrupo();

    }

    public void CambiarCanvas()
    {
        canvasManager.AbrirCanvas();
        canvasManager.CerrarCanvas();
    }

    public void CargarGrupo()
    {
        if (grupoSeleccionado != null)
        {
            modificarGrupo.ActualizarGrupoSeleccionado(grupoSeleccionado);
            CambiarCanvas();
        }

    }
}
