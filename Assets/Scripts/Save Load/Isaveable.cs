public interface Isaveable
{
    /// <summary>
    /// �ӿ�ʵ����
    /// </summary>
    void SaveableRegister()
    {
        SaveLoadManager.Instance.Register(this);
    }

    GameSaveData GeneratesaveData();
    void RestoreGameData(GameSaveData saveData);
}