using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionGrupos : MonoBehaviour
{
    
    [SerializeField] DatosGrupoUI datosGrupo;
    [SerializeField] Transform content;
    public void ActualizarListaMostrada(List<Grupo> grupos)
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);

        }
        if (grupos != null)
            foreach (Grupo grupo in grupos)
            {
                Instantiate(datosGrupo, content).CargarDatos(grupo);

            }

    }

    private void OnEnable()
    {

        ActualizarListaMostrada(UserManager.getGrupos());
    }
}
