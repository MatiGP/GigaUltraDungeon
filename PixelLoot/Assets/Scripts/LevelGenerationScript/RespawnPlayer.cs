using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public GameObject[] playerCharacters;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(playerCharacters[PlayerPrefs.GetInt("selectedChar") - 1], new Vector3(0,0,0), Quaternion.identity);
        PlayerStats.instance.LoadState();
        PlayerStats.instance.GetComponentInChildren<CharacterStatsUI>().SetFloorText("BOSS");
    }

   
}
