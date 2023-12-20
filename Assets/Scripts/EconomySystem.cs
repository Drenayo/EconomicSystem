//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections.Generic;
//using System;

///// <summary>
///// 经济系统
///// </summary>
//public class EconomySystem : MonoBehaviour
//{
//    public static EconomySystem instance;
//    public MaterialUnit rtemp;
//    private void Awake()
//    {
//        instance = this;

//        Debug.Log(AdjustPrice(rtemp, 100 , rtemp.total));
//    }

//    ///// <summary>
//    ///// 调整经济-每循环调用一次，自动调整市场价格
//    ///// </summary>
//    //public void AdjustEconomy() 
//    //{
//    //    foreach (var item in EconomicManager.instance.allBuildingList)
//    //        item.currMaterialUnit = CalculateProductionMaxCost(item);

//    //    foreach (var item in EconomicManager.instance.allResourceList)
//    //        item.price = AdjustPrice(item, CalculateDemand(item.ID) , CalculateSupply(item.ID));


//    //    // 调整某个物品的价格之后再去计算新价格产生的影响
//    //    Debug.Log("--------------------------------------");
//    //}

//    private float AdjustPrice(MaterialUnit materialUnit, float quantityDemanded, float supplyQuantity)
//    {
//        float adjustedPrice = materialUnit.price;

//        // 设置供需比的阈值，低于这个阈值不进行价格调整
//        float imbalanceThreshold = 0.2f;

//        // 计算供需失衡比例
//        float imbalanceRatio = (quantityDemanded - supplyQuantity) / Mathf.Max(quantityDemanded, supplyQuantity);

//        Debug.Log(imbalanceRatio+" "+imbalanceThreshold);
//        // 只有在供需比高于阈值时才进行价格调整
//        if (MathF.Abs(imbalanceRatio) > imbalanceThreshold)
//        {
//            // 根据供需失衡比例来影响涨跌的比例
//            adjustedPrice += (adjustedPrice*imbalanceRatio / 4); // 上涨或下降的比例可以根据实际需求进行调整
//        }

//        // 如果供需平衡或低于阈值，价格不变
//        Debug.Log(materialUnit.gameObject.name + $"的需求量:{quantityDemanded} 供给量:{supplyQuantity} 调整价格:{adjustedPrice}");

//        return adjustedPrice;
//    }

//    // 计算材料的市场需求量(输入材料ID)
//    private float CalculateDemand(int id)
//    {
//        int demand = 0;
//        foreach (var item in EconomicManager.instance.allBuildingList)
//        {
//            if(item.currMaterialUnit != null && item.currMaterialUnit.productionTask.inputMaterialUnits.Count != 0)
//                foreach(var inputMaterial in item.currMaterialUnit.productionTask.inputMaterialUnits)
//                {
//                    if(inputMaterial.materialUnit.ID == id)
//                        demand += inputMaterial.neededMaterials;
//                }
//        }

//        return demand;
//    }

//    // 计算材料的市场供应量
//    private float CalculateSupply(int id)
//    {
//        int supply = 0;
//        foreach (var item in EconomicManager.instance.allBuildingList)
//        {
//            if (item.currMaterialUnit != null && item.currMaterialUnit.productionTask.outputMaterialUnit.materialUnit.ID == id)
//                supply += item.currMaterialUnit.productionTask.outputMaterialUnit.neededMaterials;
//        }
//        return supply;
//    }


//    /// <summary>
//    /// 计算材料调整后的价格(根据供需原则计算，需求大于供给则价格提升，反之亦然，遍历市场建筑上个循环对与某项材料的总需求度与材料供给量(农田供给原材料))
//    /// 材料之前的供应链也要考虑到，顶层物品的涨跌规则，一层供应链的涨跌规则，二层供应链的涨跌规则
//    /// 备注：上条提到的有关供应链价格，无需考虑到，因为受到供需关系影响，一旦某个物品价格上涨，对应的原材料价格也必然上涨。反之亦然
//    /// </summary>
//    /// <returns></returns>
//    private float CalculateMaterialPrice()
//    {
//        // 顶层物品的涨跌规则
//        // 价格变化百分比 = k * (供给/需求比 - 1)



//        // 要考虑到存量的价格是不受到影响的

//        // 考虑到不同生产环节的弹性（响应价格变动的敏感性）可能不同。如果某个环节的原材料有更多的替代品，那么价格上涨可能会受到一定的抑制。

//        // 没有外在因素干扰，供需失衡导致一个物品价格从100涨到1000，且这个物品的生产过程包括的原材料及生产链上的环节都没有其他因素干扰，那么在理论上，每个环节都会等比例涨价。

//        // 在一个封闭的系统中，涨价的信号会通过整个生产链传递。如果每个环节都在市场均衡中，那么在价格发生变动时，这个变动会沿着生产链向上传递。
//        return 0;
//    }

    

//    /// <summary>
//    /// 计算建筑能获取最大利益的任务
//    /// </summary>
//    /// <param name="productionUnit"></param>
//    /// <returns></returns>
//    private MaterialUnit CalculateProductionMaxCost(ProductionUnit productionUnit)
//    {
//        MaterialUnit maxPriceTask = null;
//        float maxTempNumber = 0;
//        foreach (var item in productionUnit.materialUnits)
//        {
//            if (item.productionTask.CheckMaterialAvailability())
//            {
//                if (maxTempNumber < item.productionTask.TaskPrice && item.productionTask.TaskPrice > 0)
//                {
//                    maxPriceTask = item;
//                    maxTempNumber = item.productionTask.TaskPrice;
//                    //Debug.Log($"价格比较{transform.name}   max{maxPriceTask.productionTask.TaskPrice}  item{item.productionTask.TaskPrice} ");
//                }
//            }
//        }
//        return maxPriceTask;
//    }
 
//}
