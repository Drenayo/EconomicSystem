using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源单位
/// </summary>
[System.Serializable]
public class ResourceUnit
{
    public ResourceUnit(Resource r, int n)
    {
        res = r;
        resCount = n;
    }
    public float Price { get { return res.currPrice * resCount; } }
    public int ID { get { return res.id; } }

    /// <summary>
    /// 资源
    /// </summary>
    public Resource res;
    /// <summary>
    /// 数量
    /// </summary>
    public int resCount;

}
