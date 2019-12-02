using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_CastSpell : StateMachineBehaviour
{
    public float offset = -90;
    public float startTimeBtwShots = 1f;

    public GameObject[] projectiles;
    private Transform playerPos;
    private float timeBtwShots;
    private float rotz;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = animator.GetComponent<Enemy>().playerPos;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        try
        {
            Vector3 difference = playerPos.transform.position - animator.transform.position;
            rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        catch
        {
            animator.ResetTrigger("attack");
        }


        if (timeBtwShots <= 0)
        {
            //Instantiate(projectile, animator.transform.position, Quaternion.Euler(0, 0, rotz + offset));
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
