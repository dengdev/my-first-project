using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemName itemName;

    public void ItemClicked()
    {
        InventoryManager.Instance.AddItem(itemName);
        // 添加到背包后隐藏物体
        this.gameObject.SetActive(false);
    }
}
