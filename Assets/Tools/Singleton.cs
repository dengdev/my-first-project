using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 注：这是常用的工具类型
/// 为所有的Manager，方便提供一个泛型的单例模式
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
            instance = (T)this; // 不同的类继承过来的，所以加上泛型的T，无论你是GameManager还是MouseManager，你都等于当前你所归属那个类的本体
            //DontDestroyOnLoad(gameObject); // 保持单例对象在场景切换时不被销毁
    }

    /// <summary>
    /// 用这个属性来返回当前这个泛型单例模式是否已经生成了
    /// </summary>
    public static bool IsInitialized
    {
        get {return instance != null;} // 不为空返回 True,代表当前这个单例模式已经被生成过了
    }

    /// <summary>
    /// 如果一个场景中有多个单例模式的话，是需要将它销毁的
    /// </summary>
    protected virtual void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }
}
