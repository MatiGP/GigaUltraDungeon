using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RelicForge : MonoBehaviour
{
    public static RelicForge instance;
    public static bool isForgeUiOpen = false;
    private Relic relicToReforge;
    [SerializeField] GameObject forgeUI;
    [SerializeField] Button relicInTheForgeButton;
    [SerializeField] Button reforgeButton;
    private int costOfReforging = 5;
    public int costOfReforgingGain = 5;
    public TextMeshProUGUI costValueText;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isForgeUiOpen = true;
            forgeUI.SetActive(true);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isForgeUiOpen = false;
            forgeUI.SetActive(false);
        }
    }

    public void PutRelicInEQ()
    {
       if(Inventory.instance.addItem(relicToReforge))
       {
            relicInTheForgeButton.image.sprite = null;
            relicInTheForgeButton.gameObject.SetActive(false);
            reforgeButton.gameObject.SetActive(false);
       }
    }

    public void ForgeRelic()
    {
        relicToReforge.Reroll();
        costOfReforging += costOfReforgingGain;
        costValueText.text = costOfReforging.ToString();
    }

    public void PutRelicInTheForge(Relic relic)
    {
        relicToReforge = relic;
        relicInTheForgeButton.gameObject.SetActive(true);
        relicInTheForgeButton.image.sprite = relicToReforge.itemIcon;
        reforgeButton.gameObject.SetActive(true);
        costValueText.text = costOfReforging.ToString();

    }

  
}
