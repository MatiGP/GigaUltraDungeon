using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageRegenerateMana : MonoBehaviour
{
    public int manaRegen;
    public int ticks;
    public float manaRegenCooldown;

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
            if (Input.GetKeyDown(KeyCode.Space))
            {               
                StartCoroutine(RegenerateMana());                
                currentManaRegenCooldown = manaRegenCooldown;
            }
            
        }
        else
        {
            currentManaRegenCooldown -= Time.deltaTime;
        }
    }

    IEnumerator RegenerateMana()
    {        
        for (int i = 0; i < ticks; i++)
        {
            pstats.RestoreMana(manaRegen);        
            yield return new WaitForSeconds(1.0f);
        }
    }

}
