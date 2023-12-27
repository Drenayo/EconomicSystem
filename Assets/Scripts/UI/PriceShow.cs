using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriceShow : MonoBehaviour
{
    public Transform showTran;
    public Text title_Text;
    public Text info_Text;

    private void Start()
    {
        title_Text.text = showTran.name;
    }

    void Update()
    {
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
