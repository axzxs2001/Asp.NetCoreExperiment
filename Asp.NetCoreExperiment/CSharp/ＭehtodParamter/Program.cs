#region in
Console.WriteLine("in 参数 只能从外部把值传入方法内部使用");
var i = 10;
InMethod01(in i);
Console.WriteLine($"外：{i}");
//InMethod01(in 10);//错误，in后不能是实际的数据

//可以直接传入数据
InMethod01(10);


//order内的OrderNo传入方法内部有可能被改掉，但order不可以被替换
var order = new Order { OrderNo = "O000000" };
InMethod02(in order);
Console.WriteLine($"外：{order}");
//InMethod02(in new Order { OrderNo = "abcd" });//错误，in后不能是实际的数据

//可以直接传入数据
InMethod02(new Order { OrderNo = "O000000" });



//int i1;
//Order order1;
//InMethod03(in order1 in i1);//错误，不能是没有赋值的变量

int i1 = 1;
Order order1 = new Order() { OrderNo = "O000000" };
//方法的参数列表中可以有多个in
InMethod03(in order1, in i1);//错误，不能是没有赋值的变量

InMethod03(new Order { OrderNo = "O000000" }, 20);


static void InMethod01(in int i)
{
    //i = 10;//错误
    Console.WriteLine($"内：{i}");
}
static void InMethod02(in Order order)
{
    //order = new Order();//错误
    order.OrderNo = "O000002"; //正确，这里可以参数成员赋值
    Console.WriteLine($"内：{order}");
}
static void InMethod03(in Order order, in int i)
{
    //i = 10;
    Console.WriteLine($"内：{i}");
    //order = new Order(); //错误

    //order.OrderNo = "O000001"; //正确，这里可以参数成员赋值
    Console.WriteLine($"内：{order}");
}
#endregion


#region out
//Console.WriteLine("out 参数，是从方法内部把数据带出来");

////定义调用一起
//OutMethod01(out int i1);
//Console.WriteLine($"外 :{i1}");

////定义调用分开
//int i2=20; //即使赋值，方法内部接收不到
//OutMethod01(out i2);
//Console.WriteLine($"外  :{i2}");

////定义调用一起
//OutMethod02(out Order order1);
//Console.WriteLine($"外  :{order1}");

////定义调用分开
//Order order2;
//OutMethod02(out order2);
//Console.WriteLine($"外  :{order2}");

////多个out同时存在
//OutMethod03(out Order order3, out int i3);
//Console.WriteLine($"外  :{order3}");
//Console.WriteLine($"外  :{i3}");

//Order order4;
//int i4=20;//这里的20是能传入OutMethod03中的，但在方法内部，不能在给i赋值前使用
//OutMethod03(out order4, out i4);
//Console.WriteLine($"外  :{order4}");
//Console.WriteLine($"外  :{i4}");

////带有out的参数，在方法内即使有值，也能不使用，只有赋值后才能使用
//static void OutMethod01(out int i)
//{
//    //Console.WriteLine(i);//错误
//    i = 10;
//    Console.WriteLine(i);
//}
//static void OutMethod02(out Order order)
//{
//    //Console.WriteLine(order); //错误
//    order = new Order { OrderNo = "O000001" };
//    Console.WriteLine(order);
//}
//static void OutMethod03(out Order order, out int i)
//{
//    //Console.WriteLine(i);//错误
//    //Console.WriteLine(order); //错误
//    i = 10;
//    order = new Order { OrderNo = "O000001" };
//    Console.WriteLine(i);//错误
//    Console.WriteLine(order); //错误
//}
#endregion


#region ref
//Console.WriteLine("ref 参数，即能把外部的数据传入，也能把方法里的参数变化值传出，这里更多的是把参数转成一个引用，穿透方法内外共享");

////RefMethod01(ref 10);//错误，这里只能传入一个变量，不能是具体的数据

////int i1;//错误，ref要求传入必须有值
////RefMethod01(ref i1);//错误
//int i1 = 1;
//RefMethod01(ref i1);
//Console.WriteLine($"外 :{i1}");


//var order1 = new Order { OrderNo = "O000000" };
//RefMethod02(ref order1);
//Console.WriteLine($"外 :{order1}");


//Order order2 = new Order { OrderNo = "O000000" };
//int i2 = 1;
//RefMethod03(ref order2, ref i2);
//Console.WriteLine($"外 :{order2}");
//Console.WriteLine($"外 :{i2}");


//static void RefMethod01(ref int i)
//{
//    Console.WriteLine($"前：{i}");
//    i = 10;
//    Console.WriteLine($"后：{i}");
//}
//static void RefMethod02(ref Order order)
//{
//    Console.WriteLine($"前：{order}");
//    order = new Order { OrderNo = "O000001" };
//    Console.WriteLine($"后：{order}");
//}
//static void RefMethod03(ref Order order, ref int i)
//{
//    Console.WriteLine($"前：{i}");
//    Console.WriteLine($"前：{order}");
//    i = 10;
//    order = new Order { OrderNo = "O000001" };
//    Console.WriteLine($"后：{i}");
//    Console.WriteLine($"后：{order}");
//}
#endregion
public class Order
{
    public string OrderNo { get; set; }

    public override string ToString()
    {
        return OrderNo;
    }
}