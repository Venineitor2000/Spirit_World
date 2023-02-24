using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrupoUI : MonoBehaviour
{
    Grupo grupo;

    

    public void SetGrupo(Grupo grupo)
    {
        this.grupo = grupo;
    }

    public Grupo GetGrupo()
    {
        return grupo;
    }
}
