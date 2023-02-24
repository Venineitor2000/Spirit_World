using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evadir : StateMachineBehaviour
{
    AI self;
    float evasionDistance = 15;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (self == null)
            self = animator.GetComponent<AI>();
        if (self.GetMeState() == AI.ME_states.Evadiendo)
        {
            self.CheckHp();
            //Calcula una posicion al azar en direccion opuesta al enemigo pero en un angulo de semi circulo para la evasion
            Vector3 auxEvasionDirection = (self.transform.position - self.GetSelectedEnemy().transform.position);
            
            self.MoveToPosition(self.transform.position + ExtraMath.GetRotatedVectorInRandomAngle(-90,90,evasionDistance, auxEvasionDirection, Vector3.up), 1);
            
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (self.GetMeState() == AI.ME_states.Evadiendo)
        {
            self.CheckHp();
            if(self.ValidateMovementEnd())
            {
                
                self.ChangeMeState(AI.ME_states.Cazando);
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
