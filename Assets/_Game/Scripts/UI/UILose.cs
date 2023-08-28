using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILose : UICanvas
{
    [SerializeField] Text textRanking;
    [SerializeField] Text textKilled;

    public void MainMenuButton()
    {
        GameManager.ChangeState(GameState.MainMenu);
        UIManager.Ins.OpenUI<UIMainMenu>();
        Close(0);
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
