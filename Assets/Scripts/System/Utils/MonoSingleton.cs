using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 单例脚本
/// </summary>
public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = (T)obj.AddComponent(typeof(T));
                }
            }
            return instance;
        }
    }
}