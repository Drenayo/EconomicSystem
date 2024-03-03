using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEconomicManager
{
    /// <summary>
    /// 获取所有经济实体
    /// </summary>
    List<IEconomicUnit> GetAllEconomicEntity();

    /// <summary>
    /// 获取建筑列表
    /// </summary>
    List<IBuilding> GetBuildingList();

    /// <summary>
    /// 获取所有资源数据
    /// </summary>
    List<IResourceData> GetResourcesDataList();

    /// <summary>
    /// 获取某个资源的市场供应量
    /// </summary>
    int GetMarketSupply(int resID);

    /// <summary>
    /// 获取某个资源的市场需求量
    /// </summary>
    int GetMarketDemand(int resID);

    /// <summary>
    /// 得到某个资源的市场总量
    /// </summary>
    int GetMarketTotalQuantity(int resID);

    /// <summary>
    /// 获取指定天数的历史供应量
    /// </summary>
    /// <param name="hisDays">指定天数</param>
    Dictionary<int, List<int>> GetHistoricalSupply(int hisDays);

    /// <summary>
    /// 获取指定天数的历史需求量
    /// </summary>
    /// <param name="hisDays">指定天数</param>
    Dictionary<int, List<int>> GetHistoricalDemand(int hisDays);

    

}
