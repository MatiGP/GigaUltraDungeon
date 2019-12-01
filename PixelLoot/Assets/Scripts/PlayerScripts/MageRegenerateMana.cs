using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MageRegenerateMana : MonoBehaviour
{
    public int manaRegen;
    public int ticks;
    public float manaRegenCooldown;
    public ParticleSystem manaRegenEffect;
    public Image skillno1;
    private float currentManaRegenCooldown;
    private PlayerStats pstats;
    // Start is called before the first frame update
    void Start()
    {
        pstats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentManaRegenCooldown <= 0)
        {
            skillno1.fillAmount = 1;
            if (Input.GetKeyDown(KeyCode.Space))
            {               
                StartCoroutine(RegenerateMana());                
                currentManaRegenCooldown = manaRegenCooldown;
            }
            
        }
        else
        {
            currentManaRegenCooldown -= Time.deltaTime;
            skillno1.fillAmount = currentManaRegenCooldown / manaRegenCooldown;
        }
    }

    IEnumerator RegenerateMana()
    {        
        for (int i = 0; i < ticks; i++)
        {
            pstats.RestoreMana(manaRegen);
            Instantiate(manaRegenEffect, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.0f);
        }
    }

}
