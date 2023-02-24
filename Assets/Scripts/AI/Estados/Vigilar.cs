using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vigilar : StateMachineBehaviour
{
    AI self;

    List<Collider> enemys;
    void ChangeToConfuseState()
    {
        self.ChangeMeState(AI.ME_states.Confundido);
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (self == null)
            self = animator.GetComponent<AI>();
        self.OnTakeDamage += ChangeToConfuseState;//ACA
        if (self.GetMeState() == AI.ME_states.Alerta)
        {
            
            self.CheckHp();            
            enemys = new List<Collider>();
            self.StartCoroutine(self.MoveRandomAroundPosition(animator.transform.position, 5, 10, 0, 360, 0));
            
        }


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(self.GetMeState() == AI.ME_states.Alerta)
        {
            self.CheckHp();
            
            if (enemys.Count == 0)
            {
                enemys.AddRange(Physics.OverlapSphere(animator.transform.position, self.GetVisionRange(), LayerMask.GetMask("Spirits")));               
                enemys.RemoveAll(enemy => !enemy.GetComponent<ControladorEspiritu>().GetIsDetectable());
                enemys.Remove(self.GetComponent<BoxCollider>());//Evita q se detecte a el mismo como enemigo
                
                self.ValidateMovementEnd();
                
                
                    
                
            }

            else
            {
                
                self.SetSelectedEnemy(enemys[0].GetComponent<ControladorEspiritu>());
                
                if (self.GetCourageLevel() >= self.GetSelectedEnemy().GetThreatLevel())
                {
                    
                    self.ChangeMeState(AI.ME_states.Cazando);
                }
                else
                {
                    
                    self.ChangeMeState(AI.ME_states.Amenazado);
                }
            }
        }




    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        self.CancelMoveRandomAroundPosition();
        self.OnTakeDamage -= ChangeToConfuseState;
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
