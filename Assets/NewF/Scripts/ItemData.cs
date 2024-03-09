using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public int itemID;//物品ID
    public string itemName;//物品名称
    public int itemPrice;//价格
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
}
