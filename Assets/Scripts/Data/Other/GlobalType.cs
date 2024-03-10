using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GlobalType
{

}

public enum BuildingType
{
    empty, home, wen, wu, nong, gong, shang, other, government
}
public enum SellType
{
    empty, allItem, foodMaterial, book, food, woodItem,
}
public class MapData_SO : ScriptableObject
{
}

public class BuildingNoInsideV2
{
}
public class BuildingUseitemV2
{
    public int useitemID;
    public Vector2Int v2;
}


public class BuildingPlantV2
{
}
public class GameTimeHour
{
}
public class ItemAndCount
{
    public int itemID;//ID
    public ItemData itemData
    {
        // 已修改
        get; set;
    }//物品
    public int count;//数量
    public int price
    {
        get
        {
            int p = (int)itemData.itemPrice;
            switch (priceType)
            {
                case PriceType.veryLow:
                    p = Mathf.FloorToInt(p * (1 + Settings.veryLowPrice));
                    break;
                case PriceType.Low:
                    p = Mathf.FloorToInt(p * (1 + Settings.LowPrice));
                    break;
                case PriceType.littleLow:
                    p = Mathf.FloorToInt(p * (1 + Settings.littleLowPrice));
                    break;
                case PriceType.littleHigth:
                    p = Mathf.CeilToInt(p * (1 + Settings.littleHigthPrice));
                    break;
                case PriceType.Higth:
                    p = Mathf.CeilToInt(p * (1 + Settings.hourPremin));
                    break;
                case PriceType.veryHigth:
                    p = Mathf.CeilToInt(p * (1 + Settings.veryHigthPrice));
                    break;
            }
            return p;
        }
    }//购买价格
    public PriceType priceType;//价格类型
    public int priceBetterValueTemp;//价格性价比，临时存放

    public string buildingIndex;//生产建筑
    public int makeChIndex;///生产者
	public int hideRandom;//获取概率
    public int hideCountChange;//获取数量
    public ItemAndCount(int itemID, int count)
    {
        this.itemID = itemID;
        this.count = count;
        priceType = PriceType.normal;
    }
}
/// <summary>
/// 图鉴类型
/// </summary>
public enum BookType
{
    empty, itemBook, useItemBook, BuildingBook, PlantBook, all
}

/// <summary>
/// 物品制作类型 
/// </summary>
public enum MakeType
{
    empty, eatMake, woodMake, clothMake, storMake, ironMake, brickBuilding, wineMake, plantMake, all
}
public class ChAddValue
{
    public ChAdditionType additionType;//类型
    public int value;//数值
}

public enum PlantState
{
    empty, NotPlant, Grow, NotWater, Finish, Dead
}       /// 作物状态


/// <summary>
/// 行为类型，获取物品，增加数值
/// 获取物品类型，消耗物品，获取物品
/// 增加数值类型，消耗物品，或许属性
/// </summary>
public enum ActionDoType
{
    empty, getValue, getItem, makeItem, makeUseItem, makeBuilding, Business, ItmeOrder, BuildingOrder, Sell, setContract, Hire, OrderControl, IndustryManagerOrderBuilding, TakeOver, CreatGang, lease, talk, work, plant, Fight, learn, familyAdd, familyMeet, store, tame, fire, sendHire, workManager, signSellContract, sendContract, setSell
}



/// <summary>
/// 职业类型
/// </summary>
public enum OccupationType
{
    empty, wen, wu, nong, gong, shang
}


/// <summary>
/// 行为权限
/// </summary>
public enum ChLimitType
{
    empty, all, lease, worker, Family, owner, client, business
}






public enum ChValueType
{
    empty, Hunger, Energy, money
}//任务基本数值类型,钱用于UI刷新

public enum GetValueGetType
{
    empty, expend, useitem
}

/// <summary>
/// 物品类型
/// </summary>
public enum ItemType
{
    /// <summary>
    /// 空物品，可能表示格子中没有物品
    /// </summary>
    empty,

    /// <summary>
    /// 种子，用于种植植物或作为农作物的种子
    /// </summary>
    Seed,

    /// <summary>
    /// 材料，通常用于制作其他物品或建筑
    /// </summary>
    material,

    /// <summary>
    /// 食物，可供角色食用或用于料理
    /// </summary>
    food,

    /// <summary>
    /// 书籍，可能是技能书、图纸或其他阅读类物品
    /// </summary>
    book,

    /// <summary>
    /// 种植工具，用于种植或处理植物的工具
    /// </summary>
    PlantTool,

    /// <summary>
    /// 收集工具，用于采集资源或收集物品
    /// </summary>
    CollectTool,

    /// <summary>
    /// 硬币，游戏中的货币单位
    /// </summary>
    coin
}

/// <summary>
/// 物品获取方式
/// </summary>
public enum ItemGetType
{
    /// <summary>
    /// 空获取类型，可能表示未定义的获取方式
    /// </summary>
    empty,

    /// <summary>
    /// 购买，通过购买获得物品
    /// </summary>
    buy,

    /// <summary>
    /// 订购，通过订购获得物品
    /// </summary>
    order,

    /// <summary>
    /// 收集，通过收集或采集获得物品
    /// </summary>
    collect,

    /// <summary>
    /// 学习，通过学习或获取知识获得物品
    /// </summary>
    learn
}

/// <summary>
/// 季节
/// </summary>
public enum TimeSeason
{
    empty, spring, summer, autumn, winter, all
}
public enum Level
{
    empty, one, two, three, four, five
}//等级


public class UseItemBookData : BookData
{
}
public class BuildingBookData : BookData
{
} 


public enum PriceType
{
    emtpy, veryLow, Low, littleLow, normal, littleHigth, Higth, veryHigth
}

public enum ChAdditionType
{
    empty, plantTime, plantValue, learnTime, learnValue, fightHealth, fightEnergy, fightLoseReward, fightWinReward, TameRange
}//人物数值加成类型

public class Settings
{
    public const float veryLowPrice = -0.2f;//价格数量比例系数
    public const float LowPrice = -0.1f;//价格数量比例系数
    public const float littleLowPrice = -0.05f;//价格数量比例系数
    public const float littleHigthPrice = 0.05f;//价格数量比例系数
    public const float HigthPrice = 0.1f;//价格数量比例系数
    public const float veryHigthPrice = 0.2f;//价格数量比例系数
    public const float hourPremin = 0.1f;
}

[System.Serializable]
public class IDAndCount
{
    public int ID;
    public int count;
}