using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class EconomicManager : MonoSingleton<EconomicManager>,IEconomicManager
{
    public Transform buildingParent;
    public Transform NPCParent;

    // 建筑列表
    public List<Building> allBuildingList;
    // NPC列表
    public List<NPC> allNPCList;
    // 资源模板列表
    public List<ResourceData> allResourceData;

    private void Start()
    {
        Load();
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
            allResourceData.Add(item);
        }
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
    
    /// <summary>
    /// 通过ID拿到对应资源Data
    /// </summary>
    /// <returns></returns>
    public ResourceData GetResourceDataByID(int resID)
    {
        foreach (var item in allResourceData)
        {
            if(item.id == resID)
                return item;
        }
        return null;
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

    // 获取某资源的市场供应量
    public int CalculateSupply(int resID)
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

    // 获取某资源的市场需求量
    public int CalculateDemand(int resID)
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

}
