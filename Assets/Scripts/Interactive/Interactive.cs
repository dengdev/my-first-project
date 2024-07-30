using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public ItemName requireItem; // �ɽ���������Ҫ����Ʒ
    public bool isDone; // �ɽ��������Ƿ����
    
    public void CheckItem(ItemName itemname)
    {
        if(requireItem==itemname&& !isDone)
        {
            isDone = true;
            // ʹ�ò��Ƴ������е���Ʒ
            OnclickedAction();
            EventHandler.CallItemUsedEvent(itemname);
        }
    }

    /// <summary>
    /// Ĭ������ȷ����Ʒִ��
    /// </summary>
    protected virtual void OnclickedAction()
    {

    }

    public virtual void EmptyClicked()
    {
        Debug.Log("��û������ö�����Ҫ����Ʒ");
    }

}
