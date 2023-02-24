using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espiritu : ScriptableObject
{
    int threatLevel;
    List<HabilidadActiva> abilities;
    AtaqueBasico basicAtack;
    
    public void SetEspiritu(int threatLevel,List<HabilidadActiva> abilities, AtaqueBasico basicAtack)
    {
        this.threatLevel = threatLevel;
        this.abilities = abilities;
        this.basicAtack = basicAtack;

    }

    public int GetThreatLevel()
    {
        return threatLevel;
    }



    public List<HabilidadActiva> GetActiveAbilities()
    {
        
        List<HabilidadActiva> activeAbilities = new List<HabilidadActiva>();
        foreach (var abilitie in abilities)
	    {
            
            
                activeAbilities.Add(abilitie);
            
	    }      
        return activeAbilities;
    }

    public AtaqueBasico GetBacicAtack()
    {
        return basicAtack;
    }

    public bool GetIsAtackAvaliable(Atacks atack)
    {
        if (atack == Atacks.BasicAtack)
        {
            return false;
                //return GetBacicAtack().GetIsAvaliable(); Eliminado ahora que solo tenemos habilidades, es para hacer las pruebas solo
            
        }
            
        else
        {

          
            return GetActiveAbilities()[(int)atack].GetIsAvaliable();
            
        }
            
    }

    public void ExecuteAtack(Atacks atack)
    {
        
        if (GetIsAtackAvaliable(atack))
            if (atack == Atacks.BasicAtack)
                basicAtack.Execute();
            else
                abilities[(int)atack].Execute();
        
    }

    public int getAtackRange(Atacks atack)
    {

        if (atack == Atacks.BasicAtack)
            return GetBacicAtack().GetRange();
        else
        {
            
            return GetActiveAbilities()[(int)atack].GetRange();
        }

    }
}
