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
            ItemName.Key => "����Կ��",
            ItemName.Ticket => "һ�Ŵ�Ʊ",
            _ => "Ĭ��"
        };
    }
}
