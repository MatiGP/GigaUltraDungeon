using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] string shopOwnerName = "";
    [TextArea(1,3)]
    public string[] succesfulPurchaseSentence;
    [TextArea(1,3)]
    public string[] failedPurchaseSentenceNoMoney;
    [TextArea(1, 3)]
    public string[] failedPurchaseSentenceNoSpace;
    public static Shop instance;
    public static bool isShopUiOpen = false;
    public AudioClip purchaseSound;
    public Button[] shopButtons;
    public TextMeshProUGUI relicName;
    public TextMeshProUGUI[] relicInfoStat;
    public TextMeshProUGUI[] relicInfoStatValue;
    public TextMeshProUGUI relicPrice;
    public GameObject relicFromShopInfo;
    public GameObject relicShopUi;
    [Space(5)]
    [Header("Shop Details")]
    //COMMON, MAGIC, RARE, LEGENDARY
    [SerializeField] int[] prices;
    public Sprite[] relicSprites;
    public int minRelicsInShop;
    public int maxRelicsInShop;

    private Relic[] shopInventory;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        shopInventory = new Relic[Random.Range(minRelicsInShop, maxRelicsInShop+1)];
        for(int i = 0; i < shopInventory.Length; i++)
        {
            shopInventory[i] = new Relic(relicSprites[Random.Range(0, relicSprites.Length)], "Relicw");
            shopInventory[i].Reroll();
            SetupShopUI(i);
        }

        
    }

    void SetupShopUI(int index)
    {
        shopButtons[index].image.sprite = shopInventory[index].itemIcon;
        shopButtons[index].gameObject.SetActive(true);
    }

    public void BuyRelic(int index)
    {
        if (Inventory.instance.HasRelicSpace())
        {
            if (Inventory.instance.SpendGold(prices[(int)shopInventory[index].rarity]))
            {

                SoundManager.instance.PlaySoundEffect(purchaseSound);
                Inventory.instance.addItem(shopInventory[index]);
                Dialog.instance.OpenDialog(shopOwnerName, succesfulPurchaseSentence[Random.Range(0, 3)]);

                shopButtons[index].gameObject.SetActive(false);
                shopInventory[index] = null;              
            }
            else
            {
                Dialog.instance.OpenDialog(shopOwnerName, failedPurchaseSentenceNoMoney[Random.Range(0, 3)]);
            }
        }
        else
        {
            Dialog.instance.OpenDialog(shopOwnerName, failedPurchaseSentenceNoSpace[Random.Range(0, 2)]);
        }

        
    }

    public void ShowInformationsAboutRelicInShop(int index)
    {
        relicFromShopInfo.SetActive(true);

        relicName.text = shopInventory[index].itemName;
        SwapColor(shopInventory[index]);

        for (int i = 0; i < shopInventory[index].affectedStats.Length; i++)
        {
            relicInfoStat[i].text = shopInventory[index].affectedStats[i].ToString();
            relicInfoStatValue[i].text = shopInventory[index].values[i].ToString();
        }

        relicPrice.text = prices[(int)shopInventory[index].rarity].ToString();
    }

    public void HideInformationsAboutRelicInShop()
    {
        relicFromShopInfo.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            relicInfoStat[i].text = "";
            relicInfoStatValue[i].text = "";
        }
    }

    public void SellRelic(Relic relic)
    {
        Inventory.instance.AddGold(prices[(int)relic.rarity]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Inventory.instance.relicInventory.SwapSpriteSell();
            OpenShop();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Inventory.instance.relicInventory.SwapSpriteDispose();
            CloseShop();
        }
    }

    void OpenShop()
    {
        relicShopUi.SetActive(true);
        isShopUiOpen = true;
    }

    void CloseShop()
    {
        relicShopUi.SetActive(false);
        relicFromShopInfo.SetActive(false);
        isShopUiOpen = false;
    }

    void SwapColor(Relic relic)
    {
        switch (relic.rarity)
        {
            case RelicRarity.COMMON:
                relicName.color = Color.white;
                break;
            case RelicRarity.LEGENDARY:
                relicName.color = new Color32(255, 102, 0, 255);
                break;
            case RelicRarity.MAGIC:
                relicName.color = new Color32(92, 214, 92, 255);
                break;
            case RelicRarity.RARE:
                relicName.color = Color.yellow;
                break;
        }
    }
}
