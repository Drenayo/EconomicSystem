using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 游戏主循环
/// </summary>
public class GameLoop : MonoBehaviour
{
    public static GameLoop instance;
    [HideInInspector]
    public UnityEvent gameLoopEvent;

    [Header("nS/天")]
    public float callInterval;
    [Header("已经度过的天数")]
    public float totalDaysElapsed = 0;
    [Header("是否暂停")]
    public bool isStop = false;


    [Space]
    public Text totalDaysElapsed_Text;

    private void Awake()
    {
        instance = this;
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
        gameLoopEvent.Invoke();


        EconomySystem.instance.AdjustEconomy();
    }


    public void Btn_Continue()
    {
        isStop = false;
    }
    public void Btn_Stop()
    {
        isStop = true;
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
