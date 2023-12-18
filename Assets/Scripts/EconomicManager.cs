using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomicManager : MonoBehaviour
{
    public static EconomicManager instance;
    public Transform productionTran;
    public Transform materialTran;

    // 建筑列表
    public List<ProductionUnit> allProductionUnits;
    // 材料列表
    public List<MaterialUnit> allMaterialUnits;

    private void Awake()
    {
        instance = this;

        foreach (Transform item in productionTran)
        {
            allProductionUnits.Add(item.GetComponent<ProductionUnit>());
        }
        foreach (Transform item in materialTran)
        {
            allMaterialUnits.Add(item.GetComponent<MaterialUnit>());
        }
    }

}
