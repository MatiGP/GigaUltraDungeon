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
    [TextArea(1, 3)]
    public string[] relicSmithDialogsPositive;
    [TextArea(1, 3)]
    public string[] relicSmithDialogsNegative;

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
            Dialog.instance.SkipDialog();
       }
    }

    public void ForgeRelic()
    {
        if (Inventory.instance.SpendGold(costOfReforging))
        {
            relicToReforge.Reroll();
            costOfReforging += costOfReforgingGain;
            costValueText.text = costOfReforging.ToString();
            Dialog.instance.OpenDialog("Nor'Zak", relicSmithDialogsPositive[Random.Range(0, relicSmithDialogsPositive.Length)]);
        }
        else
        {
            Dialog.instance.OpenDialog("Nor'Zak", relicSmithDialogsNegative[Random.Range(0, relicSmithDialogsNegative.Length)]);
        }
        
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
