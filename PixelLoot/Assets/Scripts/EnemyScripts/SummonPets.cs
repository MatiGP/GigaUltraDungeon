using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonPets : StateMachineBehaviour
{

    [SerializeField] float startTimeBtwSummons;
    [SerializeField] float summonMaxDistanceX;
    [SerializeField] float summonMaxDistanceY;
    [SerializeField] float summonMinDistanceX;
    [SerializeField] float summonMinDistanceY;
    private GameObject pet;
    [SerializeField] LayerMask mask;
    private float timeBtwSummons;    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pet = animator.GetComponent<Enemy>().enemyTemplate.projectileOrSummon;
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (timeBtwSummons <= 0)
        {
            float x = Random.Range(animator.transform.position.x + summonMinDistanceX, animator.transform.position.x + summonMaxDistanceX);
            float y = Random.Range(animator.transform.position.y + summonMinDistanceY, animator.transform.position.y + summonMaxDistanceY);

            RaycastHit2D hit = Physics2D.Linecast(animator.transform.position, new Vector2(x, y), mask);
            Debug.Log(hit.point);
            if(hit)
            {
                Instantiate(pet, new Vector2(hit.point.x, hit.point.y), Quaternion.identity);
            }
            else
            {
                Instantiate(pet, new Vector2(x,y), Quaternion.identity);
            }
           
            timeBtwSummons = startTimeBtwSummons;
        }
        else
        {
            timeBtwSummons -= Time.deltaTime;
        }
    }    
}
