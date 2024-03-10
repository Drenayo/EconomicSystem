using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEconomicManager
{
    /// <summary>
    /// 获取建筑列表
    /// </summary>
    List<BuildingData> GetBuildingDataList();

    /// <summary>
    /// 获取所有物品数据
    /// </summary>
    List<ItemData> GetItemDataList();

    /// <summary>
    /// 获取所有NPC数据
    /// </summary>
    List<ChDataInst> GetNPCDataList();

    /// <summary>
    /// 获取某个物品的市场（总）供应量
    /// </summary>
    int GetMarketSupply(int itemID);

    /// <summary>
    /// 获取某个物品的市场（总）需求量
    /// </summary>
    int GetMarketDemand(int itemID);

    /// <summary>
    /// 得到某个物品的市场总量
    /// </summary>
    int GetMarketTotalQuantity(int itemID);

    ///// <summary>
    ///// 获取指定天数的历史供应量
    ///// </summary>
    ///// <param name="hisDays">指定天数</param>
    //Dictionary<int, List<int>> GetHistoricalSupply(int hisDays);

    ///// <summary>
    ///// 获取指定天数的历史需求量
    ///// </summary>
    ///// <param name="hisDays">指定天数</param>
    //Dictionary<int, List<int>> GetHistoricalDemand(int hisDays);

    ///// <summary>
    ///// 获取所有经济实体
    ///// </summary>
    //List<IEconomicUnit> GetAllEconomicEntity();

}
