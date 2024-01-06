using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源接口
/// </summary>
public interface IResourceData
{
    /// <summary>
    /// 调整价格
    /// </summary>
    void SetPrice(float price);

    /// <summary>
    /// 获取ID
    /// </summary>
    int GetID();

    /// <summary>
    /// 获取当前价格
    /// </summary>
    float GetCurrPrice();
}
