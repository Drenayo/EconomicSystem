using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建筑接口
/// </summary>
public interface IBuilding
{
    /// <summary>
    /// 获取最大利润的生产配方
    /// </summary>
    IProductionRecipe GetMaxProfitProductionRecipe();
}
