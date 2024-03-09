using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 职业实例（公司岗位）（一个岗位可以有很多人）
/// </summary>
[System.Serializable]
public class Profession
{
    // 岗位ID,和职业ID不一样
    public string id;

    // 岗位基本数据
    [LabelText("岗位职业数据")]
    public ProfessionData_ professionData;

    // 基本薪资数据在模板上，实际奖励加成薪资，在实例中

    [LabelText("最大人数(招聘人数)")]
    public int maxCount;

    // 当前岗位人数
    [LabelText("岗位人数")]
    public int postCount;

    public bool isRecruiting = true;

    // 当前岗位的NPC 内部命令
    public List<NPC> npcList = new List<NPC>();

    /// <summary>
    /// 入职
    /// </summary>
    public bool EntryPost(NPC npc)
    {
        bool bo = false;
        if (maxCount > postCount)
        {
            npcList.Add(npc);
            postCount++;
            bo = true;
        }


        if (maxCount > postCount)
        {
            isRecruiting = true;
        }
        else
        {
            isRecruiting = false;
        }

        return bo;
    }
}
