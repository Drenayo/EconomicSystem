using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EconomicManager : MonoSingleton<EconomicManager>, IEconomicManager
{
    public Generator generator;

    // 建筑列表
    [SerializeField] private List<BuildingData> allBuildingList;
    // NPC列表
    [SerializeField] private List<ChDataInst> allNPCList;
    // 物品列表
    [SerializeField] private List<ItemData> allItemDataList;
    // NPC列表
    [SerializeField] private List<ChDataInst> allNPCDataList = new List<ChDataInst>();

    ///// <summary>
    ///// 历史供应量
    ///// </summary>
    //[SerializeField] private Dictionary<int, List<int>> historicalSupply;

    ///// <summary>
    ///// 历史需求量
    ///// </summary>
    //[SerializeField] private Dictionary<int, List<int>> historicalDemand;

    private void Start()
    {
        Load();
    }

    private void Load()
    {
        allBuildingList = new List<BuildingData>();
        allBuildingList = generator.buildingDataList_SO.listBuildingData.ConvertAll(x => x);
        allItemDataList = new List<ItemData>();
        allItemDataList = generator.itemDataList_SO.listItem.ConvertAll(x => x);


        allNPCList = new List<ChDataInst>();
        var npc = new ChDataInst();
        npc.ChIndex = 0;
        // 生成NPC
        for (int i = 0; i < 20; i++)
            allNPCList.Add(npc);
    }

    #region 接口实现

    // 总供
    public int GetMarketSupply(int itemID)
    {

        return allItemDataList.FirstOrDefault(item => item.itemID == itemID).supply;
    }

    // 总需
    public int GetMarketDemand(int itemID)
    {
        return allItemDataList.FirstOrDefault(item => item.itemID == itemID).demand;
    }
    
    // 总量
    public int GetMarketTotalQuantity(int itemID)
    {
        return allItemDataList.FirstOrDefault(item => item.itemID == itemID).quantity;

    }

    // NPC
    public List<ChDataInst> GetNPCDataList()
    {
        return allNPCList;
    }

    public List<BuildingData> GetBuildingDataList()
    {
        return allBuildingList;
    }

    public List<ItemData> GetItemDataList()
    {
        return allItemDataList;
    }

    int IEconomicManager.GetMarketSupply(int itemID)
    {
        throw new System.NotImplementedException();
    }

    int IEconomicManager.GetMarketDemand(int itemID)
    {
        throw new System.NotImplementedException();
    }

    int IEconomicManager.GetMarketTotalQuantity(int itemID)
    {
        throw new System.NotImplementedException();
    }

 

    #endregion
}
