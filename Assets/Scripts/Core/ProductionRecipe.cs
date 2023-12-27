using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductionRecipe:IProductionRecipe
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

    [LabelText("输入资源")]
    public List<ResourceUnit> inputRes;
    [LabelText("输出资源")]
    public ResourceUnit outputRes;
}


