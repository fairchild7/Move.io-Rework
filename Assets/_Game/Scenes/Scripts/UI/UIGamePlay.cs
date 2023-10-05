using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : UICanvas
{
    [SerializeField] Text aliveText;

    public FloatingJoystick joystick;

    public void SettingButton()
    {
        GameManager.ChangeState(GameState.Setting);
        UIManager.Ins.OpenUI<UISetting>();
    }

    public void UpdateAliveText(int alive)
    {
        aliveText.text = "Alive: " + alive.ToString();
    }

    public override void Close(float delayTime)
    {
        anim.SetTrigger(Constants.ANIM_EXIT);
        base.Close(delayTime);
    }
}
