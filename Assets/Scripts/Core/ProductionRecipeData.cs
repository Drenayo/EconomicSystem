using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生产配方数据
/// </summary>
[CreateAssetMenu(menuName = "生产图谱",fileName = "新生产图谱")]
public class ProductionRecipeData:ScriptableObject
{
    /// <summary>
    /// 该生产配方的纯利润
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

    // 生产配方ID
    public int id;

    // 生产配方名字
    public string prName;

    [LabelText("输入资源")]
    public List<ResourceUnit> inputRes;
    [LabelText("输出资源")]
    public ResourceUnit outputRes;
}
