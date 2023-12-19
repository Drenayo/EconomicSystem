using System.Collections;
using UnityEngine;

/// <summary>
/// 单位
/// </summary>
public class Unit : MonoBehaviour
{
    private void Start()
    {
        GameLoop.instance.gameLoopEvent.AddListener(Loop);
    }

    public virtual void Loop()
    {

    }
}

// 根据消耗的原材料的价格来影响选择产出的物品
// 加入NPC变量
// TODO 利润低于多少的时候，商店则不生产了
// 原材料为空，或低于所需材料时，对应原产品也无法生产，缺少材料。


// 无法影响产品了
// 原材料和产品库存量到多少的时候，单价会下降，取决于存储容量和是否有人购买

// 加入NPC 购买消耗商品
// 商品要分类，吃的商品要有饱食度、用的商品要符合NPC能使用的行为，

