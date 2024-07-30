using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour,Isaveable
{
    // ������Ʒ�Ļ�����
    private Dictionary<ItemName,bool> itemAvailableDict= new Dictionary<ItemName,bool>();
    // ���泡���ɽ������ݵĻ������
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
        // ����Ѿ����ֵ����������ʾ״̬�����������
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
        // ����Ѿ����ֵ����������ʾ״̬�����������
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

    // �ⷽ��ֻ��ʰȡ������Ʒʱ���£�ʰȡ�����ų�����Ʒ���false
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
