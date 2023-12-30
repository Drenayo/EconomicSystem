using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 资源类
/// </summary>
[CreateAssetMenu(menuName = "资源数据", fileName = "新资源")]
public class ResourceData : ScriptableObject
{
    // 资源ID
    public int id;

    // 资源名称
    public string resName;

    // 资源等级
    public Level level;

    [LabelText("初始价格")]
    public float originalPrice;

    [LabelText("均衡价格")]
    public float balancePrice;

    [LabelText("当前价格")]
    public float currPrice;
}


//public class ResourceData 
//{
    
//}
