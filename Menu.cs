using System.Collections.Generic;

namespace ChiHuoLianMengOOP
{
    /// <summary>
    /// 菜品库（数据类）：用 List&lt;Dish&gt; 管理所有菜品
    /// 职责只有"存数据 + 提供访问"，不含任何输入输出
    /// </summary>
    public class Menu
    {
        private readonly List<Dish> _dishes = new List<Dish>();

        /// <summary>初始化：预置三个菜品</summary>
        public void Seed()
        {
            AddDish("红烧带鱼", 38);
            AddDish("鱼香肉丝", 20);
            AddDish("时令鲜蔬", 10);
        }

        /// <summary>私有方法：加一道菜（Seed 内部使用）</summary>
        private void AddDish(string name, double price)
        {
            _dishes.Add(new Dish { Name = name, Price = price, Likes = 0 });
        }

        /// <summary>获取全部菜品（供打印菜单用）</summary>
        public List<Dish> GetDishes() { return _dishes; }

        /// <summary>菜品数量</summary>
        public int Count { get { return _dishes.Count; } }

        /// <summary>给第 num 道菜点赞（num 从 1 开始）</summary>
        public void Like(int num)
        {
            if (num < 1 || num > _dishes.Count)
            {
                return;
            }
            _dishes[num - 1].Likes++;
        }
    }
}
