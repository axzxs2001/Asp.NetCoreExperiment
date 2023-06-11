//参照项目中的 ZuHu.cs和ZuHuService.cs，生成订单实体类，属性有：编号，生成日期，下单人，订单状态
public class Order
{
    public int Id { get; set; }
    public DateTime CreateTime { get; set; }
    public string OrderPerson { get; set; }
    public string OrderStatus { get; set; }
}


