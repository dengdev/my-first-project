using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    public RectTransform hand; // UI手的组件

    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

    private ItemName currentItem;

    private bool canClick;
    private bool holdItem;


    // 3.在周期中订阅事件,在方法中实现内容
    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedevent;
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedevent;
        EventHandler.ItemUsedEvent += OnItemUsedEvent;

    }
    private void Update()
    {
        canClick = ObjectAtMousePosition();

        if(hand.gameObject.activeInHierarchy)
        {
            hand.position=Input.mousePosition;  
        }
        if (InteractWithUI()) return;

        if(canClick && Input.GetMouseButtonDown(0))
        {
            // 检测鼠标互动情况
            ClickAction(ObjectAtMousePosition().gameObject);
        }
    }



    private void OnItemUsedEvent(ItemName name)
    {
        currentItem = ItemName.None;
        holdItem = false;
        hand.gameObject.SetActive(false);
    }

    private void OnItemSelectedevent(ItemDetails itemDetails, bool isSelected)
    {
        holdItem = isSelected;
        if(isSelected)
        {
            currentItem = itemDetails.itemName;
           

        }
        hand.gameObject.SetActive(holdItem);

    }

    private void ClickAction(GameObject clickObject)
    {
        switch (clickObject.tag)
        {
            case "Teleport":
                var teleport = clickObject.GetComponent<Teleport>();
                UnityEngine.Debug.Log("点击");
                teleport?.TeleportToScene();
                break;
            case "Item":
                var item = clickObject.GetComponent<Item>();
                item?.ItemClicked();
                break;
            case "Interactive":
                var interactive= clickObject.GetComponent<Interactive>();
                if(holdItem) 
                    interactive?.CheckItem(currentItem);
                else
                    interactive?.EmptyClicked();
                break;
        }
           
    }


    /// <summary>
    ///  检测鼠标点击范围的碰撞体
    /// </summary>
    /// <returns></returns>
    private Collider2D ObjectAtMousePosition()
    {
        return Physics2D.OverlapPoint(mouseWorldPos);
    }

    /// <summary>
    ///  判断是否跟UI互动
    /// </summary>
    /// <returns></returns>
    private bool InteractWithUI()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return true;
        return false;
    }
}
