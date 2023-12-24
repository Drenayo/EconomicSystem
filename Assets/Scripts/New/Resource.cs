using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源类
/// </summary>
public class Resource : MonoBehaviour, IEconomicUnit,IResource
{
    public void SetResource(int id, float originalPrice, float balancePrice, float currPrice, Building building)
    {
        this.id = id;
        this.originalPrice = originalPrice; 
        this.currPrice = currPrice;
        this.building = building;
    }

    public int id;

    // 初始价格
    public float originalPrice;

    // 均衡价格
    public float balancePrice;

    // 当前价格
    public float currPrice;

    // 产出地
    public Building building;

    public void Loop()
    {
        // 
    }
}
