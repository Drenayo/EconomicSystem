using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 建筑
/// </summary>
[System.Serializable]
public class Building : MonoBehaviour,IEconomicUnit,IBuilding
{
    #region 参数

    // 库存容量，雇员数量，ID，配方，  招募 NPC 计算工资 / 最低和最高上限
    [LabelText("建筑ID")]
    public int id;

    [LabelText("建筑名字")]
    public string buildingName;

    /// <summary>
    /// 最低可接受利润,后续考虑边际成本，该值可变动
    /// </summary>
    [LabelText("最低可接受利润")]
    public float minAcceptableProfit = 5;

    /// <summary>
    /// 利益最大化的误差值 *
    /// </summary>
    [LabelText("利益最大化的误差值*")]
    public float maxProfitError = 1;

    /// <summary>
    /// 是否正在招工  后续加上招工要求
    /// </summary>
    [LabelText("是否正在招工")]
    public bool isRecruiting = true;

    /// <summary>
    /// 建筑积蓄
    /// </summary>
    [LabelText("建筑积蓄")]
    public float deposit = 10000;

    [LabelText("建筑状态")]
    public string buildingState = string.Empty;

    [LabelText("岗位列表")]
    public List<Profession> professions = new List<Profession>();

    [LabelText("雇员列表")]
    public List<NPC> npcList = new List<NPC>();

    [LabelText("当前生产列表")]
    public List<ProductionRecipeData> currProductionRecipe = new List<ProductionRecipeData>();

    [LabelText("原材料库存单位")]
    public List<ResourceUnit> rawResourcesStock = new List<ResourceUnit>();

    [LabelText("产品库存单位")]
    public List<ResourceUnit> productStock = new List<ResourceUnit>();

    #endregion



    #region 公共方法
    private void Awake()
    {

    }


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
        foreach (var item in professions)
        {
            if (item.isRecruiting)
            {
                foreach (var graph in npc.acquiredGraph)
                {
                    if (item.professionData.recipePRList[0].id == graph.id)
                    {
                        if (item.EntryPost(npc))
                            npcList.Add(npc);
                        // else
                        //Debug.Log("加入失败");
                    }
                }
            }
        }
        // 检查是否继续招工
        CheckRecruitmentStatus();
    }

    /// <summary>
    /// 建筑循环
    /// </summary>
    public void Loop()
    {
        UpdateState();

        // 看看积蓄多不多，要不要扩充生产线，招工
        //CheckRecruitmentStatus();

        // 调整今日生产策略
        ModifyProductionPlan();

        // 根据今天的生产任务进货（考虑多进货,不要一天一进货） 
        StockUp();

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
        foreach (ProductionRecipeData recipe in currProductionRecipe)
        {
            foreach (ResourceUnit inputResource in recipe.inputRes)
            {
                int refCount = inputResource.resCount;
                deposit -= Market.Instance.BuyResources(inputResource.ID, ref refCount);
            }
        }
    }

    /// <summary>
    /// 检查招募状态
    /// </summary>
    private void CheckRecruitmentStatus()
    {
        // 检查是否继续招工
        foreach (var item in professions)
        {
            if (!item.isRecruiting)
                isRecruiting = false;
        }
        
        isRecruiting = true;
    }

    /// <summary>
    /// 调整生产策略
    /// </summary>
    private void ModifyProductionPlan()
    {
        // 生产最大利润的商品（允许误差，真正的商人做不到绝对利益最大化，大致有个方向即可）
        currProductionRecipe.Clear();


        //if (npcList[0].acquiredGraph[0].Profit > minAcceptableProfit)
        //    currProductionRecipe.Add(npcList[0].acquiredGraph[0]);
        //else
        //    buildingState = "利润过低";

        // 计算生产任务所得利润是否大于最低接收利润
        foreach (var item in professions)
        {
            // 检查岗位人数，检查该岗位的职业数据，一个岗位对应一个职业，虽然写成了List,但是设计思路是1：1
            for (int i = 0; i < item.postCount; i++)
            {
                if (item.professionData.recipePRList[0].Profit > minAcceptableProfit)
                    currProductionRecipe.Add(item.professionData.recipePRList[0]);
                else
                    buildingState = "利润过低";
            }
        }
    }

    private void UpdateState()
    {
        if (!npcList.Any())
            buildingState = "没有工人";
    }

    /// <summary>
    /// 买入原材料，增加库存材料
    /// </summary>
    private void StockUp()
    {
        // 先遍历一次，拿到进货总量
        List<ResourceUnit> stockUpList = new List<ResourceUnit>();
        foreach (ProductionRecipeData recipe in currProductionRecipe)
        {
            foreach (ResourceUnit inputResource in recipe.inputRes)
            {
                stockUpList.AddRes(inputResource.ID,inputResource.resCount);
            }
        }

        // 与库存对比，削去多余的进货量, （留百分之10%的余量：未实现）
        foreach (var item in rawResourcesStock)
        {
            stockUpList.SubRes(item.ID, item.resCount);
        }
        
        // 进货
        foreach (ResourceUnit stockUpUnit in stockUpList)
        {
            int resCountRefTemp = stockUpUnit.resCount;
            // 从市场购买资源
            float price = Market.Instance.BuyResources(stockUpUnit.ID, ref resCountRefTemp);
            if (price != 0 && (deposit-price) >= 0)
            {
                deposit -= price;
                rawResourcesStock.AddRes(stockUpUnit.ID, stockUpUnit.resCount);
            }
            else
            {
                buildingState = "无货源";
                //Debug.Log(gameObject.name + "进货失败");
            }
        }
    }

    /// <summary>
    /// 检查建筑是否可以生产
    /// </summary>
    private bool CheckProductionFeasibility(ProductionRecipeData pr)
    {
        foreach (var item in pr.inputRes)
        {
            if (!rawResourcesStock.ValidateResourceAvailability(item.ID, item.resCount))
            {
                //Debug.Log("原材料不足" + transform.name + "无法生产" + $"[{pr.outputRes.res.name}]");
                return false;
            }
        }
        buildingState = "原材料缺失";
        return true;
    }

    /// <summary>
    /// 生产操作
    /// </summary>
    private bool Produce(ProductionRecipeData pr)
    {
        // 检测建筑是否可以生产
        if (!CheckProductionFeasibility(pr))
            return false;

        // 减去原材料
        foreach (var item in pr.inputRes)
            rawResourcesStock.SubRes(item.ID, item.resCount);

        // 添加到本地库存
        productStock.AddRes(pr.outputRes.ID, pr.outputRes.resCount);
        // 推送到市场
        Market.Instance.PushResources(pr.outputRes.ID, pr.outputRes.resCount, id);

        buildingState = $"生产了{pr.outputRes.res.resName}";
        return true;
    }

    #endregion
}
