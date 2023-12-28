using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// TODO 市场，建筑从市场进货，放到市场上的才会被售卖，目前默认生产的物品就是被放到市场售卖的，市场是时刻更新的，保证每个建筑最新产出可以投放到市场上

/// <summary>
/// 建筑
/// </summary>
public class Building : MonoBehaviour,IEconomicUnit,IBuilding
{
    #region 参数

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
    public int maxNPCNumber = 3;

    // 库存 应该是一个什么概念?，最大多少容量，能存放什么单位，各个单位多少上限     // 暂时先简单 * 暂时没用到
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

    [LabelText("原材料库存单位")]
    public List<ResourceUnit> rawResourcesStock;

    [LabelText("产品库存单位")]
    public List<ResourceUnit> productStock;

    #endregion



    #region 公共方法
    /// <summary>
    /// 卖出资源
    /// </summary>
    /// <returns>返回价格，无货源返回0</returns>
    public float SellResources(int resID, ref int resCount)
    {
        if (productStock != null && productStock.Count != 0)
        {
            if (productStock.HasID(resID))
            {
                ResourceUnit resUnit = productStock.GetIDRes(resID);
                float price = 0;
                // 库存满足
                if (resUnit.resCount >= resCount)
                {
                    price = resUnit.res.currPrice * resCount;
                    resUnit.resCount -= resCount;
                    resCount = 0;
                    if (resUnit.resCount == 0)
                        productStock.Remove(resUnit);
                }
                else
                {
                    price = resUnit.Price;
                    resCount -= resUnit.resCount;
                    resUnit.resCount = 0;
                    productStock.Remove(resUnit);
                }
                deposit += price;
                return price;
            }
        }
        return 0;
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
    /// 建筑循环
    /// </summary>
    public void Loop()
    {
        // 看看积蓄多不多，要不要扩充生产线，招工
        RecruitNPC();

        // 根据今天的生产任务进货（考虑多进货,不要一天一进货） 
        StockUp();

        // 调整生产策略
        ModifyProductionPlan();

        // 生产操作
        foreach (var item in currProductionRecipe)
            Produce(item);

    }
    #endregion



    #region 私有方法

    /// <summary>
    /// 购买资源
    /// </summary>
    private void BuyResources()
    {
        foreach (ProductionRecipe recipe in currProductionRecipe)
        {
            foreach (ResourceUnit inputResource in recipe.inputRes)
            {
                int refCount = inputResource.resCount;
                deposit -= Market.Instance.BuyResources(inputResource.ID, ref refCount);
            }
        }
    }

    /// <summary>
    /// 招募NPC
    /// </summary>
    private void RecruitNPC()
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
    /// 调整生产策略
    /// </summary>
    private void ModifyProductionPlan()
    {
        // 生产最大利润的商品（允许误差，真正的商人做不到绝对利益最大化，大致有个方向即可）

        // 目前策略，选出一种利益最大化的配方，然后所有NPC都生产这一种。

        // 根据NPC数量，选择产线数量，（目前策略，一个产线固定一个NPC）
        currProductionRecipe.Clear();

        for (int i = 0; i < npcList.Count; i++)
        {
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

    /// <summary>
    /// 买入原材料，增加库存材料
    /// </summary>
    private void StockUp()
    {
        // 新写法: 早晨进货，全部进够生产三次的货
        foreach (ProductionRecipe recipe in currProductionRecipe)
        {
            foreach (ResourceUnit inputResource in recipe.inputRes)
            {
                float price = Market.Instance.BuyResources(inputResource.ID, ref inputResource.resCount);
                if (price != 0 && (deposit-price) >= 0)
                {
                    deposit -= price;
                    rawResourcesStock.AddRes(inputResource.ID, inputResource.resCount);
                }
                else
                    Debug.Log(gameObject.name + "进货失败");

            }
        }
    }

    /// <summary>
    /// 检查生产配方所需原材料是否充足
    /// </summary>
    private bool IsRawResourceSufficient(ProductionRecipe pr)
    {
        foreach (var item in pr.inputRes)
        {
            if (!rawResourcesStock.ValidateResourceAvailability(item.ID, item.resCount))
                return false;
        }
        return true;
    }

    /// <summary>
    /// 生产操作
    /// </summary>
    private bool Produce(ProductionRecipe pr)
    {
        // 检测当前配方的原材料是否充足，充足就生产
        if(!IsRawResourceSufficient(pr))
            return false;

        // 减去原材料
        foreach (var item in pr.inputRes)
            rawResourcesStock.SubRes(item.ID, item.resCount);

        // 添加到本地库存
        productStock.AddRes(pr.outputRes.ID, pr.outputRes.resCount);
        // 推送到市场
        Market.Instance.PushResources(pr.outputRes.ID, pr.outputRes.resCount, id);
        return true;
    }

    #endregion
}
