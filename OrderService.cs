using System;
using System.Collections.Generic;

namespace ChiHuoLianMengOOP
{
    /// <summary>
    /// 功能类：把"订餐/查看/签收/删除/点赞"五个功能集中在这里
    /// 通过构造方法注入订单库和菜品库（依赖注入），自己不存数据
    /// </summary>
    public class OrderService
    {
        private readonly OrderManager _orderManager;
        private readonly Menu _menu;

        public OrderService(OrderManager orderManager, Menu menu)
        {
            _orderManager = orderManager;
            _menu = menu;
        }

        /// <summary>功能1：我要订餐</summary>
        public void PlaceOrder()
        {
            Console.WriteLine("\n****************** 我要订餐 ********************");

            Console.Write("请输入你的订餐姓名：");
            string customerName = Console.ReadLine();

            PrintMenu();

            Console.Write("请选择菜品编号：");
            int dishNumber = ReadIntInRange(1, _menu.Count, "请重新选择菜品编号：");
            Dish chosenDish = _menu.GetDishes()[dishNumber - 1];

            Console.Write("请输入菜品数量：");
            int quantity = ReadIntAtLeast(1, "请重新输入菜品数量：");

            Console.Write("请输入送餐时间(10-20点整点)：");
            int deliveryHour = ReadIntInRange(10, 20, "请重新输入送餐时间(10-20点整点)：");

            Console.Write("请输入送餐地址：");
            string address = Console.ReadLine();

            // 总金额 = 菜品单价 * 份数 + 送餐费；不满50元加收5元送餐费
            double mealFee = chosenDish.Price * quantity;                 // 餐费 = 单价 × 份数
            Order order = new Order
            {
                CustomerName = customerName,
                DishName = chosenDish.Name,
                Quantity = quantity,
                DeliveryHour = deliveryHour,
                DeliveryAddress = address,
                TotalAmount = mealFee < 50 ? mealFee + 5 : mealFee,       // 不足50加5配送费
                Status = 0                                                // 新订单：待签收
            };

            _orderManager.Add(order);                                     // List 自动追加，不会满

            Console.WriteLine("订餐成功");
            Console.WriteLine($"你的订的是：{order.DishName}{order.Quantity}份");
            Console.WriteLine($"送餐时间：{order.DeliveryHour}");
            Console.WriteLine($"送餐地址：{order.DeliveryAddress}");
            Console.WriteLine($"餐费：{mealFee}，运送费{order.TotalAmount - mealFee}，总计{order.TotalAmount}");
        }

        /// <summary>功能2：查看餐袋（列出所有订单）</summary>
        public void ViewOrders()
        {
            Console.WriteLine("\n****************** 查看餐袋 ********************");
            List<Order> orders = _orderManager.GetOrders();
            if (orders.Count == 0)
            {
                Console.WriteLine("当前没有任何订单。");
                return;
            }

            Console.WriteLine("序号\t订餐人\t菜名\t份数\t送餐时间\t地址\t\t金额\t状态");
            for (int i = 0; i < orders.Count; i++)
            {
                Order o = orders[i];
                string statusText = o.Status == 1 ? "已签收" : "待签收";
                Console.WriteLine($"{i + 1}\t{o.CustomerName}\t{o.DishName}\t{o.Quantity}\t{o.DeliveryHour}点\t{o.DeliveryAddress}\t{o.TotalAmount}\t{statusText}");
            }
        }

        /// <summary>功能3：签收订单</summary>
        public void ConfirmOrder()
        {
            Console.WriteLine("\n****************** 签收订单 ********************");
            Console.Write("请输入要签收的订单序号：");
            int num = ReadIntAtLeast(1, "请输入正整数序号：");

            int result = _orderManager.Confirm(num);
            if (result == 1)
            {
                Console.WriteLine($"第 {num} 单签收成功！");
            }
            else if (result == 0)
            {
                Console.WriteLine($"第 {num} 单已经签收过了。");
            }
            else
            {
                Console.WriteLine($"第 {num} 单不存在。");
            }
        }

        /// <summary>功能4：删除订单（只允许删已签收的）</summary>
        public void DeleteOrder()
        {
            Console.WriteLine("\n****************** 删除订单 ********************");
            Console.Write("请输入要删除的订单序号：");
            int num = ReadIntAtLeast(1, "请输入正整数序号：");

            int result = _orderManager.Delete(num);
            if (result == 1)
            {
                Console.WriteLine($"第 {num} 单删除成功！");
            }
            else if (result == 0)
            {
                Console.WriteLine($"第 {num} 单还未签收，不能删除。");
            }
            else
            {
                Console.WriteLine($"第 {num} 单不存在。");
            }
        }

        /// <summary>功能5：我要点赞</summary>
        public void LikeDish()
        {
            Console.WriteLine("\n****************** 我要点赞 ********************");
            PrintMenu();
            Console.Write("请输入要点赞的菜品编号：");
            int num = ReadIntInRange(1, _menu.Count, "请重新输入菜品编号：");
            _menu.Like(num);
            Console.WriteLine($"第 {num} 道菜点赞成功！");
        }

        // ---------- 私有辅助方法 ----------

        /// <summary>打印菜品菜单</summary>
        private void PrintMenu()
        {
            Console.WriteLine("序号\t菜名\t\t单价\t点赞数");
            List<Dish> dishes = _menu.GetDishes();
            for (int i = 0; i < dishes.Count; i++)
            {
                Dish d = dishes[i];
                Console.WriteLine($"{i + 1}\t{d.Name}\t{d.Price}\t{d.Likes}");
            }
        }

        /// <summary>读取 min~max 范围内的整数，非法就重输</summary>
        private int ReadIntInRange(int min, int max, string retryMessage)
        {
            while (true)
            {
                bool ok = int.TryParse(Console.ReadLine(), out int value);
                if (ok && value >= min && value <= max)
                {
                    return value;
                }
                Console.Write(retryMessage);
            }
        }

        /// <summary>读取不小于 min 的整数，非法就重输</summary>
        private int ReadIntAtLeast(int min, string retryMessage)
        {
            while (true)
            {
                bool ok = int.TryParse(Console.ReadLine(), out int value);
                if (ok && value >= min)
                {
                    return value;
                }
                Console.Write(retryMessage);
            }
        }
    }
}
