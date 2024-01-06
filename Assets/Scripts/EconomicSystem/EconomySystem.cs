using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EconomicSystem
{
    /// <summary>
    /// 经济系统控制中枢，与实际业务剥离，只处理经济系统抽象核心逻辑
    /// </summary>
    public class EconomySystem
    {
        private IEconomicManager economicManager;

        #region 公共方法
        public EconomySystem(IEconomicManager economicManager) 
        {
            this.economicManager = economicManager;
        }

        ///// <summary>
        ///// 启动经济系统
        ///// </summary>
        ///// <param name="economicManager">经济系统数据接口</param>
        //public void StartEconomySystem()
        //{
            
        //}

        /// <summary>
        /// 经济系统主循环
        /// </summary>
        public void EconomySystemLoop()
        {
            // 经济实体循环
            InvokeEntityLoop();

            // 调整资源价格
            AdjustResPrice();
        }
        #endregion


        #region 局部方法
        /// <summary>
        /// 调用经济实体循环
        /// </summary>
        private void InvokeEntityLoop()
        {
            foreach (var item in economicManager.GetAllEconomicEntity())
                item.Loop();
        }

        /// <summary>
        /// 调整资源价格
        /// </summary>
        private void AdjustResPrice()
        {
            foreach (var item in economicManager.GetResourcesDataList())
            {
                int resID = item.GetID();
                item.SetPrice(AdjustPriceBasedOnSupplyDemand(item,economicManager.GetMarketDemand(resID),economicManager.GetMarketSupply(resID)));
            }
        }
        #endregion

        #region 调整策略
        /// <summary>
        /// 根据供需关系调整价格
        /// </summary>
        /// <param name="resource">原始价格</param>
        /// <param name="quantityDemanded">需求量</param>
        /// <param name="supplyQuantity">供应量</param>
        private float AdjustPriceBasedOnSupplyDemand(IResourceData resourceData, float quantityDemanded, float supplyQuantity)
        {
            float adjustedPrice = resourceData.GetCurrPrice();
                                                 
            // 设置供需比的阈值，低于这个阈值不进行价格调整
            float imbalanceThreshold = 0.2f;

            // 如果需求大于供给，需要减去市场库存
            if (quantityDemanded > supplyQuantity)
            {
                if (economicManager.GetMarketTotalQuantity(resourceData.GetID()) - quantityDemanded >= 0)
                    return adjustedPrice;
            }

            // 计算供需失衡比例
            float imbalanceRatio = (quantityDemanded - supplyQuantity) / Mathf.Max(quantityDemanded, supplyQuantity);

            // 只有在供需比高于阈值时才进行价格调整
            if (MathF.Abs(imbalanceRatio) > imbalanceThreshold)
            {
                // 根据供需失衡比例来影响涨跌的比例
                adjustedPrice += (adjustedPrice * imbalanceRatio / 4); // 上涨或下降的比例可以根据实际需求进行调整
            }

            // 如果供需平衡或低于阈值，价格不变
            Debug.Log(EconomicManager.Instance.GetResourceDataByID(resourceData.GetID()).resName + $"的需求量:{quantityDemanded} 供给量:{supplyQuantity} 调整价格:{adjustedPrice}");

            return adjustedPrice;
        }

        #endregion
    }
}
