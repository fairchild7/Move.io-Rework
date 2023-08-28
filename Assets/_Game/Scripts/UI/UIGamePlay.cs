using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : UICanvas
{
    [SerializeField] Text aliveText; 

    /*
    public void WinButton()
    {
        UIManager.Ins.OpenUI<Win>().score.text = Random.Range(100, 200).ToString();
        Close(0);
    }

    public void LoseButton()
    {
        UIManager.Ins.OpenUI<Lose>().score.text = Random.Range(0, 100).ToString(); 
        Close(0);
    }
    */

    public void SettingButton()
    {
        GameManager.ChangeState(GameState.Setting);
        UIManager.Ins.OpenUI<UISetting>();
    }

    public void UpdateAliveText(int alive)
    {
        aliveText.text = "Alive: " + alive.ToString();
    }
}
