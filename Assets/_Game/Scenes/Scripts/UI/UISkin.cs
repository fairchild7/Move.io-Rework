using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Pants,
    Hair,
    Color,
}

public class UISkin : UICanvas
{
    [SerializeField] Text priceText;
    [SerializeField] Text equipStateText;
    [SerializeField] Button equipButton;
    [SerializeField] Button buyButton;
    [SerializeField] Text shopNameText;
    [SerializeField] Text coinText;

    public Transform colorContent;
    public Transform hairContent;
    public Transform pantsContent;
    public ShopButton button;

    private ItemType currentType;
    private int currentEnum;

    private List<ShopButton> skinButtons = new List<ShopButton>();
    private List<ShopButton> pantsButtons = new List<ShopButton>();
    private List<ShopButton> hairButtons = new List<ShopButton>();

    private void Start()
    {
        InitItem();
    }

    public void OnLoad()
    {
        LevelManager.Ins.player.ChangeAnim(Constants.ANIM_DANCE_SKIN);
        equipButton.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);
        UpdateCoinText(DataManager.Ins.GetCurrentCoin());
    }

    public void InitItem()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(ColorType)).Length; i++)
        {
            int j = i;
            ShopButton b = Instantiate(button, colorContent);
            b.buttonImage.color = DataManager.Ins.colorData.GetColorData((ColorType)j).color;
            b.enumOrder = (ItemType.Color, j);
            b.UpdateButton();
            b.button.onClick.AddListener(delegate
            {
                currentType = ItemType.Color;
                currentEnum = j;
                LevelManager.Ins.player.ChangeColor(DataManager.Ins.colorData.GetColorData((ColorType)j));
                UpdateShopItem(GetItemState(ItemType.Color, j));
                RefreshShop();
                b.OnSelect();
            });
            skinButtons.Add(b);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(PantsType)).Length; i++)
        {
            int j = i;
            ShopButton b = Instantiate(button, pantsContent);
            b.buttonImage.sprite = DataManager.Ins.pantsData.GetPantsData((PantsType)j).GetIcon();
            b.enumOrder = (ItemType.Pants, j);
            b.UpdateButton();
            b.button.onClick.AddListener(delegate
            {
                currentType = ItemType.Pants;
                currentEnum = j;
                LevelManager.Ins.player.EquipPants((PantsType)j);
                UpdateShopItem(GetItemState(ItemType.Pants, j));
                RefreshShop();
                b.OnSelect();
            });
            pantsButtons.Add(b);
        }

        for (int i = 0; i < System.Enum.GetValues(typeof(HairType)).Length; i++)
        {
            int j = i;
            ShopButton b = Instantiate(button, hairContent);
            b.buttonImage.sprite = DataManager.Ins.hairData.GetHairData((HairType)j).GetIcon();
            b.enumOrder = (ItemType.Hair, j);
            b.UpdateButton();
            b.button.onClick.AddListener(delegate
            {
                currentType = ItemType.Hair;
                currentEnum = j;
                LevelManager.Ins.player.EquipHair((HairType)j);
                UpdateShopItem(GetItemState(ItemType.Hair, j));
                RefreshShop();
                b.OnSelect();
            });
            hairButtons.Add(b);
        }
    }

    public void BackButton()
    {
        GameManager.ChangeState(GameState.MainMenu);
        LevelManager.Ins.player.ChangeAnim(Constants.ANIM_IDLE);
        DataManager.Ins.LoadPlayerData();
        UIManager.Ins.OpenUI<UIMainMenu>();
        Close(0.5f);
    }

    public void BuyButton()
    {
        switch (currentType)
        {
            case ItemType.Color:
                DataManager.Ins.SetColorState((ColorType)currentEnum, ItemState.Unequipped);
                UpdateShopItem(ItemState.Unequipped);
                break;
            case ItemType.Pants:
                DataManager.Ins.SetPantsState((PantsType)currentEnum, ItemState.Unequipped);
                UpdateShopItem(ItemState.Unequipped);
                break;
            case ItemType.Hair:
                DataManager.Ins.SetHairState((HairType)currentEnum, ItemState.Unequipped);
                UpdateShopItem(ItemState.Unequipped);
                break;
        }
        RefreshShop();
        LevelManager.Ins.AddCoin(GetCurrentItemPrice() * -1);
        UpdateCoinText(DataManager.Ins.GetCurrentCoin());
    }

    public void EquipButton()
    {
        equipButton.interactable = false;
        equipStateText.text = "Equipped";
        switch (currentType)
        {
            case ItemType.Color:
                LevelManager.Ins.player.ChangeColor(DataManager.Ins.colorData.GetColorData((ColorType)currentEnum));
                DataManager.Ins.SaveCurrentColor((ColorType)currentEnum);
                break;
            case ItemType.Pants:
                LevelManager.Ins.player.EquipPants((PantsType)currentEnum);
                DataManager.Ins.SaveCurrentPants((PantsType)currentEnum);
                break;
            case ItemType.Hair:
                LevelManager.Ins.player.EquipHair((HairType)currentEnum);
                DataManager.Ins.SaveCurrentHair((HairType)currentEnum);
                break;
        }
        RefreshShop();
    }

    public void UpdateShopItem(ItemState state)
    {
        switch (state)
        {
            case ItemState.Unbought:
                buyButton.gameObject.SetActive(true);
                equipButton.gameObject.SetActive(false);
                priceText.text = GetCurrentItemPrice().ToString();
                if (DataManager.Ins.GetCurrentCoin() >= GetCurrentItemPrice())
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

    public int GetCurrentItemPrice()
    {
        switch (currentType)
        {
            case ItemType.Color:
                return DataManager.Ins.colorData.GetColorData((ColorType)currentEnum).cost;
            case ItemType.Hair:
                return DataManager.Ins.hairData.GetHairData((HairType)currentEnum).GetCost();
            case ItemType.Pants:
                return DataManager.Ins.pantsData.GetPantsData((PantsType)currentEnum).GetCost();
        }
        return 0;
    }

    public ItemState GetItemState(ItemType type, int enumType)
    {
        switch (type)
        {
            case ItemType.Color:
                return DataManager.Ins.GetColorState((ColorType)enumType);
            case ItemType.Hair:
                return DataManager.Ins.GetHairState((HairType)enumType);
            case ItemType.Pants:
                return DataManager.Ins.GetPantsState((PantsType)enumType);
        }
        return ItemState.Unbought;
    }

    public void UpdateCoinText(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void ChangeShopName(string name)
    {
        shopNameText.text = name;
    }

    public void HideButton()
    {
        buyButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
    }

    public void RefreshShop()
    {
        for (int i = 0; i < skinButtons.Count; i++)
        {
            skinButtons[i].UpdateButton();
        }
        for (int i = 0; i < pantsButtons.Count; i++)
        {
            pantsButtons[i].UpdateButton();
        }
        for (int i = 0; i < hairButtons.Count; i++)
        {
            hairButtons[i].UpdateButton();
        }
    }

    public override void Close(float delayTime)
    {
        anim.SetTrigger(Constants.ANIM_EXIT);
        base.Close(delayTime);
    }
}
