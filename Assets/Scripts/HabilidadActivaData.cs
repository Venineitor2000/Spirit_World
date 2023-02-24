using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoMovilidad{
    Estatica,
    Movimiento
}


[CreateAssetMenu(menuName = "ScriptableObject/Habilidad Activa Data", fileName ="NewHabilidadActivaData")]
public class HabilidadActivaData : ScriptableObject
{
    public float cd;
    public int rango;
    public TipoMovilidad movilidad; 
    
}
