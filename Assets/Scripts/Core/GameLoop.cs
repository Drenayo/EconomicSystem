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
        economySystem = new EconomySystem(EconomicManager.Instance as IEconomicManager);
    }

    public void Start()
    {
        // 启动模拟
        StartCoroutine(Repeating());
    }

    
    public void GameLoopUpdate()
    {
        //Debug.Log($"度过了第{++ totalDaysElapsed}天。");
        totalDaysElapsed_Text.text = $"第 <color=#bbb>{++totalDaysElapsed}</color> 天";
        economySystem.EconomySystemLoop();
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
