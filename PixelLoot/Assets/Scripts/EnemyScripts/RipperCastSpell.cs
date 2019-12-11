using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipperCastSpell : StateMachineBehaviour
{
    public float offset = -90;
    public float startTimeBtwShots = 1f;
    public string projectileTag;
    private ObjectPooler pooler;

    private GameObject projectile;
    private Transform playerPos;
    private float timeBtwShots;
    private float rotz;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pooler = ObjectPooler.instance;
        playerPos = animator.GetComponent<Enemy>().playerPos;
        projectile = animator.GetComponent<Enemy>().enemyTemplate.projectileOrSummon;
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
            pooler.SpawnFromPool(projectileTag, animator.transform.position, Quaternion.Euler(0, 0, rotz + offset)).GetComponent<RipperProjectile>().SetRipperPos(animator.transform);
           // GameObject go = Instantiate(projectile, animator.transform.position, Quaternion.Euler(0, 0, rotz + offset));
           // go.GetComponent<RipperProjectile>().SetRipperPos(animator.transform); 
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    
}
