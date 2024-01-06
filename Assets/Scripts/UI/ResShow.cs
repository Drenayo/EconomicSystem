using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ResShow : MonoBehaviour
{
    public Text showText;
    void Update()
    {
        string displayText = string.Empty;
        if (Market.Instance.dicMarketStock != null)
        {
            foreach (var item in Market.Instance.dicMarketStock)
            {
                displayText += string.Format("{0,-10} {1,-10} {2,-10:F1} {3,-10} {4,-10}\n", item.Value.res.resName, item.Value.resCount, item.Value.res.currPrice,EconomicManager.Instance.GetMarketSupply(item.Key), EconomicManager.Instance.GetMarketDemand(item.Key));
            }
        }
        showText.text = displayText;

        //if (showTran != null)
        //{
        //    // 获取Title下所有材料单位节点
        //    Transform[] materialUnitNodes = showTran.GetComponentsInChildren<Transform>();
        //    string displayText = string.Empty;
        //    foreach (Transform materialUnitNode in materialUnitNodes)
        //    {
        //        // 获取MaterialUnit脚本
        //        MaterialUnit materialUnit = materialUnitNode.GetComponent<MaterialUnit>();

        //        if (materialUnit != null)
        //        {
        //            // 获取节点名字、材料单价和材料总量
        //            string nodeName = materialUnitNode.name;
        //            float price = materialUnit.price;
        //            int total = materialUnit.total;

        //            // 格式化字符串
        //            //string formattedInfo = string.Format("{0,-10} {1,-5} {2,-5}", nodeName, $"{price:F2}", total);
        //            displayText += string.Format("{0,-8} {1,-6:F2} {2,-5}", nodeName, price, total) + "\n";
        //        }
        //    }
        //    // 在UI的Info text组件上显示信息
        //    info_Text.text = displayText;
        //}
    }
}
