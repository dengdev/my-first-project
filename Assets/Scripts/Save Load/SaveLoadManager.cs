using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    private string jsonFloder;
    private List<Isaveable> saveableList = new List<Isaveable>();
    private Dictionary<string,GameSaveData> saveableDict= new Dictionary<string,GameSaveData>();

    protected override void Awake()
    {
        // �����awake�̳��Ե���ģʽ������ģʽ�����awake�Ѿ������ˣ���Ȼ������
        base.Awake();
        jsonFloder = Application.persistentDataPath + "/SAVE/";
    }
    private void OnEnable()
    {
        EventHandler.StarNewGameEvent += OnStarNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.StarNewGameEvent -= OnStarNewGameEvent;
    }

    private void OnStarNewGameEvent(int obj)
    {
        var resultPath=jsonFloder + "data.sav";
        if(File.Exists(resultPath))
        {
            File.Delete(resultPath);
        }
    }

    public void Register(Isaveable saveable)
    {
        saveableList.Add(saveable);
    }

    /// <summary>
    /// �������
    /// </summary>
    public void Save()
    {
        saveableDict.Clear();

        foreach(var saveable in saveableList)
        {
            saveableDict.Add(saveable.GetType().Name, saveable.GeneratesaveData());
        }

        var resultPath = jsonFloder + "data.sav";

        var jsonData=JsonConvert.SerializeObject(saveableDict,Formatting.Indented);

        if (!File.Exists(resultPath))
        {
            Directory.CreateDirectory(jsonFloder);   
        }

        File.WriteAllText(resultPath, jsonData);
        Debug.Log(resultPath);
    }

    /// <summary>
    /// ��ȡ����
    /// </summary>
    public void Load()
    {
        var resultPath = jsonFloder + "data.sav";

        if (!File.Exists(resultPath)) return;

        var stringData=File.ReadAllText(resultPath);

        var jsonData=JsonConvert.DeserializeObject<Dictionary<string,GameSaveData>>(stringData);

        foreach (var saveable in saveableList)
        {
            saveable.RestoreGameData(jsonData[saveable.GetType().Name]);
        }

    }
}
