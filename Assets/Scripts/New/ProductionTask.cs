using System.Collections.Generic;
using System;

/// <summary>
/// 生产任务
/// </summary>
[System.Serializable]
public struct ProductionTask
{
    /// <summary>
    /// 这个生产任务的利润
    /// </summary>
    public float TaskPrice
    {
        get
        {
            if (inputMaterialUnits.Count == 0 || inputMaterialUnits == null)
                return outputMaterialUnit.Price;

            float max = 0;
            foreach (var item in inputMaterialUnits)
            {
                max += item.Price;
            }

            // 产品总利润 - 源材料利用 = 纯利润（原型）
            return outputMaterialUnit.Price - max;
        }
    }

    /// <summary>
    /// 输入材料
    /// </summary>
    public List<NeededMaterial> inputMaterialUnits;
    /// <summary>
    /// 输出材料
    /// </summary>
    public NeededMaterial outputMaterialUnit;

    /// <summary>
    /// 生产操作
    /// </summary>
    public bool Produce()
    {
        if (inputMaterialUnits.Count != 0)
            foreach (var item in inputMaterialUnits)
            {
                item.Produce(MaterialProductionType.Sub);
            }
        outputMaterialUnit.Produce(MaterialProductionType.Add);
        
        return true;
    }

    /// <summary>
    /// 判断原材料是否足够
    /// </summary>
    public bool CheckMaterialAvailability()
    {
        if (inputMaterialUnits.Count != 0)
            foreach (var item in inputMaterialUnits)
            {
                if (!item.IsEnough)
                    return false;
            }
        return true;
    }
}

/// <summary>
/// 所需材料
/// </summary>
[System.Serializable]
public struct NeededMaterial
{
    // 价格
    public float Price { get { return materialUnit.price * (float)neededMaterials; } }
    // 材料是否充足
    public bool IsEnough { get { return materialUnit.total - neededMaterials >= 0;} }
    /// <summary>
    /// 材料
    /// </summary>
    public MaterialUnit materialUnit;
    /// <summary>
    /// 所需材料数量
    /// </summary>
    public int neededMaterials;

    // 材料生产操作
    public bool Produce(MaterialProductionType sourceType)
    {
        if (sourceType == MaterialProductionType.Sub)
        {
            materialUnit.SubToTotal(neededMaterials);
        }
        else if (sourceType == MaterialProductionType.Add)
        {
            materialUnit.AddToTotal(neededMaterials);
        }
        return true;
    }
}

// 材料生产操作
public enum MaterialProductionType
{
    Sub, Add
}
