using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Button leftButton, rightButton;
    public SlotUI slotUI;

    public int currentIndex; // 显示UI当前物品序号

    private void OnEnable()
    {
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;

    }

    private void OnDisable()
    {
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
    }

    private void OnUpdateUIEvent(ItemDetails itemDetails, int index)
    {
        if(itemDetails == null)
        {
            slotUI.SetEmpty();
            currentIndex = -1;
            leftButton.interactable = false;
            rightButton.interactable = false;
        }
        else
        {
            currentIndex = index;
            slotUI.SetItem(itemDetails);
            if(index>0)
            {
                leftButton.interactable = true;
            }
            if (index == -1)
            {
                rightButton.interactable = false;
                leftButton.interactable |= false;
            }
        }

    }


    /// <summary>
    /// 左右按钮Event事件
    /// </summary>
    /// <param name="amout"></param>
    public void SwitchItem(int amout)
    {
        var   index=currentIndex+amout;
        if(index < currentIndex)
        {
            leftButton.interactable = false;
            rightButton.interactable = true;
        }else if (index > currentIndex)
        {
            leftButton.interactable = true;
            rightButton.interactable = false;
        }
        else // 多余两个物体的情况
        {
            leftButton.interactable = true;
            rightButton.interactable = true;
        }
        EventHandler.CallChangeItemEvent(index);

    }



}
