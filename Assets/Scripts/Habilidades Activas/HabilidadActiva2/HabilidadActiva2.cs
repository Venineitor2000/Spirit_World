using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadActiva2 : HabilidadActiva
{
    
    [SerializeField] GameObject prefab;
    
    

    public override void Execute()
    {
        GameObject proyectil = Instantiate(prefab, transform.position, transform.rotation);
        proyectil.GetComponent<Proyectil>().Inicializar(transform);
    }
}
