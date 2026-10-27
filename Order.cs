namespace ChiHuoLianMengOOP
{
    /// <summary>
    /// 订单实体类：一条订单的所有信息
    /// 对比数组版：原来一条订单要拆成 6 个平行数组（names/dishMegs/times/addresses/states/sumPrices）分别存，
    /// 下标对不上就容易出错；面向对象后，用一个 Order 对象就把一条订单的所有字段装在一起。
    /// Status：0=待签收(已预订)，1=已签收(已完成)
    /// </summary>
    public class Order
    {
        public string CustomerName { get; set; }    // 订餐人姓名
        public string DishName { get; set; }        // 菜名
        public int Quantity { get; set; }           // 份数
        public int DeliveryHour { get; set; }       // 送餐时间（整点小时）
        public string DeliveryAddress { get; set; } // 送餐地址
        public double TotalAmount { get; set; }     // 订单总金额（元）
        public int Status { get; set; }             // 0=待签收(已预订)，1=已签收(已完成)
    }
}
