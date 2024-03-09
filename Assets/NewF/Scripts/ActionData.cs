using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class ActionData
{
    public int ActionID;//
    public string ActionName;//行为名称
    public ActionDoType ActionDoType;//行为类型，直接执行或者选择执行
    public OccupationType occupationType;//行为职业类型

    public int UseTime;//消耗时间
    public int UseEnergy;//消耗体力
    public int useMaxCount;//最大占用数量
    public bool isNeedProfessionAction;//需要职业参与行为,工业预定不需要职业，商业预定需要，影响是否扣除材料

    public List<ChLimitType> ListChinfoLimits = new List<ChLimitType>();//行为权限
    public Sprite actionIcon;

    public ActionAnim actionAnim;//行为动作

    [Header("物品制作类型")]

    public BookType BookType;
    public MakeType makeType;//制作类型
    [Header("增加属性值选择")]
    public ChValueType GetValueValueType;
    public GetValueGetType GetValueGetType;
}