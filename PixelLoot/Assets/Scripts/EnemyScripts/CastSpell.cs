using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : StateMachineBehaviour
{
    private GameObject whatToSpawn;
    public float startTimeBtwCasts;
    private float timeBtwCasts;
    private Transform playerPos;
    private Vector3 difference;
    private float rotz;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        whatToSpawn = animator.GetComponent<EnemyScript>().character.projectile;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerPos != null)
        {
            animator.SetFloat("distanceFromThePlayer", Vector2.Distance(animator.gameObject.transform.position, playerPos.position));
            difference = animator.transform.position - playerPos.position;
            rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        else
        {
            playerPos = null;
        }         

        if (timeBtwCasts <= 0 && animator.GetBool("playerSpotted"))
        {
            Instantiate(whatToSpawn, animator.transform.position, Quaternion.Euler(0f,0f, rotz+90));
            timeBtwCasts = startTimeBtwCasts;
        }
        else
        {
            timeBtwCasts -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {       
        animator.ResetTrigger("attack");
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
