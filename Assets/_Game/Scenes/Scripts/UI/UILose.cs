using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILose : UICanvas
{
    [SerializeField] Text textRanking;
    [SerializeField] Text textKilled;

    public override void Setup()
    {
        base.Setup();
    }

    public override void Open()
    {
        base.Open();
    }

    public override void Close(float delayTime)
    {
        anim.SetTrigger(Constants.ANIM_EXIT);
        base.Close(delayTime);
    }

    public void MainMenuButton()
    {
        GameManager.ChangeState(GameState.MainMenu);
        UIManager.Ins.OpenUI<UIMainMenu>();
        LevelManager.Ins.OnReset();
        Close(0.5f);
    }

    public void ReviveButton()
    {
        UIManager.Ins.OpenUI<UIGamePlay>();
        LevelManager.Ins.Revive();
        GameManager.ChangeState(GameState.GamePlay);
        Close(0.5f);
    }

    public void UpdateTextRanking(int ranking)
    {
        textRanking.text = "#" + ranking;
    }

    public void UpdateTextKilled(string name)
    {
        textKilled.text = name;
    }
}
