using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public (ItemType, int) enumOrder;
    public Button button;
    public Image buttonBorder;
    public Image buttonImage;
    public Image lockImage;

    public void UpdateButton()
    {
        ItemState state = UIManager.Ins.GetUI<UISkin>().GetItemState(enumOrder.Item1, enumOrder.Item2);
        SetActiveLockImage(state);
        SetButtonBorder(state);
    }

    public void SetActiveLockImage(ItemState state)
    {
        if (state == ItemState.Unbought)
        {
            lockImage.gameObject.SetActive(true);
        }
        else
        {
            lockImage.gameObject.SetActive(false);
        }
    }

    public void SetButtonBorder(ItemState state)
    {
        if (state == ItemState.Equipped)
        {
            buttonBorder.color = Color.red;
        }
        else
        {
            buttonBorder.color = Color.white;
        }
    }

    public void OnSelect()
    {
        buttonBorder.color = Color.green;
    }
}
