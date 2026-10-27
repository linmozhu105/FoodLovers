using System;

namespace ChiHuoLianMengOOP
{
    /// <summary>
    /// 页面类（界面层）：负责打印菜单、读取用户选择、分发到功能类
    /// 通过构造方法注入 OrderService，自己不碰数据
    /// </summary>
    public class MainPage
    {
        private readonly OrderService _service;

        public MainPage(OrderService service)
        {
            _service = service;
        }

        /// <summary>启动主界面：菜单循环直到用户选 6 退出</summary>
        public void Run()
        {
            while (true)
            {
                PrintHeader();
                Console.WriteLine("1.我要订餐");
                Console.WriteLine("2.查看餐袋");
                Console.WriteLine("3.签收订单");
                Console.WriteLine("4.删除订单");
                Console.WriteLine("5.我要点赞");
                Console.WriteLine("6.退出系统");
                Console.WriteLine("*******************************************");

                Console.Write("请选择你的功能：");
                int choice = ReadIntInRange(1, 6, "请重新选择你的功能：");

                switch (choice)
                {
                    case 1: _service.PlaceOrder(); break;      // 订餐
                    case 2: _service.ViewOrders(); break;      // 查看餐袋
                    case 3: _service.ConfirmOrder(); break;    // 签收
                    case 4: _service.DeleteOrder(); break;     // 删除
                    case 5: _service.LikeDish(); break;        // 点赞
                    case 6:
                        Console.WriteLine("你已退出系统");
                        return;
                }
            }
        }

        /// <summary>打印系统标题</summary>
        private void PrintHeader()
        {
            Console.WriteLine("\n*******************************************");
            Console.WriteLine("欢迎使用“吃货联盟订餐系统”");
            Console.WriteLine("*******************************************");
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
    }
}
