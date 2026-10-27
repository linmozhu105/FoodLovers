namespace ChiHuoLianMengOOP
{
    /// <summary>
    /// 程序入口：负责"组装"整个系统（初始化 + 依赖注入 + 启动）
    /// 面向对象思维：只创建一次对象，全程共享，保证数据一致
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. 创建菜品库并预置数据
            Menu menu = new Menu();
            menu.Seed();

            // 2. 创建订单库并预置数据
            OrderManager orderManager = new OrderManager();
            orderManager.Seed();

            // 3. 创建功能类（把两个库注入进去）
            OrderService service = new OrderService(orderManager, menu);

            // 4. 创建页面并启动
            MainPage mainPage = new MainPage(service);
            mainPage.Run();
        }
    }
}
