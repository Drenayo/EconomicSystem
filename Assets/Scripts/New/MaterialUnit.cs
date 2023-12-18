using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 材料单位
/// </summary>
public class MaterialUnit : Unit
{
    public int ID;
    /// <summary>
    /// 材料单价
    /// </summary>
    public float price;

    /// <summary>
    /// 材料总量
    /// </summary>
    public int total;

    // 生产所需材料，以及产出数量
    public ProductionTask productionTask;

    // 增加总量
    public int AddToTotal(int num)
    {
        return total += num;
    }

    // 减少总量
    public int SubToTotal(int num)
    {
        if (total - num < 0)
        {
            return -1;
        }
        return total -= num;
    }

    /// <summary>
    /// 上一次价格调整的总量
    /// </summary>
    private float preTotal;

    private void Start()
    {
        preTotal = total;
    }

    /// <summary>
    /// 调整价格的函数
    /// </summary>
    public void AdjustPrice()
    {
        if (total > 1)
        {
            // 计算涨幅
            float increasePercentage = (total - preTotal) / preTotal * 100;

            if (increasePercentage > 20)
            {
                // 涨幅超过20%，降低价格20%
                preTotal = total;
                price -= 0.2f * price;
            }
            else if (increasePercentage < -20)
            {
                // 总量下降超过20%，提高价格20%
                preTotal = total;
                price += 0.2f * price;
            }
        }
    }
}
