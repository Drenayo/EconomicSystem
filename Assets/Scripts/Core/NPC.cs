using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour,IEconomicUnit,INPC
{
    public int id;

    // 名字
    public string npcName;

    // 性别
    public GenderType genderType;

    // 年龄
    public int age;

    // 存款
    public float deposit;

    /// <summary>
    /// NPC工作单位
    /// </summary>
    public Building building;

    [LabelText("NPC学会的技能")]
    public List<ProductionRecipeData> acquiredGraph;

    // 所属工种 *
    public WorkType workType;

    /// <summary>
    /// 找工作
    /// </summary>
    private void FindJob()
    {
        foreach (var item in EconomicManager.Instance.GetBuildingList())
        {
            // 查看建筑是否还在招工
            Building buildingTemp = item as Building;
            if (buildingTemp.isRecruiting)
            {
                // 加入
                buildingTemp.JoinBuilding(this);
                building = buildingTemp;
            }
        }
    }

    public void Loop()
    {
        if (!building)
            FindJob();
        // 有工作就看看有没有更好的工作（考虑NPC的技能体系，某个领域呆的越久的技能值越高，防止NPC频繁跳槽，啥都能干）

        // 吃饭，下班（消费：消费在一天的循环中考虑多次消费）（考虑NPC的喜好，家庭住址（只找附近的店铺））

        // 上班工作

        //
    }


}


/// <summary>
/// 工种
/// </summary>
public enum WorkType 
{
    店主,
    雇员,
    待定
}


/// <summary>
/// 性别
/// </summary>
public enum GenderType
{
    Male,
    Female
}