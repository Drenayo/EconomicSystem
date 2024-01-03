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
        float price = AdjustPriceBasedOnSupplyDemand(resData.originalPrice, economicManager.CalculateDemand(resID), economicManager.CalculateSupply(resID));
        return price;
    }

    /// <summary>
    /// 根据供需关系调整价格
    /// </summary>
    /// <param name="resource">原始价格</param>
    /// <param name="quantityDemanded">需求量</param>
    /// <param name="supplyQuantity">供应量</param>
    /// <returns></returns>
    private float AdjustPriceBasedOnSupplyDemand(float originalPrice,float quantityDemanded, float supplyQuantity)
    {
        float adjustedPrice = originalPrice; // 这里记得赋值，原始价格
        Debug.Log(adjustedPrice);
        // 设置供需比的阈值，低于这个阈值不进行价格调整
        float imbalanceThreshold = 0.2f;

        // 计算供需失衡比例
        float imbalanceRatio = (quantityDemanded - supplyQuantity) / Mathf.Max(quantityDemanded, supplyQuantity);

        // Debug.Log(imbalanceRatio + " " + imbalanceThreshold);
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

    // 调整行为策略
}
