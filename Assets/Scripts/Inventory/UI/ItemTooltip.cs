using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    public Text itemNameText;

    public void updateItemname(ItemName itemName)
    {
        itemNameText.text = itemName switch
        {
            ItemName.Key => "信箱钥匙",
            ItemName.Ticket => "一张船票",
            _ => "默认"
        };
    }
}
