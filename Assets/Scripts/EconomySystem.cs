using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

/// <summary>
/// 经济系统
/// </summary>
public class EconomySystem : MonoBehaviour
{
    public static EconomySystem instance;
    public MaterialUnit rtemp;
    private void Awake()
    {
        instance = this;

        Debug.Log(AdjustPrice(rtemp, 100 , rtemp.total));
    }

    /// <summary>
    /// 调整经济-每循环调用一次，自动调整市场价格
    /// </summary>
    public void AdjustEconomy() 
    {
        foreach (var item in EconomicManager.instance.allProductionUnits)
            item.currMaterialUnit = CalculateProductionMaxCost(item);

        foreach (var item in EconomicManager.instance.allMaterialUnits)
            item.price = AdjustPrice(item, CalculateDemand(item.ID) , CalculateSupply(item.ID));


        // 调整某个物品的价格之后再去计算新价格产生的影响
        Debug.Log("--------------------------------------");
    }

    private float AdjustPrice(MaterialUnit materialUnit, float quantityDemanded, float supplyQuantity)
    {
        
        // 根据供给和需求的关系调整价格
        float adjustedPrice = materialUnit.price;

        if (quantityDemanded > supplyQuantity)
        {
            // 需求大于供给，价格上涨
            adjustedPrice *= 1.1f;
        }
        else if (supplyQuantity > quantityDemanded)
        {
            // 供给大于需求，价格下降
            adjustedPrice *= 0.9f;
        }
        // 如果供需平衡，价格不变
        Debug.Log(materialUnit.gameObject.name + $"的需求量:{quantityDemanded}  供给量:{supplyQuantity} 调整价格:{adjustedPrice}");
        return adjustedPrice;
    }

    // 计算材料的市场需求量(输入材料ID)
    private float CalculateDemand(int id)
    {
        int demand = 0;
        foreach (var item in EconomicManager.instance.allProductionUnits)
        {
            if(item.currMaterialUnit != null && item.currMaterialUnit.productionTask.inputMaterialUnits.Count != 0)
                foreach(var inputMaterial in item.currMaterialUnit.productionTask.inputMaterialUnits)
                {
                    if(inputMaterial.materialUnit.ID == id)
                        demand += inputMaterial.neededMaterials;
                }
        }

        return demand;
    }

    // 计算材料的市场供应量
    private float CalculateSupply(int id)
    {
        int supply = 0;
        foreach (var item in EconomicManager.instance.allProductionUnits)
        {
            if (item.currMaterialUnit != null && item.currMaterialUnit.productionTask.outputMaterialUnit.materialUnit.ID == id)
                supply += item.currMaterialUnit.productionTask.outputMaterialUnit.neededMaterials;
        }
        return supply;
    }


    /// <summary>
    /// 计算材料调整后的价格(根据供需原则计算，需求大于供给则价格提升，反之亦然，遍历市场建筑上个循环对与某项材料的总需求度与材料供给量(农田供给原材料))
    /// 材料之前的供应链也要考虑到，顶层物品的涨跌规则，一层供应链的涨跌规则，二层供应链的涨跌规则
    /// </summary>
    /// <returns></returns>
    private float CalculateMaterialPrice()
    {
        // 顶层物品的涨跌规则
        // 价格变化百分比 = k * (供给/需求比 - 1)



        // 要考虑到存量的价格是不受到影响的

        return 0;
    }

    

    /// <summary>
    /// 计算建筑能获取最大利益的任务
    /// </summary>
    /// <param name="productionUnit"></param>
    /// <returns></returns>
    private MaterialUnit CalculateProductionMaxCost(ProductionUnit productionUnit)
    {
        MaterialUnit maxPriceTask = null;
        float maxTempNumber = 0;
        foreach (var item in productionUnit.materialUnits)
        {
            if (item.productionTask.CheckMaterialAvailability())
            {
                if (maxTempNumber < item.productionTask.TaskPrice && item.productionTask.TaskPrice > 0)
                {
                    maxPriceTask = item;
                    maxTempNumber = item.productionTask.TaskPrice;
                    //Debug.Log($"价格比较{transform.name}   max{maxPriceTask.productionTask.TaskPrice}  item{item.productionTask.TaskPrice} ");
                }
            }
        }
        return maxPriceTask;
    }
 
}
