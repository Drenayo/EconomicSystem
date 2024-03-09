using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class PlantBookData : BookData
{

    public Level PlantLevel;//种植等级
    public int itemSeedID;//种子物品
    public int PlantDataID;//作物ID

    public PlantState initState;//初始/新种植状态

    [Header("不同阶段的时间")]
    public int GrowPreTime;//多少时间需要浇水
    public int GrowTimes;//浇多少次
    [Header("不同阶段的物品Prefab")]
    public GameObject[] growPrefab;
    [Header("种植季节")]
    public TimeSeason plantSeason;



    [Space]
    [Header("收割工具")]
    public int toolItemID;


    [Header("收割后转换ID")]
    public int transferItemID;//如果为0则重新变成农田，如果不为空，则继续循环成长（树木）

    [Space]
    [Header("获取物品ID")]
    public List<ItemAndCount> listGetItem = new List<ItemAndCount>();
    [Header("产量转化比")]
    public int plantCountToItem;//种植产量和物品转化比(种植总数除以这个数，获得最终获得数量)

    public int randomItemID;//稀有物品ID
    public int randomCount;//稀有物品比例

    [Header("其他")]

    public bool hasAnim;//动画
    public bool hasEffect;//粒子
                          //音效
}
//////////////////////

public class ItemBookData : BookData
{
    public int getItemID;
    public int getCount;
    public List<ItemAndCount> ListSpentItem = new List<ItemAndCount>();//花费物品
    public ItemData getItem
    {
        get
        {
            // 已修改
            return new ItemData();
        }
    }
}