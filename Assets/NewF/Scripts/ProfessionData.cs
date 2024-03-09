using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// 职业工作
/// </summary>
public class ProfessionData
{
    ///职业
    public int ProfessionID;//职业ID
    public string ProfessionName;//职业名称
    public int actionUnLock;//职业解锁行为
    public List<int> listProfessionInBuilding = new List<int>();//职业所在建筑
    public List<int> listProfessionLearnAbility = new List<int>();//职业可学习能力
    public List<int> listProfessionPreAbility = new List<int>();//职业升级得前置能力
    public int preProfessionID;//就职前置职业
    public Level professionLevel;
    /// 工作
    public string ClothName;//职业服装
    public List<int> ListJobAction = new List<int>();//工作行为
    public OccupationType occupationType; //职业类型

    public int baseWorkValue;//标准工作值
    [Header("工作专属家具")]
    public int workUseitemID;//工作行为家具，第一个为该职业独占家具


    [Header("工作公共家具")]
    public List<IDAndCount> listWorkPublicUseitemID;//工作行为作物
    [Header("工作专属作物")]
    public List<IDAndCount> listWorkPlantID;//工作行为作

    public bool isWorkOutBuilding;//是否室外工作（作用的家具或者作物不在建筑物内）


}