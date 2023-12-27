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
    /// 经济系统循环
    /// </summary>
    public void EconomySystemLoop()
    {
        Init();
        InvokeEntityLoop();
        
    }

    // 每次循环的初始化与数据获取
    private void Init()
    {
        foreach (var item in economicManager.GetResourceList())
        {
            AdjustPrice(item,economicManager.CalculateDemand(item),economicManager.CalculateSupply(item));
        }
    }

    // 调用经济实体循环
    private void InvokeEntityLoop()
    {
        foreach (var item in economicManager.GetAllEconomicEntity())
            item.Loop();
    }




    /// <summary>
    /// 根据供需关系调整资源价格
    /// </summary>
    /// <param name="resource">资源</param>
    /// <param name="quantityDemanded">需求量</param>
    /// <param name="supplyQuantity">供应量</param>
    /// <returns></returns>
    private float AdjustPrice(IResource resource, float quantityDemanded, float supplyQuantity)
    {
        float adjustedPrice = 0;

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
