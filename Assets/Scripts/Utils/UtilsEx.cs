using System.Collections;
using System.Collections.Generic;

public static class UtilsEx
{
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

    public static void AddResource(this List<ResourceUnit> list, ResourceUnit newResource)
    {
        // 查找对应的Resource在inputRes中的位置
        int indexInInputRes = list.FindIndex(existingResource => existingResource.res.Equals(newResource.res));

        // 如果找到了对应的Resource
        if (indexInInputRes != -1)
        {
            // 创建新的ResourceUnit对象并替换原有的对象
            list[indexInInputRes] = new ResourceUnit
            {
                res = newResource.res,
                resCount = newResource.resCount + newResource.resCount
            };
        }
        else
        {
            // 创建新项并添加到inputRes中
            list.Add(newResource);
        }
    }

    public static void SubResource(this List<ResourceUnit> list, ResourceUnit subtractResource)
    {
        // 查找对应的Resource在inputRes中的位置
        int indexInInputRes = list.FindIndex(existingResource => existingResource.res.Equals(subtractResource.res));

        // 如果找到了对应的Resource
        if (indexInInputRes != -1)
        {
            // 减去数量
            list[indexInInputRes] = new ResourceUnit()
            {
                res = subtractResource.res,
                resCount = subtractResource.resCount - subtractResource.resCount
            };

            // 如果减到零或以下，删除该项
            if (list[indexInInputRes].resCount <= 0)
            {
                list.RemoveAt(indexInInputRes);
            }
        }
        // 如果在inputRes中找不到对应的Resource，可以选择抛出异常或进行其他处理
    }
}
