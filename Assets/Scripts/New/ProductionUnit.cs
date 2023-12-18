using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 生产单位
/// </summary>
public class ProductionUnit : Unit
{
    // 建筑存款
    public float balance;

    /// <summary>
    /// 雇佣NPC列表
    /// </summary>
    public List<NPCUnit> NPCUnits;

    // 可生产物品列表
    public List<MaterialUnit> materialUnits;

    // 当前生产物品
    public MaterialUnit currMaterialUnit;

    public override void Loop()
    {
        base.Loop();
        Produce();
    }

    /// <summary>
    /// 生产操作
    /// </summary>
    public void Produce()
    {
        // 为空表示利润不足
        if (currMaterialUnit)
            currMaterialUnit.productionTask.Produce();
    }

}

