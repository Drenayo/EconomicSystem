using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 建筑
/// </summary>
public class Building : MonoBehaviour,IEconomicUnit,IBuilding
{
    // 库存容量，雇员数量，ID，配方，  招募 NPC 计算工资 / 最低和最高上限
    public int id;

    /// <summary>
    /// 最低可接受利润,后续考虑边际成本，该值可变动
    /// </summary>
    public float minAcceptableProfit = 5;

    /// <summary>
    /// 利益最大化的误差值 *
    /// </summary>
    public float maxProfitError = 1;

    /// <summary>
    /// 最大员工数量
    /// </summary>
    public int maxNPCNumber = 2;

    // 库存 应该是一个什么概念?，最大多少容量，能存放什么单位，各个单位多少上限     // 暂时先简单 *
    public int maxStock = 100;

    /// <summary>
    /// 是否正在招工  后续加上招工要求
    /// </summary>
    public bool isRecruiting = true;

    /// <summary>
    /// 建筑积蓄
    /// </summary>
    public float deposit = 10000;

    [LabelText("雇员列表")]
    public List<NPC> npcList;

    [LabelText("配方列表")]
    public List<ProductionRecipe> productionRecipeList;

    [LabelText("当前生产列表")]
    public List<ProductionRecipe> currProductionRecipe;

    [LabelText("销售的资源单位")]
    public List<ResourceUnit> saleResourceUnits;

    [LabelText("原材料库存单位")]
    public List<ResourceUnit> stockResourceUnits;

    
    /// <summary>
    /// 买入原材料，增加库存材料
    /// </summary>
    public void StockUp() 
    {
        // 每次按照当前生产列表进货进满，消耗的只剩三分之一的时候，再次按照当前生产列表进货
        // 考虑抽象出来一个市场，现阶段是直接遍历所有建筑

        foreach (ProductionRecipe recipe in currProductionRecipe)
        {
            // 对每个生产配方中的输入资源进行处理
            foreach (ResourceUnit inputResource in recipe.inputRes)
            {

                foreach (var bul in EconomicManager.instance.GetBuildingList())
                {
                        Building building=bul as Building;  

                        // 查找对应的Resource在saleResourceUnits中的位置
                        int indexInSale = building.saleResourceUnits.FindIndex(saleResource => saleResource.res.Equals(inputResource.res));

                        // 如果找到了对应的Resource
                        if (indexInSale != -1)
                        {
                            // 检查库存是否足够
                            if (building.saleResourceUnits[indexInSale].resQuantity >= inputResource.resQuantity)
                            {
                                // 购买资源
                               /// building.SellGoods()

                                // 将购买的资源加入到stockResourceUnits中
                                int indexInStock = stockResourceUnits.FindIndex(stockResource => stockResource.res.Equals(inputResource.res));

                                
                            }
                            else
                            {
                              // Debug.Log("库存不足");
                                // 处理库存不足的情况，你可以抛出异常或进行其他处理
                            }
                        }
                        else
                        { 
                             //Debug.Log("找不到资源");
                            // 处理找不到对应资源的情况，你可以抛出异常或进行其他处理
                        }
                    }
                

            }
        }


    }

    /// <summary>
    /// 生产操作
    /// </summary>
    public bool Produce(ProductionRecipe pr)
    {
        // 检测当前配方的原材料是否充足，充足就生产
        //foreach (ResourceUnit requiredResource in pr.inputRes)
        //{
        //    // 查找对应的Resource在stock中的位置
        //    int indexInStock = stockResourceUnits.FindIndex(stockResource => stockResource.res.Equals(requiredResource.res));

        //    // 如果找到了对应的Resource
        //    if (indexInStock != -1)
        //    {
        //        // 检查数量是否足够
        //        if (stockResourceUnits[indexInStock].resQuantity >= requiredResource.resQuantity)
        //        {
        //            // 材料及其数量足够，继续检查下一个
        //            continue;
        //        }
        //        else
        //        {
        //            // 数量不足，返回 false
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        // 在stock中找不到对应的Resource，返回 false
        //        return false;
        //    }
        //}

        // 所有材料及其数量都足够(减去原材料，生产出商品)
        saleResourceUnits.AddResource(pr.outputRes);
        
        return true;
    }

    /// <summary>
    /// 出售商品
    /// </summary>
    /// <param name="res"></param>
    public void SellGoods(Resource res)
    {
        saleResourceUnits.SubResource(new ResourceUnit(res,1));
        deposit += res.currPrice;
    }


    /// <summary>
    /// 招募NPC
    /// </summary>
    public void RecruitNPC()
    {
        // 检查是否可以扩大生产线

        // 检查是否继续招工
        if (npcList.Count < maxNPCNumber)
        {
            isRecruiting = true;
        }
        else
        {
            isRecruiting = false;
        }
        

        // 招工
    }

    /// <summary>
    /// 加入该建筑
    /// </summary>
    public void JoinBuilding(NPC npc)
    {
        npcList.Add(npc);

        // 检查是否继续招工
        if (npcList.Count < maxNPCNumber)
        {
            isRecruiting = true;
        }
        else
        {
            isRecruiting = false;
        }
    }


    /// <summary>
    /// 调整生产策略
    /// </summary>
    private void ModifyProductionPlan()
    {
        // 生产最大利润的商品（允许误差，真正的商人做不到绝对利益最大化，大致有个方向即可）

        // 目前策略，选出一种利益最大化的配方，然后所有NPC都生产这一种。

        // 根据NPC数量，选择产线数量，（目前策略，一个产线固定一个NPC）
        for (int i = 0; i < npcList.Count; i++)
        {
            currProductionRecipe.Clear();
            currProductionRecipe.Add(GetMaxProfitProductionRecipe());
        }
    }

    /// <summary>
    /// 获取最大利润的生产配方
    /// </summary>
    /// <returns></returns>
    private ProductionRecipe GetMaxProfitProductionRecipe()
    {
        float maxProfit = 0;
        ProductionRecipe productionRecipe = null;
        foreach (var item in productionRecipeList) 
        {
            if (maxProfit < item.Profit && item.Profit > minAcceptableProfit)
            {
                maxProfit = item.Profit;
                productionRecipe = item;
                //Debug.Log($"价格比较{transform.name}   max{maxPriceTask.productionTask.TaskPrice}  item{item.productionTask.TaskPrice} ");
            }
        }
        return productionRecipe;
    }

    public void Loop()
    {
        // 看看积蓄多不多，要不要扩充生产线，招工
        RecruitNPC();

        // 根据今天的生产任务进货（考虑多进货,不要一天一进货）
        //StockUp();

        // 调整生产策略
        ModifyProductionPlan();
        
        // 生产操作
        foreach (var item in currProductionRecipe)
            Produce(item);


                
    }
}
