using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRelicInventory : MonoBehaviour
{
    private bool isRelicInventoryOpen = false;
    public GameObject equiptedRelicsUI;
    public GameObject relicsUI;
    public GameObject relicInfo;
    public Image characterImage;
    public Image characterWeapon;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isRelicInventoryOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }

    private void Start()
    {
        characterImage.sprite = PlayerStats.instance.character.characterSprite;
        characterWeapon.sprite = Inventory.instance.weaponHolder.GetComponent<WeaponDamage>().weapon.itemIcon;
    }

    void Close()
    {
        equiptedRelicsUI.SetActive(false);
        relicsUI.SetActive(false);
        isRelicInventoryOpen = false;
        relicInfo.SetActive(false);
    }

    void Open()
    {
        equiptedRelicsUI.SetActive(true);
        relicsUI.SetActive(true);
        isRelicInventoryOpen = true;
    }
}
