using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源类
/// </summary>
public class Resource : MonoBehaviour, IEconomicUnit,IResource
{
    public int id;

    // 初始价格
    public float originalPrice;

    // 均衡价格
    public float balancePrice;

    // 当前价格
    public float currPrice;

    public void Loop()
    {
        // 
    }
}
