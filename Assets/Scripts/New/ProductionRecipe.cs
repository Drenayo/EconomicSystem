using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductionRecipe:IProductionRecipe
{
    /// <summary>
    /// 该生产配方的利润
    /// </summary>
    public float Profit {
        get 
        {
            float inputProfit = 0;
            if (inputRes.Count == 0 || inputRes == null)
                return outputRes.Price;
            else
                foreach (var item in inputRes)
                {
                    inputProfit += item.Price;   
                }
            return outputRes.Price - inputProfit;
        }
        
    }



    [LabelText("输入资源")]
    public List<ResourceUnit> inputRes;
    [LabelText("输出资源")]
    public ResourceUnit outputRes;

    /// <summary>
    /// 生产工具
    /// </summary>
    public Resource productionTool;

}

/// <summary>
/// 资源单位
/// </summary>
[System.Serializable]
public struct ResourceUnit
{
    public ResourceUnit(Resource r,int n)
    {
        res =r; 
        resQuantity = n;
    }
    public float Price { get { return res.currPrice * resQuantity; } }

    /// <summary>
    /// 资源
    /// </summary>
    public Resource res;
    /// <summary>
    /// 数量
    /// </summary>
    public int resQuantity;
}


public static class UtilsEx
{
    public static void AddResource(this List<ResourceUnit> list,ResourceUnit newResource)
    {
        // 查找对应的Resource在inputRes中的位置
        int indexInInputRes = list.FindIndex(existingResource => existingResource.res.Equals(newResource.res));

        // 如果找到了对应的Resource
        if (indexInInputRes != -1)
        {
            // 创建新的ResourceUnit对象并替换原有的对象
            list[indexInInputRes] = new ResourceUnit
            {
                res = newResource.res,
                resQuantity = newResource.resQuantity + newResource.resQuantity
            };
        }
        else
        {
            // 创建新项并添加到inputRes中
            list.Add(newResource);
        }
    }

    public static void SubResource(this List<ResourceUnit> list,ResourceUnit subtractResource)
    {
        // 查找对应的Resource在inputRes中的位置
        int indexInInputRes = list.FindIndex(existingResource => existingResource.res.Equals(subtractResource.res));

        // 如果找到了对应的Resource
        if (indexInInputRes != -1)
        {
            // 减去数量
            list[indexInInputRes] = new ResourceUnit
            {
                res = subtractResource.res,
                resQuantity = subtractResource.resQuantity - subtractResource.resQuantity
            };

            // 如果减到零或以下，删除该项
            if (list[indexInInputRes].resQuantity <= 0)
            {
                list.RemoveAt(indexInInputRes);
            }
        }
        // 如果在inputRes中找不到对应的Resource，可以选择抛出异常或进行其他处理
    }
}
