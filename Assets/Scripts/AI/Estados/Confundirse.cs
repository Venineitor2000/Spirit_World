using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confundirse : StateMachineBehaviour
{
    AI self;
    float time;
    float duration = 5;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (self == null)
            self = animator.GetComponent<AI>();
        if (self.GetMeState() == AI.ME_states.Confundido)
        {
            time = 0;
            self.StartCoroutine(self.MoveRandomAroundPosition(animator.transform.position, 1.5f, 30, 0, 360, 1));
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (self == null)
            self = animator.GetComponent<AI>();
        if (self.GetMeState() == AI.ME_states.Confundido)
        {
            self.CheckHp();
            if(time >= duration)
            {               
                
                self.ChangeMeState(AI.ME_states.Alerta);
            }
            time += Time.deltaTime;

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        self.CancelMoveRandomAroundPosition();
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
