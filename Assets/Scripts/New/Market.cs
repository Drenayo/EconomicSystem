using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 市场类  资源的索引，真正的售卖行为还是建筑类自身
/// </summary>
public class Market : MonoBehaviour
{
    public static Market instance;
    private void Awake()
    {
        instance = this;
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
        for (int i = 0; i < resUnit.resQuantity; i++)
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
