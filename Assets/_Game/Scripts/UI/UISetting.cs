using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : UICanvas
{
    public void ContinueButton()
    {
        GameManager.ChangeState(GameState.GamePlay);
        Close(0);
    }
}
