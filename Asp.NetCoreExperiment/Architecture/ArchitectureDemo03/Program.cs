using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ArchitectureDemo03
{
    class Program
    {
        static void Main(string[] args)
        {
            var readCard033 = new ReadCard033
            {
                TradeCode = "033",
                BeginDate = DateTime.Now,
                EndDate = "20200102"
            };
            var readcard033Back = Send<ReadCard033Back>(readCard033);
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(readcard033Back));


            var readCard027 = new ReadCard027
            {
                Date = "20201212",
                InvoiceNo = "abcd",
                Time = "121212",
                TradeCode = "003",
                TransType = "123"
            };
            var readCard027Back = Send<ReadCard027Back>(readCard027);
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(readCard027Back));

        }
        /// <summary>
        /// 模拟输入函数与输出xml对应集合，以作mock
        /// </summary>
        static Dictionary<string, string> BackDic = new Dictionary<string, string> {
            {"ReadCard033" ,@"<Response>
    <PatientId>病历编号</PatientId>
    <SiHisOrderNo>HIS端结算流水</SiHisOrderNo>
    <PubCost>1</PubCost>
    <PayCost>2</PayCost>
    <OwnCost>3</OwnCost>
    <TotCost>4</TotCost>
    <TransType>1</TransType>
    <OperCode>操作人员编号</OperCode>
    <OperName>操作人员姓名</OperName>
    <PayType>支付方式</PayType>
    <Invoices>
        <INum>单据编号</INum>
        <InvoiceType>单据类别</InvoiceType>
        <ISum>5</ISum>
    </Invoices>
</Response>"},
            { "ReadCard027",$@"<Response>
    <TradeCode>业务编号</TradeCode>
    <Result>0</Result>
    <Err>错误描述信息</Err>
    <HospitalTransNO>本次交易流水</HospitalTransNO>
    <Fees>
        <Fee>
            <fybm>收费项目编码</fybm>
            <fymc>收费项目名称</fymc>
            <ksbm>开立科室编码</ksbm>
            <ksmc>开立科室名称</ksmc>
            <ysbm>开立医生编码</ysbm>
            <ysxm>开立医生姓名</ysxm>
            <kdsj>2020-12-12 13:14:15</kdsj>
            <sfsj>2020-12-12 13:14:15</sfsj>
            <fysl>10</fysl>
            <yxbz>有效标志</yxbz>
            <czy>收费人员编号</czy>
            <zxks>执行科室编号</zxks>
            <InvoiceNo>单据编号</InvoiceNo>
        </Fee> 
        <Fee>
            <fybm>收费项目编码</fybm>
            <fymc>收费项目名称</fymc>
            <ksbm>开立科室编码</ksbm>
            <ksmc>开立科室名称</ksmc>
            <ysbm>开立医生编码</ysbm>
            <ysxm>开立医生姓名</ysxm>
            <kdsj>2020-12-12 13:14:15</kdsj>
            <sfsj>2020-12-12 13:14:15</sfsj>
            <fysl>10</fysl>
            <yxbz>有效标志</yxbz>
            <czy>收费人员编号</czy>
            <zxks>执行科室编号</zxks>
            <InvoiceNo>单据编号</InvoiceNo>
        </Fee> 
    </Fees>
</Response>"}
        };

        /// <summary>
        /// 封装调用接口
        /// </summary>
        /// <typeparam name="T">输出参数类型</typeparam>
        /// <param name="request">输入参数</param>
        /// <returns></returns>
        static T Send<T>(Request request) where T : class
        {
            Console.WriteLine("输入参数：");
            Console.WriteLine(request.ToXML());

            var backXML = BackDic[request.GetType().Name];
            return request.ToResponse(typeof(T), backXML) as T;
        }
    }
    /// <summary>
    /// 请求父类
    /// </summary>
    abstract class Request
    {
        public override string ToString()
        {
            var requestSB = new StringBuilder();
            var type = this.GetType();
            //遍历属性，获取属性值
            foreach (var pro in type.GetProperties())
            {
                //处理Request的子类，所有输入参数都应该继承Request
                if (pro.PropertyType.IsSubclassOf(typeof(Request)))
                {
                    requestSB.AppendLine($"<{pro.Name}>");
                    requestSB.AppendLine($"{pro.GetValue(this)}");
                    requestSB.AppendLine($"</{pro.Name}>");
                }
                else
                {
                    //处理DateTime类型属性
                    if (pro.PropertyType.IsAssignableFrom(typeof(DateTime)))
                    {
                        var value = Convert.ToDateTime(pro.GetValue(this)).ToString("yyyyMMddHHMMSS");
                        requestSB.AppendLine($"<{pro.Name}>{value}</{pro.Name}>");
                    }
                    else
                    {
                        requestSB.AppendLine($"<{pro.Name}>{pro.GetValue(this)}</{pro.Name}>");
                    }
                }
            }
            return requestSB.ToString().Trim();
        }
        /// <summary>
        /// 输成xml输入参数
        /// </summary>
        /// <returns></returns>
        public string ToXML()
        {
            return $"<Request>\n{this}\n</Request>";
        }
        /// <summary>
        /// 输出参数xml转成实体类
        /// </summary>
        /// <param name="type">输出参数类型</param>
        /// <param name="xml">输出参数xml</param>
        /// <returns></returns>
        public object ToResponse(Type type, string xml)
        {
            xml = $@"<?xml version=""1.0"" encoding=""utf-8""?>{xml}";
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var instance = Activator.CreateInstance(type);
            foreach (var pro in type.GetProperties())
            {
                //自定义实体类属性，利用在同一个命名空间里来解瘊这个事情
                if (pro.PropertyType.Namespace == this.GetType().Namespace)
                {
                    var xmlElement = xmlDoc.GetElementsByTagName(pro.Name);
                    if (xmlElement.Count > 0)
                    {
                        var response = ToResponse(pro.PropertyType, $"<{pro.Name}>{xmlElement[0].InnerXml}</{pro.Name}>");
                        pro.SetValue(instance, response);
                    }
                }
                else
                {
                    //泛型集合实体类属性
                    if (pro.PropertyType.IsGenericType)
                    {
                        //获取泛型集合属性的类型
                        var subType = pro.PropertyType.GetGenericArguments()[0];
                        var xmlElement = xmlDoc.GetElementsByTagName(pro.Name);
                        if (xmlElement.Count > 0)
                        {
                            //生成泛型集合属性的实体类
                            var list = Activator.CreateInstance(pro.PropertyType) as IList;
                            //把输出字符串中的列表对应数据添加到泛型属性集合中
                            foreach (XmlNode childItem in xmlElement[0].ChildNodes)
                            {
                                if (childItem.ChildNodes.Count > 0)
                                {
                                    var subInstance = ToResponse(subType, $"<{subType.Name}>{xmlElement[0].ChildNodes[0].InnerXml}</{subType.Name}>");
                                    list.Add(subInstance);
                                }
                            }
                            //设置泛型集合属性的值
                            pro.SetValue(instance, list);
                        }
                    }
                    else
                    {
                        //普通属性
                        var xmlElement = xmlDoc.GetElementsByTagName(pro.Name);
                        if (xmlElement.Count > 0)
                        {
                            var value = Convert.ChangeType(xmlElement[0].InnerText, pro.PropertyType);
                            pro.SetValue(instance, value);
                        }
                    }
                }
            }
            return instance;
        }
    }
    /// <summary>
    /// 医院就诊卡接口规范（业务：033）
    /// </summary>
    class ReadCard033 : Request
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        public string TradeCode { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndDate { get; set; }

    }
    /// <summary>
    /// 医院就诊卡接口规范（业务：033）输出参数
    /// </summary>
    class ReadCard033Back
    {
        /// <summary>
        /// 病历编号
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// HIS端结算流水
        /// </summary>
        public string SiHisOrderNo { get; set; }
        /// <summary>
        /// 医保统筹支付（元）
        /// </summary>
        public decimal PubCost { get; set; }
        /// <summary>
        /// 医保帐户支付（元
        /// </summary>
        public decimal PayCost { get; set; }
        /// <summary>
        /// 患者个人自付（元）
        /// </summary>
        public decimal OwnCost { get; set; }
        /// <summary>
        /// 本次结算总额（元）
        /// </summary>
        public decimal TotCost { get; set; }
        /// <summary>
        /// 结算类别（1消费，0退费）
        /// </summary>
        public int TransType { get; set; }
        /// <summary>
        /// 操作人员编号
        /// </summary>
        public string OperCode { get; set; }
        /// <summary>
        /// 操作人员姓名
        /// </summary>
        public string OperName { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string PayType { get; set; }
        /// <summary>
        /// 单据信息
        /// </summary>
        public Invoices Invoices { get; set; }

    }
    /// <summary>
    /// 单据
    /// </summary>
    class Invoices
    {
        /// <summary>
        /// 单据编号
        /// </summary>
        public string INum { get; set; }
        /// <summary>
        /// 单据类别
        /// </summary>
        public string InvoiceType { get; set; }
        /// <summary>
        /// 单据金额（元）
        /// </summary>
        public decimal ISum { get; set; }
    }
    /// <summary>
    /// 医院就诊卡接口规范（业务：027）
    /// </summary>
    class ReadCard027 : Request
    {
        /// <summary>
        /// 交易码(见上表交易代码)
        /// </summary>
        public string TradeCode { get; set; }
        /// <summary>
        /// 交易日期（YYYYMMDD
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// 交易时间（HHMMSS）
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNo { get; set; }
        /// <summary>
        /// 交易类别
        /// </summary>
        public string TransType { get; set; }
    }
    /// <summary>
    /// 医院就诊卡接口规范（业务：027）输出参数
    /// </summary>
    class ReadCard027Back
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        public string TradeCode { get; set; }
        /// <summary>
        /// 返回值：0 成功，其他失败
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 错误描述信息
        /// </summary>
        public string Err { get; set; }
        /// <summary>
        /// 本次交易流水
        /// </summary>
        public string HospitalTransNO { get; set; }
        /// <summary>
        /// 明细列表
        /// </summary>
        public List<Fee> Fees { get; set; }

    }
    /// <summary>
    /// 明细
    /// </summary>
    class Fee
    {
        /// <summary>
        /// 收费项目编码
        /// </summary>
        public string fybm { get; set; }
        /// <summary>
        /// 收费项目名称
        /// </summary>
        public string fymc { get; set; }
        /// <summary>
        /// 开立科室编码
        /// </summary>
        public string ksbm { get; set; }
        /// <summary>
        /// 开立科室名称
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// 开立医生编码
        /// </summary>
        public string ysbm { get; set; }
        /// <summary>
        /// 开立医生姓名
        /// </summary>
        public string ysxm { get; set; }
        /// <summary>
        /// 开单时间(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public string kdsj { get; set; }
        /// <summary>
        /// 收费时间(yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public string sfsj { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string fysl { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public string yxbz { get; set; }
        /// <summary>
        /// 收费人员编号
        /// </summary>
        public string czy { get; set; }
        /// <summary>
        /// 执行科室编号
        /// </summary>
        public string zxks { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string InvoiceNo { get; set; }

    }
}