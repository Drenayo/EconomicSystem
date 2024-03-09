using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 职业类
/// </summary>
[CreateAssetMenu(menuName = "职业", fileName = "新职业")]
public class ProfessionData_:ScriptableObject
{
    // 职业ID
    public int id;
    /// <summary>
    /// 职业名称
    /// </summary>
    [LabelText("职业名称")]
    public string professionName;
    /// <summary>
    /// 职业等级
    /// </summary>
    public Level professionLevel;
    /// <summary>
    /// 基本薪资
    /// </summary>
    [LabelText("基本薪资")]
    public int jobMoney;
    /// <summary>
    /// 职业所需技能
    /// </summary>
    [LabelText("职业所需技能")]
    public List<ProductionRecipeData> recipePRList;
}
