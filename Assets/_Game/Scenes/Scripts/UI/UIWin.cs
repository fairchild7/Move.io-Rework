using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWin : UICanvas
{
    [SerializeField] Text pointText;
    [SerializeField] Text congratText;
    [SerializeField] Text coinText;

    public void MainMenuButton()
    {
        GameManager.ChangeState(GameState.MainMenu);
        UIManager.Ins.OpenUI<UIMainMenu>();
        LevelManager.Ins.OnReset();
        LevelManager.Ins.OnLoadLevel(DataManager.Ins.GetCurrentLevel());
        Close(0);
    }

    public void UpdatePointText(int point)
    {
        pointText.text = point.ToString();
    }

    public void UpdateCongratText(int level)
    {
        congratText.text = "Congratulation, you have cleared level " + level + "!";
    }

    public void UpdateCoinText(int coin)
    {
        coinText.text = coin.ToString();
    }
}
