using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWeapon : UICanvas
{
    public void BackButton()
    {
        UIManager.Ins.OpenUI<UIMainMenu>();
        Close(0);
    }
}
