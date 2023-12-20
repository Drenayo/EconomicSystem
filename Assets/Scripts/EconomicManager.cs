using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EconomicManager : MonoBehaviour, IEconomicManager
{
    public static EconomicManager instance;
    public Transform buildingParent;
    public Transform ResourceParent;
    public Transform NPCParent;

    // 建筑列表
    public List<Building> allBuildingList;
    // 资源列表
    public List<Resource> allResourceList;
    // NPC列表
    public List<NPC> allNPCList;

    private void Awake()
    {
        instance = this;

        foreach (Transform item in buildingParent)
        {
            if (item.GetComponent<Building>())
                allBuildingList.Add(item.GetComponent<Building>());
        }
        foreach (Transform item in ResourceParent)
        {
            if (item.GetComponent<Resource>())
                allResourceList.Add(item.GetComponent<Resource>());
        }
    }




    public List<IBuilding> GetBuildingList()
    {
        return allBuildingList.ConvertAll<IBuilding>(x => x);
    }

    public List<IResource> GetResourceList()
    {
        return allResourceList.ConvertAll<IResource>(x => x);
    }

    public List<IEconomicUnit> GetAllEconomicEntity()
    {
        return allBuildingList.Cast<IEconomicUnit>()
                .Concat(allResourceList.Cast<IEconomicUnit>())
                .Concat(allNPCList.Cast<IEconomicUnit>())
                .ToList();
    }


    // 获取某资源的市场供应量
    public int CalculateSupply(IResource resource)
    {
        int supply = 0;
        foreach (var item in allBuildingList)
        {
            if (item.productionRecipeList != null && item.currProductionRecipe != null)
            {
                foreach (var productionRecipe in item.currProductionRecipe)
                {
                    if ((resource as Resource).id == productionRecipe.outputRes.res.id)
                    {
                        supply += productionRecipe.outputRes.resQuantity;
                    }
                }
            }
        }
        return supply;
    }

    // 获取某资源的市场需求量
    public int CalculateDemand(IResource resource)
    {
        int demand = 0;
        foreach (var item in allBuildingList)
        {
            if (item.productionRecipeList != null && item.currProductionRecipe != null)
            {
                foreach (var productionRecipe in item.currProductionRecipe)
                {
                    foreach (var inputRes in productionRecipe.inputRes)
                    {
                        if ((resource as Resource).id == inputRes.res.id)
                        {
                            demand += inputRes.resQuantity;
                        }
                    }
                }
            }
        }
        return demand;
    }
}
