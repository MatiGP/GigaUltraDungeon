using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightDagameReduction : MonoBehaviour
{
    public int damageReduction;
    public int damageReductionDuration;
    public float damageReductionCooldown;
    public Image skillno1;
    public GameObject damageReductionEffect;
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
            skillno1.fillAmount = 1;
            if (Input.GetKeyDown(KeyCode.Space))
            {              
                StartCoroutine(ReduceDamageForDuration(damageReductionDuration));
                currentDamageReductionCooldown = damageReductionCooldown;
            }
        }
        else
        {            
            currentDamageReductionCooldown -= Time.deltaTime;
            skillno1.fillAmount = currentDamageReductionCooldown / damageReductionCooldown;
        }
    }

    IEnumerator ReduceDamageForDuration(int duration)
    {
        ReduceDamage();
        damageReductionEffect.SetActive(true);
        yield return new WaitForSeconds(duration);
        pstats.SetArmor(0);
        damageReductionEffect.SetActive(false);
    }

    void ReduceDamage()
    {
        pstats.SetArmor(damageReduction);
    }
}
