using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummoner : StateMachineBehaviour
{
    [SerializeField] float offset = -90;
    [SerializeField] float startTimeBtwShots = 1f;

    [SerializeField] GameObject[] projectiles;
    [SerializeField] string[] projectileTags;
    private ObjectPooler pooler;
    private Transform playerPos;
    private float timeBtwShots;
    private float rotz;

    [SerializeField] float startTimeBtwSummons;
    [SerializeField] float summonMaxDistanceX;
    [SerializeField] float summonMaxDistanceY;
    [SerializeField] float summonMinDistanceX;
    [SerializeField] float summonMinDistanceY;
    [SerializeField] LayerMask mask;

    [SerializeField] GameObject[] pets;
    private float timeBtwSummons;

    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pooler = ObjectPooler.instance;
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
        if(Roll() == 1)
        {
            if (timeBtwShots <= 0)
            {
                int projectileIndex = Random.Range(0, 2);
                if(projectileIndex == 0)
                {
                    for(int i = 0; i < 3; i++)
                    {
                        pooler.SpawnFromPool(projectileTags[projectileIndex], animator.transform.position, Quaternion.Euler(0, 0, rotz + offset + (-30 + i * 30))).GetComponent<RipperProjectile>().SetRipperPos(animator.transform);
                    }
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    for(int i = 0; i < 8; i++)
                    {
                        pooler.SpawnFromPool(projectileTags[projectileIndex], animator.transform.position, Quaternion.Euler(0, 0, rotz + offset + (i * 45)));                      
                    }
                    
                    timeBtwShots = startTimeBtwShots;
                }
                
            }
        }
        else
        {

            if (timeBtwSummons <= 0)
            {
                int petIndex = Random.Range(0, 2);

                float x = Random.Range(animator.transform.position.x + summonMinDistanceX, animator.transform.position.x + summonMaxDistanceX);
                float y = Random.Range(animator.transform.position.y + summonMinDistanceY, animator.transform.position.y + summonMaxDistanceY);

                RaycastHit2D hit = Physics2D.Linecast(animator.transform.position, new Vector2(x, y), mask);
                Debug.Log(hit.point);
                if (hit)
                {
                    Instantiate(pets[petIndex], new Vector2(hit.point.x, hit.point.y), Quaternion.identity);
                }
                else
                {
                    Instantiate(pets[petIndex], new Vector2(x, y), Quaternion.identity);
                }
               
                timeBtwSummons = startTimeBtwSummons;
            }            
        }

        timeBtwSummons -= Time.deltaTime;
        timeBtwShots -= Time.deltaTime;
    }

    int Roll()
    {
        return Random.Range(1, 3);
    }
}
