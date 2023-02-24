using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public enum Atacks
{
    Abilitie1,
    Abilitie2,
    Abilitie3,
    Abilitie4,
    Abilitie5,
    BasicAtack,
}
public class AI : ControladorEspiritu
{
    public enum ME_states //Representan los estados de la Maquina de estados de Ai
    {
        Alerta,
        Cazando,
        Amenazado,
        Luchando,
        Evadiendo,
        Muerto,
        Confundido
    }

    
    ME_states meState = ME_states.Alerta;

    ControladorEspiritu SelectedEnemy;
    Atacks SelectedAtack;
    int courageLevel = 100;
    int InterestRange = 50;
    int visionRange = 50;
    NavMeshAgent navMeshAgente;
    Animator animatorComportamiento;
    

    public event Action OnTakeDamage;
    

    

    // Start is called before the first frame update
    void Start()
    {
        Inicializar();
        if(animatorComportamiento == null)
            animatorComportamiento = GetComponent<Animator>();
        if (navMeshAgente == null)
            navMeshAgente = GetComponent<NavMeshAgent>();
        //ES SOLO PARA HACER LAS PRUEBAS INICIALES
        AddSpirit(0,
            new List<HabilidadActiva>() {
            GetComponents<HabilidadActiva>()[0],
            GetComponents<HabilidadActiva>()[0],
            GetComponents<HabilidadActiva>()[0],
            GetComponents<HabilidadActiva>()[0],
            GetComponents<HabilidadActiva>()[0]
            },
            GetComponent<AtaqueBasico>());
        SelectSpirit(0);

        
    }

    public void SetCourageLevel(int courageLevel)
    {
        this.courageLevel = courageLevel;
    }    

    public void ExecuteAtack(Atacks atack)
    {
        animatorAnimaciones.SetTrigger(Animaciones.Atack.ToString());
        GetActualSpirit().ExecuteAtack(atack);
        
    }

    public void MoveToPosition(Vector3 position, int movementType) //0 walk, 1 run
    {

        
        if (movementType == 0)
        {
            
            navMeshAgente.speed = speedWalk;
            animatorAnimaciones.SetTrigger(Animaciones.Walk.ToString());
        }
            

        else if (movementType == 1)
        {
            
            navMeshAgente.speed = speedRun;
            animatorAnimaciones.SetTrigger(Animaciones.Run.ToString());
        }
            
        else
            Debug.LogWarning("ERROR INVALIDO ACA");
        
        navMeshAgente.isStopped = false;
        navMeshAgente.SetDestination(position);
        
    }

    public void StopMoveToPosition()
    {
        

        if (!animatorAnimaciones.GetCurrentAnimatorStateInfo(0).IsName(Animaciones.IdleBreathe.ToString()))
            animatorAnimaciones.SetTrigger(Animaciones.IdleBreathe.ToString());
        navMeshAgente.isStopped = true;
    }

    public int GetVisionRange()
    {
        return visionRange;
    }

    public int GetInteresRange()
    {
        return InterestRange;
    }
    public int GetCourageLevel()
    {
        return courageLevel;
    }

    public ControladorEspiritu GetSelectedEnemy()
    {
        return SelectedEnemy;
    }
    public void SetSelectedEnemy(ControladorEspiritu SelectedEnemy)
    {
        this.SelectedEnemy = SelectedEnemy;
    }

    
    
    
    public IEnumerator MoveRandomAroundPosition(Vector3 startPosition, float timeBetwenChangeMovement, float movementDistance, float minAngleRange, float maxAngleRange, int movementType)
    {


        
        while (true)
        {
                            

                
                //Uso transform.forward como starter vector, por que la idea es que calculo un vector alrededor de la direccion en la q esta mirando la AI, y despues sumo eso a la posicion actual
                Vector3 objectivePosition = startPosition + ExtraMath.GetRotatedVectorInRandomAngle(minAngleRange, maxAngleRange, movementDistance, transform.forward, Vector3.up);
                MoveToPosition(objectivePosition,movementType);
                


            
            yield return new WaitForSeconds(timeBetwenChangeMovement);
            
            
        }
        
        

        
    }

    public void CancelMoveRandomAroundPosition()
    {
        StopAllCoroutines();
        StopMoveToPosition();
    }

    public void SetSelectedAtack(int atack)
    {
        SelectedAtack = (Atacks)atack;
    }

    public Atacks GetSelectedAtack()
    {
        return SelectedAtack;
    }

    
    //Solo funciona correctamente si es usado en casos donde con anterioridad ya tenemos a un enemigo detectado
    public void ValidateEnemyLost()
    {
        
        if(SelectedEnemy == null)
        {
            ChangeMeState(ME_states.Alerta);            
        }
            
        else if(!SelectedEnemy.GetIsDetectable())
        {
            ChangeMeState(ME_states.Alerta);
        }
            
        

    }

    public IEnumerator FollowTarget(float updateTime, Transform target)
    {
        
        while (true)
        {
            animatorAnimaciones.SetTrigger(Animaciones.Run.ToString());
            MoveToPosition(target.position, 1);
            yield return new WaitForSeconds(updateTime);
        }
    }    

    public void StopFollowTarget()
    {
        //Si en un futuro decidimos usar multiples corrutinas, habria que revisar como parar solo la corrutina especifica FollowTarget, buscar en favoritos stopCoroutine en google
        animatorAnimaciones.SetTrigger(Animaciones.IdleBreathe.ToString());
        StopAllCoroutines();
        StopMoveToPosition();
        
    }

    public ME_states GetMeState()
    {
        return meState;
    }

    

    
    public bool ValidateMovementEnd()
    {
        if (!navMeshAgente.pathPending)
        {
            if (navMeshAgente.remainingDistance <= navMeshAgente.stoppingDistance)
            {
                if (!navMeshAgente.hasPath || navMeshAgente.velocity.sqrMagnitude == 0f)
                {                   
                    StopMoveToPosition();
                    return true;
                }
            }
        }
        return false;
    }

    public override void CheckHp()
    {
        if(GetHp() <= 0)
        {
            animatorAnimaciones.SetTrigger(Animaciones.Death.ToString());
            ChangeMeState(ME_states.Muerto);
            SetIsDead(true);
            //transform.localScale = new Vector3(1, 0.1f, 1);//Esto es temporal de momento
        }
    }

    //Puede que sea necesario hacer cabios si queremos sumar animaciones que no representen ningun estado del ME
    public void ChangeMeState(ME_states state)
    {
        animatorComportamiento.SetTrigger(state.ToString());        
        meState = state;
    }

    public override void TakeDamage(int damage) 
    {        
        base.TakeDamage(damage);
        OnTakeDamage?.Invoke();//ACA


    }

    

}
