using System.Collections.Generic;

namespace ChiHuoLianMengOOP
{
    /// <summary>
    /// 订单库（数据类）：用 List&lt;Order&gt; 管理所有订单
    /// 职责只有"存数据 + 提供访问"，不含任何输入输出
    /// </summary>
    public class OrderManager
    {
        private readonly List<Order> _orders = new List<Order>();

        /// <summary>初始化：预置两条示例订单（跟需求文档的用例数据一致）</summary>
        public void Seed()
        {
            _orders.Add(new Order
            {
                CustomerName = "张晴",
                DishName = "红烧带鱼",
                Quantity = 2,
                DeliveryHour = 12,
                DeliveryAddress = "天成路207号",
                TotalAmount = 76,   // 38 * 2 = 76，已满50，不加送餐费
                Status = 1          // 1=已签收
            });

            _orders.Add(new Order
            {
                CustomerName = "张晴",
                DishName = "鱼香肉丝",
                Quantity = 2,
                DeliveryHour = 18,
                DeliveryAddress = "天成路207号",
                TotalAmount = 45,   // 20 * 2 = 40，不满50，加5元送餐费 = 45
                Status = 0          // 0=待签收
            });
        }

        /// <summary>新增一条订单：List 自动追加到末尾，永远不会满</summary>
        public void Add(Order order) { _orders.Add(order); }

        /// <summary>获取全部订单（供查看餐袋遍历用）</summary>
        public List<Order> GetOrders() { return _orders; }

        /// <summary>订单数量</summary>
        public int Count { get { return _orders.Count; } }

        /// <summary>
        /// 签收第 num 条订单（num 从 1 开始）
        /// </summary>
        /// <returns>1=成功；0=已签收过；-1=订单不存在</returns>
        public int Confirm(int num)
        {
            int index = num - 1;
            if (index < 0 || index >= _orders.Count)        // 订单不存在
            {
                return -1;
            }
            if (_orders[index].Status == 1)                 // 已经签收过
            {
                return 0;
            }
            _orders[index].Status = 1;                      // 正常签收：0 → 1
            return 1;
        }

        /// <summary>
        /// 删除第 num 条订单（num 从 1 开始），只允许删除"已签收"的订单
        /// 对比数组版：原来要写 for 循环把后面的元素依次前移、把最后一个元素清空；
        /// 用 List 的 RemoveAt 一步就删掉，后面的元素自动前移，省心且不会出错
        /// </summary>
        /// <returns>1=成功；0=未签收不能删；-1=订单不存在</returns>
        public int Delete(int num)
        {
            int index = num - 1;
            if (index < 0 || index >= _orders.Count)        // 订单不存在
            {
                return -1;
            }
            if (_orders[index].Status != 1)                 // 还没签收
            {
                return 0;
            }
            _orders.RemoveAt(index);                        // 删除（自动前移）
            return 1;
        }
    }
}
