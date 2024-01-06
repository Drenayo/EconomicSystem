using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class BuildShow : MonoBehaviour
{
    public Text showText;
    void Update()
    {
        string displayText = string.Empty;
        if (EconomicManager.Instance.GetBuildingList() != null && EconomicManager.Instance.GetBuildingList().Count > 0)
        {
            foreach (var item in EconomicManager.Instance.GetBuildingList())
            {
                Building building = item as Building;   
                if(building.currProductionRecipe.Count > 0)
                    displayText += string.Format("{0,-10}{1,-10}{2,-10:F1}{3,-10}\n", building.buildingName, building.buildingState.ToString(), building.deposit, building?.currProductionRecipe[0]?.outputRes?.res?.resName);
                else
                    displayText += string.Format("{0,-10}{1,-10}{2,-10}{3,-10}\n", building.buildingName, building.buildingState.ToString(), building.deposit, "null");

            }
        }
        showText.text = displayText;
    }
}
 