using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public abstract class ControladorEspiritu : MonoBehaviour
    {

    public enum Animaciones 
    {
        Death,
        Walk,
        Run,
        IdleBreathe,
        GetHitFront,
        StrafeRight,
        StrafeLeft,
        Atack
    }

    //TEMPORAL, DESPUES BORRAR Y IRIA EN ESPECIE ESTO
    public float speedWalk;
    public float speedRun;
    
    List<Espiritu> spirits;
    Espiritu actualSpirit;
    protected int hpActual;
    protected bool isDead;
    protected Animator animatorAnimaciones;
    [SerializeField] protected Especie especie;





    protected void Inicializar()
    {
        hpActual = especie.hpMax;
        if(animatorAnimaciones == null)
        animatorAnimaciones = GetComponentsInChildren<Animator>()[1];
    }
    

    protected void AddSpirit(int threatLevel, List<HabilidadActiva> abilities, AtaqueBasico basicAtack)
    {
        if(spirits == null)
            spirits = new List<Espiritu>();
        spirits.Add(ScriptableObject.CreateInstance<Espiritu>());
        spirits[spirits.Count - 1].SetEspiritu(threatLevel, abilities, basicAtack);
    }

    protected void SelectSpirit(int index)
    {
        if (spirits.Count > index && index >= 0)
            actualSpirit = spirits[index];
    }
    
    public int GetThreatLevel()
    {
        return 0;
    }
    public abstract void CheckHp();
    

    public Espiritu GetActualSpirit()
    {
        return actualSpirit;
    }   

    public int GetHp()
    {
        return hpActual;
    }

    
        
    public virtual void TakeDamage(int damage)
    {
        if (hpActual - damage >= 0)
            hpActual -= damage;
        else
            hpActual = 0;
    }

    public bool GetIsDetectable()
    {
        return !isDead; //De momento es la unica condicion
    }

    protected void SetIsDead(bool value)
    {
        isDead = value;
    }
}