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

    public int resIDAcc = 0;

    // 建筑列表
    public List<Building> allBuildingList;
    // 资源列表
    public List<Resource> allResourceList;
    // NPC列表
    public List<NPC> allNPCList;

    private void Awake()
    {
        instance = this;
    }

    private void Load()
    {
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

        foreach (Transform item in NPCParent)
        {
            if (item.GetComponent<NPC>())
                allNPCList.Add(item.GetComponent<NPC>());
        }
    }

    private void Start()
    {
        Load();
    }




    public List<IBuilding> GetBuildingList()
    {
        foreach (Transform item in buildingParent)
        {
            if (item.GetComponent<Building>())
                allBuildingList.Add(item.GetComponent<Building>());
        }
        return allBuildingList.ConvertAll<IBuilding>(x => x);
    }

    public List<IResource> GetResourceList()
    {
        foreach (Transform item in ResourceParent)
        {
            if (item.GetComponent<Resource>())
                allResourceList.Add(item.GetComponent<Resource>());
        }
        //Debug.Log(allResourceList.Count+"_1");
        //Debug.Log(allResourceList.ConvertAll<IResource>(x => x).Count + "_2");
        return allResourceList.ConvertAll<IResource>(x => x);
    }

    public List<IEconomicUnit> GetAllEconomicEntity()
    {
        Load();

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
                        supply += productionRecipe.outputRes.resCount;
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
                            demand += inputRes.resCount;
                        }
                    }
                }
            }
        }
        return demand;
    }
}
