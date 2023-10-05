using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeapon : UICanvas
{
    [SerializeField] Transform weaponImagePos;
    [SerializeField] Button nextButton;
    [SerializeField] Button prevButton;
    [SerializeField] Text price;
    [SerializeField] Text nameText;
    [SerializeField] Text statusText;
    [SerializeField] Text effectText;
    [SerializeField] Button buyButton;
    [SerializeField] Button equipButton;
    [SerializeField] Text equipStateText;
    [SerializeField] Text coinText;

    private GameObject currentWeapon;
    private Weapon currentWeapData;
    private int maxWeaponCount => System.Enum.GetValues(typeof(WeaponType)).Length;

    public void OnLoad()
    {
        //update this to weapon effect - later feature
        statusText.gameObject.SetActive(false);
        effectText.gameObject.SetActive(false);

        UpdateShopWeapon(DataManager.Ins.GetCurrentWeapon());
        UpdateCoinText(DataManager.Ins.GetCurrentCoin());
    }

    public void BackButton()
    {
        UIManager.Ins.OpenUI<UIMainMenu>();
        Close(0);
    }

    public void NextButton()
    {
        int nextItem = (int)currentWeapData.GetWeaponType() + 1;
        UpdateShopWeapon((WeaponType)nextItem);
    }

    public void PreviousButton()
    {
        int prevItem = (int)currentWeapData.GetWeaponType() - 1;
        UpdateShopWeapon((WeaponType)prevItem);
    }

    public void BuyButton()
    {
        DataManager.Ins.SetWeaponState(currentWeapData.GetWeaponType(), ItemState.Unequipped);
        ChangeButtonState(ItemState.Unequipped);
    }

    public void EquipButton()
    {
        equipButton.interactable = false;
        equipStateText.text = "Equipped";
        LevelManager.Ins.player.EquipWeapon(currentWeapData.GetWeaponType());
        DataManager.Ins.SaveCurrentWeapon(currentWeapData.GetWeaponType());
    }

    private void UpdateShopWeapon(WeaponType weapType)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapData = DataManager.Ins.weaponData.GetWeapon(weapType);
        currentWeapon = Instantiate(currentWeapData.GetImage(), weaponImagePos);
        nameText.text = currentWeapData.weaponName;

        ItemState weaponState = DataManager.Ins.GetWeaponState(weapType);
        ChangeButtonState(weaponState);
        
        CheckButtonAvailable();
    }

    private void ChangeButtonState(ItemState weaponState)
    {
        switch (weaponState)
        {
            case ItemState.Unbought:
                price.text = currentWeapData.GetCost().ToString();
                buyButton.gameObject.SetActive(true);
                equipButton.gameObject.SetActive(false);
                if (DataManager.Ins.GetCurrentCoin() >= currentWeapData.GetCost())
                {
                    buyButton.interactable = true;
                }
                else
                {
                    buyButton.interactable = false;
                }
                return;
            case ItemState.Unequipped:
                buyButton.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(true);
                equipButton.interactable = true;
                equipStateText.text = "Select";
                return;
            case ItemState.Equipped:
                buyButton.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(true);
                equipButton.interactable = false;
                equipStateText.text = "Equipped";
                return;
        }
    }

    private void CheckButtonAvailable()
    {
        if (currentWeapData.GetWeaponType() == 0)
        {
            prevButton.interactable = false;
        }
        else
        {
            prevButton.interactable = true;
        }
        if ((int)currentWeapData.GetWeaponType() == maxWeaponCount - 1)
        {
            nextButton.interactable = false;
        }
        else
        {
            nextButton.interactable = true;
        }
    }

    public void UpdateCoinText(int coin)
    {
        coinText.text = coin.ToString();
    }
}
