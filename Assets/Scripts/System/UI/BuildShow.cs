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

        if (EconomicManager.Instance.GetBuildingDataList() != null)
        {
            foreach (var item in EconomicManager.Instance.GetBuildingDataList())
            {
                displayText += string.Format("{0,-10}{1,-10}{2,-10}{3,-10}\n", item.buildName, item.creatMoney.ToString(),item.richValue.ToString(), item.isWorkBuilding.ToString());
            }
        }
        showText.text = displayText;
    }
}
 