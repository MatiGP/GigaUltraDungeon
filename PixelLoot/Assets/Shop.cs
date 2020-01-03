using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    public static bool isShopUiOpen = false;
    [Space(5)]
    [Header("Shop Details")]
    [SerializeField] int basePriceCommon;
    [SerializeField] int basePriceMagic;
    [SerializeField] int basePriceRare;
    [SerializeField] int basePriceLegendary;
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
        shopInventory = new Relic[Random.Range(minRelicsInShop, maxRelicsInShop)];
        for(int i = 0; i < shopInventory.Length; i++)
        {
            shopInventory[i] = new Relic(relicSprites[Random.Range(0, relicSprites.Length)], "Relic " + (i + 1));
            shopInventory[i].Reroll();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
