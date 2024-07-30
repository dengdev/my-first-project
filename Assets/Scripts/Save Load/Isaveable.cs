public interface Isaveable
{
    /// <summary>
    /// 接口实例化
    /// </summary>
    void SaveableRegister()
    {
        SaveLoadManager.Instance.Register(this);
    }

    GameSaveData GeneratesaveData();
    void RestoreGameData(GameSaveData saveData);
}