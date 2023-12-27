using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 市场类  资源的索引，真正的售卖行为还是建筑类自身
/// </summary>
public class Market : MonoSingleton<Market>
{
    // 市场总库存
    private Dictionary<int, ResourceUnit> dicMarketStock;

    // 每个资源对应售卖建筑列表 （有哪些建筑在售卖）
    private Dictionary<int, List<Building>> dicBuildings;
    

    /// <summary>
    /// 从市场购买资源
    /// </summary>
    /// <returns>返回售价，-1为无货源</returns>
    public float BuyResources(int resID, int resCount)
    {
        if (dicMarketStock.ContainsKey(resID))
        {
            List<Building> buildings = new List<Building>();
            
            // 遍历建筑列表
            if (dicBuildings.TryGetValue(resID, out buildings))
                foreach (var item in dicBuildings)
                {

                }

        }
        else 
        { 
            // 市场上没货
            return -1;
        }

        return 0;
    }

    /// <summary>
    /// 市场资源列表
    /// </summary>
    public List<Resource> resources = new List<Resource>();

    /// <summary>
    /// 向市场添加资源
    /// </summary>
    public void AddResourceToMarket(ResourceUnit resUnit,Building building)
    {
        for (int i = 0; i < resUnit.resCount; i++)
        {
            Resource temp = resUnit.res;
            GameObject newObj = new GameObject();
            newObj.name = temp.name;
            newObj.transform.parent = EconomicManager.instance.ResourceParent;
            Resource newRes = newObj.AddComponent<Resource>();
            newRes.SetResource(temp.id, temp.originalPrice, temp.balancePrice, temp.currPrice, building);
            resources.Add(newRes);
        }
    }

    /// <summary>
    /// 从市场买入资源
    /// </summary>
    public bool BuyResourceFromMarket(int buyResID,int buyCount)
    {
        bool isBuy = false;
        for (int i = 0; i < resources.Count; i++)
        {
            if (buyResID == resources[i].id && buyCount > 0)
            {
                isBuy = true;

                // 提供该资源的NPC或建筑的金钱增加
                resources[i].building.deposit += resources[i].currPrice;

                // 市场资源减少
                Destroy(resources[i].gameObject);
                resources.Remove(null);
                buyCount--;
            }
        }

        return isBuy;
    }
}
