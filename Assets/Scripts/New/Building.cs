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
    public float minAcceptableProfit;

    [LabelText("雇员列表")]
    public List<NPC> npcList;

    [LabelText("配方列表")]
    public List<ProductionRecipe> productionRecipeList;

    [LabelText("当前生产列表")]
    public List<ProductionRecipe> currProductionRecipe;


    // 库存 应该是一个什么概念，最大多少容量，能存放什么单位，各个单位多少上限
    public int repertoryCap;

    public IProductionRecipe GetMaxProfitProductionRecipe()
    {
        float maxProfit = 0;
        IProductionRecipe productionRecipe = null;
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
        // 先根据今天的生产任务进货（考虑多进货,不要一天一进货）
        // 生产操作

        // 看看积蓄多不多，要不要扩充生产线，招工
    }
}
