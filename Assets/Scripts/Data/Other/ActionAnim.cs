using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ActionCallback(ActionInst actionInst, ChDataInst chDataInst, bool isFinish);//动画返回行为函数
[System.Serializable]
public class ActionAnim
{
    public string ActionAnimStr;//行为动作文本
    public int otherUseTime;
    public string ActionOtherAnimStr;//行为动作文本
    public string ActionOtherRetuenAnimStr;//行为动作文本
    public Vector3 otherAnimPReturnPostionV3;//返回额外i之，人物访问时候赋值
    public ActionCallback actionCallback;//动画返回

    public ActionAnim(string ActionAnimStr, string ActionOtherAnimStr, string ActionOtherRetuenAnimStr)
    {
        this.ActionAnimStr = ActionAnimStr;
        this.ActionOtherAnimStr = ActionOtherAnimStr;
        this.ActionOtherRetuenAnimStr = ActionOtherRetuenAnimStr;


    }
}
public class ActionInst
{
}