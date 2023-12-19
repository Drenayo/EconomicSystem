using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "建筑")]
public class BuildingData : ScriptableObject
{
    // 库存容量，雇员数量，ID，配方，
    public List<ProductionTask> materialUnitDatas;

    // 招募 NPC 计算工资
}
