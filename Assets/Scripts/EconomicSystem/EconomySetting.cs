using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 经济系统设置
/// </summary>
public class EconomySetting : MonoBehaviour
{
    public static EconomySetting Instance;

    private void Awake()
    {
        Instance = this;
    }

    //[Header("参与计算的类型"), Tooltip("参与计算的类型")]
    //public ItemType calculationTypes;

    [Header("供需比的阈值"),Tooltip("低于这个阈值不进行价格调整")]
    public float imbalanceThreshold = 0.2f;

    [Header("价格波动平滑系数"), Tooltip("该系数将乘以调整后的价格")]
    public float priceSmoothingFactor = 0.2f;

    [Header("计算总量"), Tooltip("勾选则表示如果需求大于供给，需要减去总库存后再计算")]
    public bool calculateTotal = false;

    [Header("计算价格弹性"), Tooltip("勾选则表示价格弹性影响价格计算结果")]
    public bool calculatePriceElasticity = true;

    [Header("平滑价格波动（未完成）"), Tooltip("价格调整中引入一些延迟，以平滑价格的波动（非一次性调整价格）")]
    public bool enableSmoothFluctuations = true;
}
