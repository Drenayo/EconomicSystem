using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 经济系统控制中枢，与实际业务剥离，只处理经济系统抽象核心逻辑
/// </summary>
public class EconomySystem : MonoBehaviour
{
    public IEconomicManager economicManager;
    private void Awake()
    {
        economicManager = GetComponent<IEconomicManager>();
        // 本地调用经济系统的方式
        GameLoop.instance.gameLoopEvent.AddListener(EconomySystemLoop);    
    }

    /// <summary>
    /// 经济系统 Main-循环
    /// </summary>
    public void EconomySystemLoop()
    {
        // 经济实体循环
        InvokeEntityLoop();

        // 市场价格调整
        // 思路
        /*
         * 不应该以当日的供应量和需求了，也应该考虑日益增长的总库存量，以及未来一个月的平均消耗量
         * 拿到当日市场总量，拿到过去一个月的平均供应量和需求量，做一个预计算
         * 
         *      // 1.拿到当日需求量与供给量
                //     a.需大于供：当日库存-剩余需求，如果还大于供给，则调整价格
                //     b.供大于需：调整价格

                // 2.拿到当日需求量与供给量
                //     a.需大于供：当日库存-剩余需求，如果还大于供给，则调整价格
                //     b.供大于需：拿到近30天供给量平均数，对比需求，如果平均数都持续大于需求，则调整价格


    // 得到过去一个月的平均消耗量、平均供给量、平均库存量（作为趋势）
    // 这个由数据管理类提供，每次循环都拿到最近30天的数据存储起来，私有的（考虑获取一整年的供给量，可能水果在某月份供给较足，这个影响要考虑到）
    // 考虑库存水平，如果库存水平足够，即使当天需求超过供给，也可以选择稳定价格，因为库存可以满足需求。
    // 数据类只提供仅30天的平均供给量

    // 扩展工具类通过这个数据计算趋势

    // 将过去一个月的平均消耗量与供给量经过近十五天的趋势预计算出后一周的价格调整与资源分配（给建筑分配任务）
         * 
         * 
         */
        foreach (var item in EconomicManager.Instance.allResourceData)
        {
           item.currPrice = AdjustResPrice(item.id);
        }
    }

    // 调用经济实体循环
    private void InvokeEntityLoop()
    {
        foreach (var item in economicManager.GetAllEconomicEntity())
            item.Loop();
    }




    private float AdjustResPrice(int resID)
    {
        ResourceData resData = EconomicManager.Instance.GetResourceDataByID(resID);
        float price = AdjustPriceBasedOnSupplyDemand(resID,resData.currPrice, economicManager.CalculateDemand(resID), economicManager.CalculateSupply(resID));
        return price;
    }


    /// <summary>
    /// 根据供需关系调整价格
    /// </summary>
    private float NewAdjustPriceBasedOnSupplyDemand(float originalPrice, float quantityDemanded, float supplyQuantity)
    {
        float adjustedPrice = originalPrice; // 这里记得赋值，原始价格
        Debug.Log(adjustedPrice);
        // 设置供需比的阈值，低于这个阈值不进行价格调整
        float imbalanceThreshold = 0.2f;

        // 计算供需失衡比例
        float imbalanceRatio = (quantityDemanded - supplyQuantity) / Mathf.Max(quantityDemanded, supplyQuantity);

        // 只有在供需比高于阈值时才进行价格调整
        if (MathF.Abs(imbalanceRatio) > imbalanceThreshold)
        {
            // 根据供需失衡比例来影响涨跌的比例
            adjustedPrice += (adjustedPrice * imbalanceRatio / 4); // 上涨或下降的比例可以根据实际需求进行调整
        }

        // 如果供需平衡或低于阈值，价格不变
        // Debug.Log(materialUnit.gameObject.name + $"的需求量:{quantityDemanded} 供给量:{supplyQuantity} 调整价格:{adjustedPrice}");

        return adjustedPrice;
    }

    /// <summary>
    /// 根据供需关系调整价格
    /// </summary>
    /// <param name="resource">原始价格</param>
    /// <param name="quantityDemanded">需求量</param>
    /// <param name="supplyQuantity">供应量</param>
    /// <returns></returns>
    private float AdjustPriceBasedOnSupplyDemand(int resID,float originalPrice,float quantityDemanded, float supplyQuantity)
    {
        float adjustedPrice = originalPrice; // 这里记得赋值，原始价格
        // Debug.Log(adjustedPrice);
        // 设置供需比的阈值，低于这个阈值不进行价格调整
        float imbalanceThreshold = 0.2f;

        // 如果需求大于供给，需要减去市场库存
        if (quantityDemanded > supplyQuantity)
        {
            if (Market.Instance.dicMarketStock[resID].resCount - quantityDemanded >= 0)
                return adjustedPrice;
        }

        // 计算供需失衡比例
        float imbalanceRatio = (quantityDemanded - supplyQuantity) / Mathf.Max(quantityDemanded, supplyQuantity);

        // 只有在供需比高于阈值时才进行价格调整
        if (MathF.Abs(imbalanceRatio) > imbalanceThreshold)
        {
            // 根据供需失衡比例来影响涨跌的比例
            adjustedPrice += (adjustedPrice * imbalanceRatio / 4); // 上涨或下降的比例可以根据实际需求进行调整
        }

        // 如果供需平衡或低于阈值，价格不变
        Debug.Log(EconomicManager.Instance.GetResourceDataByID(resID).resName + $"的需求量:{quantityDemanded} 供给量:{supplyQuantity} 调整价格:{adjustedPrice}");

        return adjustedPrice;
    }

    // 调整行为策略
}
