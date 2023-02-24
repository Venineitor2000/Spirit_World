using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cazar : StateMachineBehaviour
{
    AI self;
    int? atack;

    //Creamos este metodo, para poder hacer q se suscriba al evento OnTakeDamage, por q es la unica forma que encontre de hacer para que el observador sea el que establece el parametro que quiere usar en su metodo, ya que si definia un evento que tome un parametro, el observador recibia el parametro desde AI, y no al revez, cosa que no me servia
    void ChangeToEvadeState()
    {
      
        self.ChangeMeState(AI.ME_states.Evadiendo);
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (self == null)
            self = animator.GetComponent<AI>();
        self.OnTakeDamage += ChangeToEvadeState;//ACA
        if (self.GetMeState() == AI.ME_states.Cazando)
        {
            self.CheckHp();
            if (self.GetActualSpirit() != null)
            {
                bool noAvaliableAtacks = false;
                atack = null;
                while (atack == null && !noAvaliableAtacks)
                {
                    //Revisa si hay al menos 1 ataque disponible, antes de elegir uno al azar
                    //Primero revisa si el ataque basico no esta disponible, si esta disponible solo sigue de largo, si no esta disponible revisa las habilidades activas
                    if (!self.GetActualSpirit().GetIsAtackAvaliable(Atacks.BasicAtack))
                        for (int i = 0; i < self.GetActualSpirit().GetActiveAbilities().Count + 1; i++)
                        {

                            if (self.GetActualSpirit().GetIsAtackAvaliable((Atacks)i))
                            {
                                break;
                            }

                            else
                            {
                                noAvaliableAtacks = true;
                                self.ChangeMeState(AI.ME_states.Evadiendo);
                            }

                        }


                    //Esto puede ser una habilidad o un ataque basico
                    atack = Random.Range(0, self.GetActualSpirit().GetActiveAbilities().Count + 1);


                    if (!self.GetActualSpirit().GetIsAtackAvaliable((Atacks)atack))
                    {
                        atack = null;
                    }
                }

                self.StartCoroutine(self.FollowTarget(0.1f, self.GetSelectedEnemy().transform));

            }
        }
            


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (self.GetMeState() == AI.ME_states.Cazando)
        {
            self.CheckHp();
            self.ValidateEnemyLost();

            //CUIDADO, El paso 2 representa peligro aun, habria q limitarlo a q se ejecute cada x tiempo



            if (atack != null && animator)
                if (Vector3.Distance(self.GetSelectedEnemy().transform.position, self.transform.position) <= self.GetActualSpirit().getAtackRange((Atacks)atack))
                {
                   
                    
                    self.SetSelectedAtack((int)atack);
                    
                    self.ChangeMeState(AI.ME_states.Luchando);



                }

                else if (Vector3.Distance(self.GetSelectedEnemy().transform.position, self.transform.position) > self.GetInteresRange())
                {
                    Debug.Log("Enemigo fuera de rango de interes");
                    
                    
                    self.ChangeMeState(AI.ME_states.Alerta);

                }
        }
            

    }

    

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        self.StopFollowTarget();
        self.OnTakeDamage -= ChangeToEvadeState;

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}




}
