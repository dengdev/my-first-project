using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string sceneFrom;

    public string sceneToGO; // Ҫǰ���ĳ���


    /// <summary>
    /// ����¼��������ʱ��ִ�з���
    /// </summary>
    public void TeleportToScene()
    {
        TransitionManager.Instance.Transition(sceneFrom, sceneToGO);
    }



}
