using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemState
{
    Unbought,
    Unequipped,
    Equipped,
}

public class DataManager : Singleton<DataManager>
{
    public List<string> nameData = new List<string>();
    public WeaponData weaponData;
    public HairDatas hairData;
    public PantsDatas pantsData;
    public ColorDatas colorData;

    private void Start()
    {
        if (PlayerPrefs.GetInt("currentColor") != 0)
        {
            SetColorState(0, ItemState.Unequipped);
        }
        else
        {
            SetColorState(0, ItemState.Equipped);
        }

        if (PlayerPrefs.GetInt("currentPants") != 0)
        {
            SetPantsState(0, ItemState.Unequipped);
        }
        else
        {
            SetPantsState(0, ItemState.Equipped);
        }

        if (PlayerPrefs.GetInt("currentHair") != 0)
        {
            SetHairState(0, ItemState.Unequipped);
        }
        else
        {
            SetHairState(0, ItemState.Equipped);
        }

        if (PlayerPrefs.GetInt("currentWeapon") != 0)
        {
            SetWeaponState(0, ItemState.Unequipped);
        }
        else
        {
            SetWeaponState(0, ItemState.Equipped);
        }
    }

    #region ItemData
    public void SaveCurrentHair(HairType hairType)
    {
        int current = PlayerPrefs.GetInt("currentHair");
        SetHairState((HairType)current, ItemState.Unequipped);
        PlayerPrefs.SetInt("currentHair", (int)hairType);
        SetHairState(hairType, ItemState.Equipped);
        PlayerPrefs.Save();
    }

    public void SaveCurrentPants(PantsType pantsType)
    {
        int current = PlayerPrefs.GetInt("currentPants");
        SetPantsState((PantsType)current, ItemState.Unequipped);
        PlayerPrefs.SetInt("currentPants", (int)pantsType);
        SetPantsState(pantsType, ItemState.Equipped);
        PlayerPrefs.Save();
    }

    public void SaveCurrentColor(ColorType colorType)
    {
        int current = PlayerPrefs.GetInt("currentColor");
        SetColorState((ColorType)current, ItemState.Unequipped);
        PlayerPrefs.SetInt("currentColor", (int)colorType);
        SetColorState(colorType, ItemState.Equipped);
        PlayerPrefs.Save();
    }

    public void SaveCurrentWeapon(WeaponType weaponType)
    {
        SetWeaponState(GetCurrentWeapon(), ItemState.Unequipped);
        PlayerPrefs.SetInt("currentWeapon", (int)weaponType);
        SetWeaponState(weaponType, ItemState.Equipped);
        PlayerPrefs.Save();
    }

    public WeaponType GetCurrentWeapon()
    {
        return (WeaponType)PlayerPrefs.GetInt("currentWeapon");
    }

    public void LoadPlayerData()
    {
        LevelManager.Ins.player.ChangeColor(colorData.GetColorData((ColorType)PlayerPrefs.GetInt("currentColor")));
        LevelManager.Ins.player.EquipPants((PantsType)PlayerPrefs.GetInt("currentPants"));
        LevelManager.Ins.player.EquipHair((HairType)PlayerPrefs.GetInt("currentHair"));
        LevelManager.Ins.player.EquipWeapon(GetCurrentWeapon());
    }

    public void SetHairState(HairType hairType, ItemState state)
    {
        PlayerPrefs.SetInt("hair" + hairType.ToString(), (int)state);
        PlayerPrefs.Save();
    }

    public void SetPantsState(PantsType pantsType, ItemState state)
    {
        PlayerPrefs.SetInt("pants" + pantsType.ToString(), (int)state);
        PlayerPrefs.Save();
    }

    public void SetColorState(ColorType colorType, ItemState state)
    {
        PlayerPrefs.SetInt("color" + colorType.ToString(), (int)state);
        PlayerPrefs.Save();
    }

    public void SetWeaponState(WeaponType weaponType, ItemState state)
    {
        PlayerPrefs.SetInt("weapon" + weaponType.ToString(), (int)state);
        PlayerPrefs.Save();
    }

    public ItemState GetHairState(HairType type)
    {
        return (ItemState)PlayerPrefs.GetInt("hair" + type.ToString());
    }

    public ItemState GetPantsState(PantsType type)
    {
        return (ItemState)PlayerPrefs.GetInt("pants" + type.ToString());
    }

    public ItemState GetColorState(ColorType type)
    {
        return (ItemState)PlayerPrefs.GetInt("color" + type.ToString());
    }

    public ItemState GetWeaponState(WeaponType type)
    {
        return (ItemState)PlayerPrefs.GetInt("weapon" + type.ToString());
    }
    #endregion

    public void SaveCurrentLevel(int level)
    {
        PlayerPrefs.SetInt(Constants.DATA_LEVEL, level);
        PlayerPrefs.Save();
    }

    public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt(Constants.DATA_LEVEL);
    }

    public void SaveCurrentCoin(int coin)
    {
        PlayerPrefs.SetInt(Constants.DATA_COIN, coin);
        PlayerPrefs.Save();
    }

    public int GetCurrentCoin()
    {
        return PlayerPrefs.GetInt(Constants.DATA_COIN);
    }
}
