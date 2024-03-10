using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ItemShow : MonoBehaviour
{
    public List<Text> texts = new List<Text>();
    void Update()
    {
        foreach (Text text in texts)
        {
            text.text = string.Empty;
        }
        if (EconomicManager.Instance.GetItemDataList() != null)
        {
            foreach (var item in EconomicManager.Instance.GetItemDataList())
            {
                //        displayText += string.Format("{0,-5}{1,-5}{2,-5}{3,-5}{4,-5}\n", item.itemName, item.itemPrice,item.supply,item.demand,item.quantity);
                texts[0].text += item.itemName + "\n";
                texts[1].text += item.itemPrice.ToString() + "\n";
                texts[2].text += item.supply.ToString() + "\n";
                texts[3].text += item.demand.ToString() + "\n";
                texts[4].text += item.quantity.ToString() + "\n";
                texts[5].text += item._price.ToString() + "\n";
            }
        }


        //if (Market.Instance.dicMarketStock != null)
        //{
        //    foreach (var item in Market.Instance.dicMarketStock)
        //    {
        //        displayText += string.Format("{0,-10} {1,-10} {2,-10:F1} {3,-10} {4,-10}\n", item.Value.res.resName, item.Value.resCount, item.Value.res.currPrice,EconomicManager.Instance.GetMarketSupply(item.Key), EconomicManager.Instance.GetMarketDemand(item.Key));
        //    }
        //}


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
