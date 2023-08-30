using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkin : UICanvas
{
    public void BackButton()
    {
        UIManager.Ins.OpenUI<UIMainMenu>();
        Close(0);
    }
}
