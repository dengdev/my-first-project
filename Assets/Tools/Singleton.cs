using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ע�����ǳ��õĹ�������
/// Ϊ���е�Manager�������ṩһ�����͵ĵ���ģʽ
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance
    {
        get { return instance; }
    }

    protected virtual void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = (T)this; // ��ͬ����̳й����ģ����Լ��Ϸ��͵�T����������GameManager����MouseManager���㶼���ڵ�ǰ���������Ǹ���ı���
            //DontDestroyOnLoad(gameObject); // ���ֵ��������ڳ����л�ʱ��������
    }

    /// <summary>
    /// ��������������ص�ǰ������͵���ģʽ�Ƿ��Ѿ�������
    /// </summary>
    public static bool IsInitialized
    {
        get {return instance != null;} // ��Ϊ�շ��� True,����ǰ�������ģʽ�Ѿ������ɹ���
    }

    /// <summary>
    /// ���һ���������ж������ģʽ�Ļ�������Ҫ�������ٵ�
    /// </summary>
    protected virtual void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }
}
