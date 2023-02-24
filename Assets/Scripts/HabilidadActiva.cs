using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HabilidadActiva : Habilidad
{
    [SerializeField] protected HabilidadActivaData HabilidadActivaData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Execute()
    {
        

    }

    public int GetRange()
    {
        return HabilidadActivaData.rango;
    }

    public bool GetIsAvaliable()
    {
        return true;
    }

    
}
