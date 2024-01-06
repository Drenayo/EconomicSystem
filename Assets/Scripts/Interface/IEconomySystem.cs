using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 经济系统接口
/// </summary>
public interface IEconomySystem
{
    /// <summary>
    /// 获取建筑列表
    /// </summary>
    List<IEconomicUnit> GetBuildingList();

    /// <summary>
    /// 获取资源列表
    /// </summary>
    List<IEconomicUnit> GetResourceList();
}
