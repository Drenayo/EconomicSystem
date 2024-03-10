using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

namespace EconomicSystem
{
    /// <summary>
    /// 经济系统控制中枢，与实际业务剥离，只处理经济系统抽象核心逻辑
    /// </summary>
    public class EconomySystem
    {
        private IEconomicManager economicManager;
        private static EconomySystem instance;
        private EconomySystem(IEconomicManager economicManager)
        {
            this.economicManager = economicManager;
        }
        public static EconomySystem GetInstance(IEconomicManager economicManager)
        {
            if (instance == null)
            {
                instance = new EconomySystem(economicManager);
            }
            return instance;
        }
        public static EconomySystem GetInstance()
        {
            return instance;
        }

        #region 公共方法


        /// <summary>
        /// 经济系统主循环
        /// </summary>
        public void EconomySystemLoop()
        {
            AdjustItemPrice();
            EnsureBasicSupply();
        }

        /// <summary>
        /// 单独调用：调整物品价格
        /// </summary>
        private void AdjustItemPrice()
        {
            foreach (var item in economicManager.GetItemDataList())
            {
                if (item.itemType != ItemType.coin)
                {
                    float adjust = AdjustPriceBasedOnSupplyDemand(item.itemPrice, item.demand, item.supply, item.quantity, item.priceElasticity);
                    if (EconomySetting.Instance.enableSmoothFluctuations)
                    {
                        // 调用平滑函数
                        item._price = adjust;
                    }
                    else
                    {
                        item._price = adjust;
                    }



                    // 如果调整价格低于设定集最低值，则直接设置为设定值
                    if (item._price < item.lowestPrice)
                        item._price = item.lowestPrice;
                }
            }
        }
        
        /// <summary>
        /// 单独调用：基础供给保障（未完成）
        /// </summary>
        public void EnsureBasicSupply()
        {

        }


        #endregion

        #region 调整策略
        /// <summary>
        /// 根据供需关系调整价格
        /// </summary>
        /// <param name="resource">原始价格</param>
        /// <param name="quantityDemanded">需求量</param>
        /// <param name="supplyQuantity">供应量</param>
        private float AdjustPriceBasedOnSupplyDemand(float origPrice, int demand, int supply, int quantity,float elasticity)
        {
            //Debug.Log($"价格调整:{origPrice}  {demand}  {supply}  {quantity}");

            float adjustedPrice = origPrice;
            float imbalanceThreshold = EconomySetting.Instance.imbalanceThreshold;
            
            if (demand == supply)
                return adjustedPrice;

            // 如果需求大于供给，需要减去总库存
            if (demand > supply && EconomySetting.Instance.calculateTotal)
            {
                if (quantity - demand >= 0)
                    return adjustedPrice;
            }

            // 计算供需失衡比例
            float imbalanceRatio = (float)(demand - supply) / (float)Mathf.Max(demand, supply);
            
            // 只有在供需比高于阈值时才进行价格调整
            if (MathF.Abs(imbalanceRatio) > imbalanceThreshold)
            {
                if (EconomySetting.Instance.calculatePriceElasticity)
                    adjustedPrice += (adjustedPrice * imbalanceRatio * elasticity);
                else
                    adjustedPrice += (adjustedPrice * imbalanceRatio);
            }

            //Debug.Log($"计算后:{adjustedPrice}  {imbalanceRatio}  {imbalanceThreshold}  {adjustedPrice * imbalanceRatio}");

            return adjustedPrice;
        }


        private void GenerateBuildingStrategy()
        {
            // 拿到NPC数量，计算所需食物，
            // 判断食物是否充足，不足就生成对应建筑
            // 同时还要判断生成粮食所需消耗是否充足
            // 不够就继续生成对应建筑
            // 重复此步骤 
        }

        // 上次调整的价格
        private float lastAdjustedPrice;
        // 调整次数
        private float adjustmentCount;

        // 平滑价格波动
        //private float SmoothPriceChanges(float adjustedPrice)
        //{
            
        //}

        #endregion
    }
}
