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
    /// 获取某个资源的市场供应量
    /// </summary>
    int CalculateSupply(int resID);

    /// <summary>
    /// 获取某个资源的市场需求量
    /// </summary>
    int CalculateDemand(int resID);

}
