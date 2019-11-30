using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightDagameReduction : MonoBehaviour
{
    public int damageReduction;
    public int damageReductionDuration;
    public float damageReductionCooldown;

    private PlayerStats pstats;
    private float currentDamageReductionCooldown;

    private void Start()
    {
        pstats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentDamageReductionCooldown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(ReduceDamageForDuration(damageReductionDuration));
                currentDamageReductionCooldown = damageReductionCooldown;
            }
        }
        else
        {
            currentDamageReductionCooldown -= Time.deltaTime;
        }
    }

    IEnumerator ReduceDamageForDuration(int duration)
    {
        ReduceDamage();            
        yield return new WaitForSeconds(duration);
        pstats.SetArmor(0);
    }

    void ReduceDamage()
    {
        pstats.SetArmor(damageReduction);
    }
}
