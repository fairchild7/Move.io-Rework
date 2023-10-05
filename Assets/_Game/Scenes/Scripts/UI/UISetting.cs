using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : UICanvas
{
    public void ContinueButton()
    {
        GameManager.ChangeState(GameState.GamePlay);
        Close(0.5f);
    }

    public void MainMenuButton()
    {
        GameManager.ChangeState(GameState.MainMenu);
        UIManager.Ins.CloseUI<UIGamePlay>(0.5f);
        LevelManager.Ins.OnReset();
        UIManager.Ins.OpenUI<UIMainMenu>();
        Close(0.5f);
    }

    public override void Close(float delayTime)
    {
        anim.SetTrigger(Constants.ANIM_EXIT);
        base.Close(delayTime);
    }
}
