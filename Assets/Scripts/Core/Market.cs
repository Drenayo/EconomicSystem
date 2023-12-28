using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 市场类  资源的索引，真正的售卖行为还是建筑类自身
/// </summary>
public class Market : MonoSingleton<Market>
{
    /// <summary>
    /// 市场总库存
    /// </summary>
    private Dictionary<int, ResourceUnit> dicMarketStock;

    /// <summary>
    /// 每个资源ID对应提供售卖服务的建筑列表 （有哪些建筑在售卖）
    /// </summary>
    private Dictionary<int, List<Building>> dicBuildings;

    /// <summary>
    /// 从市场购买资源
    /// </summary>
    /// <returns>返回总价，0为无货源</returns>
    public float BuyResources(int resID,ref int resCount)
    {
        if (dicMarketStock.ContainsKey(resID))
        {
            List<Building> buildings = new List<Building>();
            int index = 0;
            float price = 0;
            // 遍历建筑列表
            if (dicBuildings.TryGetValue(resID, out buildings))
                do
                    price += buildings[index++].SellResources(resID, ref resCount);
                while (resCount > 0);
            return price;
        }
        return 0;
    }

    /// <summary>
    /// 推送资源到市场(建筑每次生产出新产品，都会推送到市场，市场管理着所有产出资源，同时接受所有人的交易)
    /// </summary>
    public void PushResources(int resID, int resCount, int buildingID)
    {
        // 维护市场资源列表
        if (dicMarketStock.ContainsKey(resID))
        {
            ResourceUnit unit = dicMarketStock[resID];
            unit.resCount += resCount;
        }
        else
            dicMarketStock.Add(resID, new ResourceUnit(EconomicManager.Instance.GetResourceDataByID(resID), resCount));

        // 维护建筑列表
        if (dicBuildings.ContainsKey(resID))
        {
            // 查看建筑列表有没有这次的建筑，如果有就不加了
            if (!dicBuildings[resID].Contains(EconomicManager.Instance.GetBuildingByID(buildingID)))
                dicBuildings[resID].Add(EconomicManager.Instance.GetBuildingByID(buildingID));
        }
        else
        {
            dicBuildings.Add(resID, new List<Building>());
            dicBuildings[resID].Add(EconomicManager.Instance.GetBuildingByID(buildingID));
        }

    }
}
