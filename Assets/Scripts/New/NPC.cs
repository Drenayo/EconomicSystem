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
    public int deposit;

    // 工作单位
    public Building building;

    // 所属工种 *
    public WorkType workType;

    public void Loop()
    {
        // 没工作就要找工作
        // 有工作就看看有没有更好的工作（考虑NPC的技能体系，某个领域呆的越久的技能值越高，防止NPC频繁跳槽，啥都能干）
        // 吃饭，下班消费（考虑NPC的喜好，家庭住址（只找附近的店铺））
        // 上班工作

        // 
        throw new System.NotImplementedException();
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