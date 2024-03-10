using EconomicSystem;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 调试器
/// </summary>
public class Debugger : MonoBehaviour
{
    public EconomicManager economicManager;

    public int min;
    public int max;

    [Button("随机供需")]
    public void Btn_Random()
    {
        foreach (var item in economicManager.GetItemDataList())
        {
            item.supply = Random.Range(min, max);
            item.demand = Random.Range(min, max);
        }
    }

    [Button("调整价格")]
    public void Btn_as()
    {
        EconomySystem.GetInstance(economicManager).EconomySystemLoop();

    }

    [Button("模拟循环")]
    public void Btn_A()
    {
        GameLoop.instance.GameLoopSimulation();
    }

    // 还要写一个模拟稳定的平衡函数，当某价格波动了，就逐渐开始平衡，这个函数是为了模拟系统运作
    // 价格变高，利润变大，供给增加，价格变低，趋于稳定，直到再度循环
}
