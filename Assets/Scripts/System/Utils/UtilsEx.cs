using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsEx
{
    /// <summary>
    /// 验证是否存在该ID
    /// </summary>
    /// <param name="list"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static bool HasID(this List<ResourceUnit> list, int id)
    {
        foreach (ResourceUnit item in list)
        {
            if (item.ID == id)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 得到ID对应资源单位
    /// </summary>
    /// <param name="list"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static ResourceUnit GetIDRes(this List<ResourceUnit> list, int id)
    {
        foreach (ResourceUnit item in list)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// 为资源单位列表添加资源
    /// </summary>
    public static void AddRes(this List<ResourceUnit> list, int resID, int resCount)
    {
        //if (list.HasID(resID))
        //{
        //    foreach (var item in list)
        //    {
        //        if (item.ID == resID)
        //            item.resCount += resCount;
        //    }
        //}
        //else
        //{
        //    list.Add(new ResourceUnit(EconomicManager.Instance.GetResourceDataByID(resID), resCount));
        //}
    }

    /// <summary>
    /// 为资源单位列表减去资源
    /// </summary>
    public static void SubRes(this List<ResourceUnit> list, int resID, int resCount)
    {
        if (list.HasID(resID))
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == resID && (list[i].resCount - resCount) >= 0)
                {
                    list[i].resCount -= resCount;
                    if (list[i].resCount <= 0)
                        list.Remove(list[i]);
                }
            }
        }
    }

    /// <summary>
    /// 验证资源是否充足
    /// </summary>
    /// <param name="list"></param>
    /// <param name="resID"></param>
    /// <param name="resCount"></param>
    /// <returns></returns>
    public static bool ValidateResourceAvailability(this List<ResourceUnit> list, int resID, int resCount)
    {
        //Debug.Log($"{resID},{resCount}");
        if (list.HasID(resID))
        {
            foreach (var item in list)
            {
                if (item.ID == resID)
                    if((item.resCount - resCount) >= 0)
                        return true;
            }
        }
        return false;
    }


    
}
