using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>, Isaveable
{
    public ItemDataList_SO itemData;

    [SerializeField] private List<ItemName> itemList = new List<ItemName>();

    private void OnEnable()
    {
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
        EventHandler.ChangeItemEvent += OnChangerItemEvent;
        EventHandler.AfterSceneloadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.StarNewGameEvent += OnStarNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
        EventHandler.ChangeItemEvent -= OnChangerItemEvent;
        EventHandler.AfterSceneloadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.StarNewGameEvent -= OnStarNewGameEvent;
    }

    private void Start()
    {
        Isaveable savaable= this;
        savaable.SaveableRegister();
    }

    private void OnStarNewGameEvent(int obj)
    {
        itemList.Clear();
    }

    private void OnAfterSceneLoadedEvent()
    {
        if(itemList.Count==0) {
            EventHandler.CallUPdateUIEvent(null, -1);
        }
        else
        {
            for(int i = 0; i < itemList.Count; i++)
            {
                EventHandler.CallUPdateUIEvent(itemData.GetItemDetails(itemList[i]), i);
            }
        }
    }

    private void OnChangerItemEvent(int index)
    {
        if(index>=0 && index<itemList.Count) {
            ItemDetails item = itemData.GetItemDetails(itemList[index]);
            EventHandler.CallUPdateUIEvent(item, index);
        }
    }

    private void OnItemUsedEvent(ItemName itemName)
    {
        var index = GetItemIndex(itemName);
        itemList.RemoveAt(index);

        //ToDo 暂时实现单一物品使用效果
        if (itemList.Count == 0)
            EventHandler.CallUPdateUIEvent(null, -1);
    }


    public void AddItem(ItemName itemName)
    {
        if (!itemList.Contains(itemName))
        {
            itemList.Add(itemName);
            // UI对应显示
            EventHandler.CallUPdateUIEvent(itemData.GetItemDetails(itemName),itemList.Count-1);

        }
    }

    private int GetItemIndex(ItemName itemName)
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] == itemName)
                return i;
        }
        return -1;
    }

    public GameSaveData GeneratesaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.itemList = this.itemList;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.itemList = saveData.itemList;
    }
}
