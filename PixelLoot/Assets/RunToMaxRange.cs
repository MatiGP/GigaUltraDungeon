using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunToMaxRange : StateMachineBehaviour
{
    public float Attackrange;

    private bool facingRight;
    private Transform playerPos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerPos != null)
        {
            facingRight = playerPos.position.x > animator.transform.position.x ? true : false;


            if ((facingRight && animator.transform.rotation.y <= 180))
            {
                animator.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if ((!facingRight && animator.transform.rotation.y >= 0))
            {
                animator.transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (Vector2.Distance(playerPos.position, animator.transform.position) > Attackrange)
            {
                animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos.position, 5 * Time.deltaTime);

            }
            else
            {
                animator.SetTrigger("attack");                
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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
