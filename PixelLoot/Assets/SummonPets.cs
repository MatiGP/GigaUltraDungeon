using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonPets : StateMachineBehaviour
{

    public float startTimeBtwSummons;
    public float summonMaxDistanceX;
    public float summonMaxDistanceY;
    public float summonMinDistanceX;
    public float summonMinDistanceY;

    private GameObject pet;
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

            Instantiate(pet, new Vector3(x, y, 0), Quaternion.identity);
            timeBtwSummons = startTimeBtwSummons;
        }
        else
        {
            timeBtwSummons -= Time.deltaTime;
        }
    }    
}
