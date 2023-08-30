using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    public void PlayButton()
    {
        UIManager.Ins.OpenUI<UIGamePlay>();
        GameManager.ChangeState(GameState.GamePlay);
        LevelManager.Ins.OnInit();
        Close(0);
    }

    public void WeaponButton()
    {
        UIManager.Ins.OpenUI<UIWeapon>();
        Close(0);
    }

    public void SkinButton()
    {
        UIManager.Ins.OpenUI<UISkin>();
        Close(0);
    }
}
