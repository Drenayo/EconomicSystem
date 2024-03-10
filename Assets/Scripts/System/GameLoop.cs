using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using EconomicSystem;

/// <summary>
/// 游戏主循环
/// </summary>
public class GameLoop : MonoBehaviour
{
    public static GameLoop instance;
    [LabelText("每天的速度")]
    public float callInterval = 1;
    [LabelText("已经度过的天数")]
    public float totalDaysElapsed = 0;
    [LabelText("是否暂停")]
    public bool isStop = false;
    [LabelText("天数UI Text")]
    public Text totalDaysElapsed_Text;

    private EconomySystem economySystem;

    private void Awake()
    {
        instance = this;
      //  economySystem = new EconomySystem(EconomicManager.Instance as IEconomicManager);
    }

    public void Start()
    {
        // 启动模拟
        StartCoroutine(Repeating());
    }

    
    public void GameLoopUpdate()
    {
        totalDaysElapsed_Text.text = $"第 <color=#bbb>{++totalDaysElapsed}</color> 天";

        // 调用主系统：写本地相关的逻辑循环，比如房屋建筑交易等模拟行为的循环
     //   GameLoopSimulation();

        // 调用经济系统：这里是经济系统调用点，每次调用，经济系统就会根据现有数据更新价格，下达指令等。
        // economySystem.EconomySystemLoop();
    }

    // 游戏循环模拟
    public void GameLoopSimulation()
    {
        // 判断初始价格与当前价格差，获取利润足够高就开始“生产”。一次产1，同时供给+1，需求-1
        foreach (var item in EconomicManager.Instance.GetItemDataList())
        {
            if (item.itemPrice < item._price)
            {
                if (item.demand >= 1)
                {
                    item.supply++;
                    item.demand--;
                    item.quantity++;
                }
            }
        }
    }


    IEnumerator Repeating()
    {
        while (true)
        {
            while (!isStop)
            {
                yield return new WaitForSeconds(callInterval);
                GameLoopUpdate();
            }
            yield return null;
        }
    }
}
