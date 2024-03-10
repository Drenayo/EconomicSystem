using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public int itemID;//物品ID
    public string itemName;//物品名称
    public float itemPrice;//价格  ------float 改成
    public int itemMakeTime;//行为消耗时间
    public int itemEffect;//影响值
    public Level itemLevel;//等级
    public float itemWeight;//物品重量

    public Sprite itemIcon;//图标
    public Sprite itemIconOnWorld;//图标

    public ItemType itemType;//物品类型
    public ItemGetType itemGetType;//物品获取类型（购买，预定）
    public TimeSeason timeSeason;//季节
    public int bookDataID;//书类型
    public string itemInfo;//详细信息

    // ------ 后续测试使用参数
    public int supply;
    public int demand;
    public int quantity;
    public float _price; // 调整后价格

    //------- 后续添加参数  滞后效应、价格变动阈值、季节性因素、
    // 价格弹性(价格敏感度) 1基准，向下低弹性，向上高弹性
    public float priceElasticity;
    // 价格降到这个地方就不能再降了
    public float lowestPrice = 1;
}
