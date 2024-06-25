！停止开发，半成品，没有参考与学习价值！


# EconomicSystem
 使用Unity实现的一个拥有简单经济机制的小Demo，市场价格根据供需关系涨跌。
 
 *待实现
 
 ### 接口
 IEconomicManager 为经济系统提供运算必备的数据
 
 ### 经济系统运行策略
 1.根据历史供需预测未来资源需求*
 2.强制为建筑下达生产任务，维持供需平衡，保持基础资源仓库充足*
 3.预测短时间难以消除供需失衡，则使用调整价格策略，强制介入
 
 ### 经济系统运行策略所需数据
 1.市场上所有流通的物品ID、数量
 2.所有生产建筑
 3.物品平均供需量、历史供需量、当天供需量
 4.物品的价格弹性、均衡价格、原始价格、当前价格
 5.物品价格调整阈值
 6.物品的缓冲量(基础物资)
 7.物品的市场存货量
 
 ### 经济个体及其功能
	#### 建筑 Building
	优先生产下达任务*
	根据利益选择日常生产任务
	招聘工人
	进货
	生产
	推送到市场
	
	#### NPC 
	找工作
	生产
	与经济相关的行为*
		消费
		
	
