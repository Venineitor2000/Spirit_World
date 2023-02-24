using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escapar : StateMachineBehaviour
{
    AI self;
    float moveDistance = 50;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Tengo que escapar de aqui! Este tipo es peligroso!");
        
        if (self == null)
            self = animator.GetComponent<AI>();
        if(self.GetMeState() == AI.ME_states.Amenazado)
        {
            self.CheckHp();
            self.ValidateEnemyLost();//Se fija antes de empezar a huir si el enemigo esta ahi, pero si ya empezo a correr no mira atras y sigue corriendo hasta estar seguro
            Vector3 moveDirection = animator.transform.position - self.GetSelectedEnemy().transform.position;
            moveDirection.y = 0;
            moveDirection.Normalize();
            Vector3 escapeObjetivePosition = animator.transform.position + moveDirection * moveDistance;//Posicion a la que queres escapar
            self.MoveToPosition(escapeObjetivePosition, 1);
            
            
        }
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (self.GetMeState() == AI.ME_states.Amenazado)
        {
            self.CheckHp();
            if(self.ValidateMovementEnd())
            {
                
                self.ChangeMeState(AI.ME_states.Alerta);
            }
        }
            
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        self.StopMoveToPosition();
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
