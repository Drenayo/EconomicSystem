using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitTaskShow : MonoBehaviour
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
        if (showTran != null)
        {
            // 获取Title下所有材料单位节点
            Transform[] nodes = showTran.GetComponentsInChildren<Transform>();
            string displayText = string.Empty;
            foreach (Transform unit in nodes)
            {
                // 获取MaterialUnit脚本
                ProductionUnit productionUnit = unit.GetComponent<ProductionUnit>();

                if (productionUnit != null)
                {
                    // 获取节点名字、材料单价和材料总量
                    string nodeName = unit.name;
                    // 格式化字符串
                    if(productionUnit.currMaterialUnit != null)
                        displayText += $"{nodeName} 生产 【{productionUnit.currMaterialUnit.transform.name}】\n";
                    else
                        displayText += $"{nodeName} 停工！\n";
                }
            }
            // 在UI的Info text组件上显示信息
            info_Text.text = displayText;
        }
    }
}