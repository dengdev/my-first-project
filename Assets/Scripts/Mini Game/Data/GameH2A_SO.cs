using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameH2A_SO", menuName = "MiniGameData/GameH2A_SO")]
public class GameH2A_SO : ScriptableObject
{
    public string gameName;

    [Header("球的名字和对应的图片")]
    public List<BallDetails> ballDetailsList;

    [Header("游戏逻辑数据")]
    public List<Connections> lineConnections;
    public List<BallName> startBallOrder;

    public BallDetails GetBallDetails(BallName ballName)
    {
        return ballDetailsList.Find(b => b.BallName == ballName);
    }
}

[System.Serializable]
public class BallDetails
{
    public BallName BallName;
    public Sprite wrongSprite;
    public Sprite rightSprite;
}

[System.Serializable]
public class Connections
{
    public int from;
    public int to;
}
