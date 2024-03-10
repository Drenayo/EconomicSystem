using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

[System.Serializable]
public class BuildingData
{
    public int buildID;//建筑ID
    public string buildName;//建筑名称
    public string buildInfo;//建筑详细信息	



    public int preBuildingID;///前置建筑
	public Level buildingLevel;//建筑等级
    public BuildingType buildingType;//建筑类型
    public SellType SellType;//出售类型,商业为收购类型
    public SellType productionType;//生产类型

    public int richValue;//繁荣度

    public int creatMoney;//初始资金





    public int BuildingBookID;//建造图鉴
    public List<ItemAndCount> listLevelUpMaterial = null;//升级需要材料

    public bool isWorkBuilding;///是否营业
	public GameTimeHour buildingOpenTime;//开门时间
    public GameTimeHour buildingCloseTime;//关门时间

    public int rewardBuildingWorkExp;//工作经验奖励

    public List<BuildingUseitemV2> listBuildingUseitemV2;//建筑包含家具
    public List<BuildingPlantV2> listBuildingPlantV2;//建筑包含作物
    public List<int> ListBuildingProfession = new List<int>();//建筑岗位列表，无论是否开启雇佣都有


    public TimeSpan openTime
    {
        get
        {
            // 经过修改
            return TimeSpan.FromSeconds(0);
        }
    }
    public TimeSpan closeTime
    {
        get
        {
            // 经过修改
            return TimeSpan.FromSeconds(0);
        }
    }

    [Header("场景信息")]
    public Sprite buildingIcon;//图标
    public Sprite buildingIconOnWorld;//图标	
    public GameObject OnWorldPrefab;//放置预设体
    public int tileX;//宽度
    public int tileY;//高度
    public List<BuildingNoInsideV2> listNoInsidebuildV2;//纯室外建筑可移动位置
    public List<Vector2Int> listNoInsidebuildMove = new List<Vector2Int>();//纯室外建筑可移动位置
    public bool isNoInside;/// 是否有室内

    public Vector2Int doorInsideGridV2;//门室内位置
    public Vector2Int doorMainGridV2;//门室外位置
    public MapData_SO mapData_SO;//地图
    public GameObject insidePrefab;//场景预设体


}