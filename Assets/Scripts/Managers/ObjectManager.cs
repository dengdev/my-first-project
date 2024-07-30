using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour,Isaveable
{
    // 保存物品的获得情况
    private Dictionary<ItemName,bool> itemAvailableDict= new Dictionary<ItemName,bool>();
    // 保存场景可交互内容的互动情况
    private Dictionary<string,bool> interactiveStateDict= new Dictionary<string,bool>(); 

    private void OnEnable() 
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneloadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
        EventHandler.StarNewGameEvent += OnStarNewGameEvent;
    }
    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneloadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
        EventHandler.StarNewGameEvent -= OnStarNewGameEvent;
    }

    private void Start()
    {
        Isaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void OnStarNewGameEvent(int obj)
    {
        itemAvailableDict.Clear();
        interactiveStateDict.Clear();
    }

    private void OnBeforeSceneUnloadEvent()
    {
        // 如果已经在字典中则更新显示状态，不在则添加
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
            {
                itemAvailableDict.Add(item.itemName, true);
            }
            
        }
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
                interactiveStateDict[item.name] = item.isDone;
            else
                interactiveStateDict.Add(item.name, item.isDone);
        }


    }
    private void OnAfterSceneLoadedEvent()
    {
        // 如果已经在字典中则更新显示状态，不在则添加
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
            {
                itemAvailableDict.Add(item.itemName, true);
            }
            else
                item.gameObject.SetActive(itemAvailableDict[item.itemName]);
        }
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
                item.isDone=interactiveStateDict[item.name] ;
            else
                interactiveStateDict.Add(item.name, item.isDone);
        }
    }

    // 这方法只在拾取场景物品时更新，拾取代表着场景物品变成false
    private void OnUpdateUIEvent(ItemDetails itemdetails, int arg2)
    {
       if(itemdetails != null)
        {
            itemAvailableDict[itemdetails.itemName] = false;
        }
    }

    public GameSaveData GeneratesaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.itemAvailableDict = this.itemAvailableDict;
        saveData.interactiveStateDict = this.interactiveStateDict;

        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.itemAvailableDict=saveData.itemAvailableDict;
        this.interactiveStateDict=saveData.interactiveStateDict;
    }
}
