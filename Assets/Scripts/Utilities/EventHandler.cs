using System;
using UnityEngine;

public static class EventHandler 
{
    public static event Action<ItemDetails, int> UpdateUIEvent;
    public static void CallUPdateUIEvent(ItemDetails itemDetails,int index)
    {
        UpdateUIEvent?.Invoke(itemDetails, index);
    }


    public static event Action BeforeSceneUnloadEvent; // ��ǰ����ж��֮ǰ
    public static void CallBeforeSceneUnloadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }

    public static event Action AfterSceneloadedEvent; // ��ǰ����ж��֮�� 
    public static void CallAfterSceneLoadedEvent()
    {
        AfterSceneloadedEvent?.Invoke();

    }

    // ע:���п���봫�����ݵ�����ִ�з����������ж��������Ҫͬʱִ�еĻ������Ƕ����������¼��ķ�����1.����Ҫ��λ�ú��и��¼��Ϳ���
    public static event Action<ItemDetails, bool> ItemSelectedEvent;
    public static void CallItemSelectedEvent(ItemDetails itemDetails,bool isselected)
    {
        ItemSelectedEvent?.Invoke(itemDetails, isselected);
    }


    // ʹ����ƷҪ�������¼�
    public static event Action<ItemName>  ItemUsedEvent;
    public static void CallItemUsedEvent(ItemName itemname)
    {
        ItemUsedEvent?.Invoke(itemname);
    }


    public static event Action<int> ChangeItemEvent;
    public static void CallChangeItemEvent(int index)
    {
        ChangeItemEvent?.Invoke(index);
    }

    public static event Action<string> ShowDialogueEvent;
    public static void CallShowDialogueEvent(string dialogue)
    {
        ShowDialogueEvent?.Invoke(dialogue);
    }

    public static event Action<GameState>GameStateChangeEvent;
    public static void CallGameStateChangeEvent(GameState gameState)
    {
        GameStateChangeEvent?.Invoke(gameState);
    }

    public static event Action CheckGameStateEvent;
    public static void CallCheckGameStateEvent()
    {
        CheckGameStateEvent?.Invoke();
    }

    public static event Action<string> GamePassEvent;
    public static void CallGamePassEvent(string gameName)
    {
        GamePassEvent?.Invoke(gameName);
    }

    public static event Action<int> StarNewGameEvent;
    public static void CallStarNewGameEvent(int gameWeek)
    {
        StarNewGameEvent?.Invoke(gameWeek);
    }
}
