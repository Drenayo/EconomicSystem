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
        if (EconomicManager.Instance.allBuildingList != null && EconomicManager.Instance.allBuildingList.Count > 0)
        {
            foreach (var item in EconomicManager.Instance.allBuildingList)
            {
                if(item.currProductionRecipe.Count > 0)
                    displayText += string.Format("{0,-10}{1,-10}{2,-10:F1}{3,-10}\n", item.buildingName, item.buildingState.ToString(), item.deposit, item?.currProductionRecipe[0]?.outputRes?.res?.resName);
                else
                    displayText += string.Format("{0,-10}{1,-10}{2,-10}{3,-10}\n", item.buildingName, item.buildingState.ToString(), item.deposit, "null");

            }
        }
        showText.text = displayText;
    }
}
 