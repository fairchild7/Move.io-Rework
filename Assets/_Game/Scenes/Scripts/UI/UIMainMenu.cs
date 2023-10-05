using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UICanvas
{
    [SerializeField] Text coinText;

    public void OnLoad()
    {
        UpdateCoinText(DataManager.Ins.GetCurrentCoin());
    }

    public override void Open()
    {
        base.Open();
        OnLoad();
    }

    public void PlayButton()
    {
        UIManager.Ins.OpenUI<UIGamePlay>();
        GameManager.ChangeState(GameState.GamePlay);
        LevelManager.Ins.OnInit();
        Close(0.5f);
    }

    public void WeaponButton()
    {
        UIManager.Ins.OpenUI<UIWeapon>();
        UIManager.Ins.GetUI<UIWeapon>().OnLoad();
        Close(0.5f);
    }

    public void SkinButton()
    {
        GameManager.ChangeState(GameState.Shop);
        UIManager.Ins.OpenUI<UISkin>();
        UIManager.Ins.GetUI<UISkin>().OnLoad();
        Close(0.5f);
    }

    public void UpdateCoinText(int coin)
    {
        coinText.text = coin.ToString();
    }

    public override void Close(float delayTime)
    {
        anim.SetTrigger(Constants.ANIM_EXIT);
        base.Close(delayTime);
    }
}
