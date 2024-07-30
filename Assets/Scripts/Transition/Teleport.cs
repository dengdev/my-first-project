using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string sceneFrom;

    public string sceneToGO; // 要前往的场景


    /// <summary>
    /// 点击事件，点击的时候执行方法
    /// </summary>
    public void TeleportToScene()
    {
        TransitionManager.Instance.Transition(sceneFrom, sceneToGO);
    }



}
