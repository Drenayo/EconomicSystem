using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class EconomicManager : MonoSingleton<EconomicManager>, IEconomicManager
{
    public Transform buildingParent;
    public Transform NPCParent;

    // 建筑列表
    [SerializeField] private List<Building> allBuildingList;
    // NPC列表
    [SerializeField] private List<NPC> allNPCList;
    // 资源模板列表
    [SerializeField] private List<ResourceData> allResourceDataList;

    /// <summary>
    /// 历史供应量
    /// </summary>
    [SerializeField] private Dictionary<int, List<int>> historicalSupply;

    /// <summary>
    /// 历史需求量
    /// </summary>
    [SerializeField] private Dictionary<int, List<int>> historicalDemand;

    private void Start()
    {
        Load();
    }
    private void OnDestroy()
    {
        foreach (var item in allResourceDataList)
        {
            item.currPrice = item.originalPrice;
        }
    }
    private void Load()
    {
        foreach (Transform item in buildingParent)
        {
            if (item.GetComponent<Building>())
                allBuildingList.Add(item.GetComponent<Building>());
        }
        foreach (Transform item in NPCParent)
        {
            if (item.GetComponent<NPC>())
                allNPCList.Add(item.GetComponent<NPC>());
        }

        // 读取所有资源
        foreach (ResourceData item in Resources.LoadAll<ResourceData>("资源/"))
        {
            allResourceDataList.Add(item);
        }
    }


    /// <summary>
    /// 通过ID拿到对应建筑实例
    /// </summary>
    public Building GetBuildingByID(int buildingID)
    {
        foreach (var item in allBuildingList)
        {
            if (item.id == buildingID)
                return item;
        }
        return null;
    }

    /// <summary>
    /// 通过ID拿到对应资源Data
    /// </summary>
    /// <returns></returns>
    public ResourceData GetResourceDataByID(int resID)
    {
        foreach (var item in allResourceDataList)
        {
            if(item.id == resID)
                return item;
        }
        return null;
    }
    
    #region 接口实现

    public int GetMarketSupply(int resID)
    {
        int supply = 0;
        foreach (var item in allBuildingList)
        {
            if (item.currProductionRecipe != null)
            {
                foreach (var productionRecipe in item.currProductionRecipe)
                {
                    if (resID == productionRecipe.outputRes.res.id)
                    {
                        supply += productionRecipe.outputRes.resCount;
                    }
                }
            }
        }
        return supply;
    }

    public int GetMarketDemand(int resID)
    {
        int demand = 0;
        foreach (var item in allBuildingList)
        {
            if (item.currProductionRecipe != null)
            {
                foreach (var productionRecipe in item.currProductionRecipe)
                {
                    foreach (var inputRes in productionRecipe.inputRes)
                    {
                        if (resID == inputRes.res.id)
                        {
                            demand += inputRes.resCount;
                        }
                    }
                }
            }
        }
        return demand;
    }
    
    public List<IBuilding> GetBuildingList()
    {
        return allBuildingList.ConvertAll<IBuilding>(x => x);
    }

    public List<IEconomicUnit> GetAllEconomicEntity()
    {
        return allBuildingList.Cast<IEconomicUnit>()
                .Concat(allNPCList.Cast<IEconomicUnit>())
                .ToList();
    }

    public List<IResourceData> GetResourcesDataList()
    {
        return allResourceDataList.ConvertAll<IResourceData>(x => x);
    }

    public Dictionary<int, List<int>> GetHistoricalSupply(int hisDays)
    {
        return historicalSupply;
    }

    public Dictionary<int, List<int>> GetHistoricalDemand(int hisDays)
    {
        return historicalDemand;
    }

    public int GetMarketTotalQuantity(int resID)
    {
        return Market.Instance.dicMarketStock[resID].resCount;
    }

    #endregion
}
