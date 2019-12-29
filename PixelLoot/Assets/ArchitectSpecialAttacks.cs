using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchitectSpecialAttacks : MonoBehaviour
{
    public GameObject architectDamageReductionBubble;
    private ArchitectDialog ad;
    public int numOfWavesPhaseTwo = 2;
    public GameObject minion;
    public int minMinions;
    public int maxMinions;
    public Transform[] spawnPoints;

    private void Start()
    {
        ad = GetComponent<ArchitectDialog>();
    }  

    public void PhaseTwo()
    {
        GetComponent<Enemy>().DisableAttackAndMovement(true);
        GetComponent<Enemy>().CanBeDamaged(false);
        transform.position = new Vector2(-3.5f, 13);
        architectDamageReductionBubble.SetActive(true);
        StartCoroutine(phaseTwoManager());
    }

    public IEnumerator PhaseOne()
    {
        GetComponent<Enemy>().DisableAttackAndMovement(true);
        architectDamageReductionBubble.SetActive(true);
        GetComponent<Enemy>().CanBeDamaged(false);
        yield return new WaitForSeconds(8f);
        GetComponent<Enemy>().DisableAttackAndMovement(false);
        architectDamageReductionBubble.SetActive(false);
        GetComponent<Enemy>().CanBeDamaged(true);
    }

    IEnumerator phaseTwoManager()
    {
        yield return new WaitForSeconds(11f);

        for(int i = 0; i< numOfWavesPhaseTwo; i++)
        {
            SpawnMinions();
            yield return new WaitForSeconds(15f);
        }
        yield return new WaitForSeconds(3f);
        architectDamageReductionBubble.SetActive(false);
        GetComponent<Enemy>().CanBeDamaged(true);
        GetComponent<Enemy>().DisableAttackAndMovement(false);

    }

    public void PhaseThree()
    {
        for(int i = 0; i < 9; i++)
        {
            Inventory.instance.relicInventory.DisposeRelic(i);
        }
        for(int i = 0; i < 4; i++)
        {
            Inventory.instance.relicInventory.equiptedRelics.Unequip(i);
        }
        for (int i = 0; i < 9; i++)
        {
            Inventory.instance.relicInventory.DisposeRelic(i);
        }
    }

    void SpawnMinions()
    {
        int minionsToSpawn = Random.Range(minMinions, maxMinions + 1);

        for(int i = 0; i < minionsToSpawn; i++)
        {
            Instantiate(minion, new Vector3(spawnPoints[Random.Range(0, spawnPoints.Length)].position.x, 
                spawnPoints[Random.Range(0, spawnPoints.Length)].position.y, 0), Quaternion.identity);
        }
    }
}
