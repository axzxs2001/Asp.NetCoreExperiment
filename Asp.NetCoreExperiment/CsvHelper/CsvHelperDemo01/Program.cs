using Bogus;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using static System.Console;
namespace CsvHelperDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = $"{Directory.GetCurrentDirectory()}/goodses.csv";
            WriteCsv(file, true);
            Console.WriteLine("Write is ok!");

            foreach (var goods in ReadCsv(file, true))
            {
                WriteLine(goods);
            }
        }

        static List<Goods> ReadCsv(string file, bool isBOM = false)
        {
            MemoryStream memory = null;
            if (isBOM)
            {
                using (var reader = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    var bytes = new byte[reader.Length];
                    reader.Read(bytes, 0, bytes.Length);

                    bytes = bytes.Skip(3).Take(bytes.Length - 3).ToArray();

                    memory = new MemoryStream(bytes);
                }
            }
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = "\r\n"
            };
            if (memory == null)
            {
                using (var reader = new StreamReader(file))
                using (var csv = new CsvReader(reader, config))
                {
                    var goodses = csv.GetRecords<Goods>();
                    return goodses.ToList();
                }
            }
            else
            {
                using (var reader = new StreamReader(memory))
                using (var csv = new CsvReader(reader, config))
                {
                    var goodses = csv.GetRecords<Goods>();
                    return goodses.ToList();
                }
            }
        }
        static void WriteCsv(string file, bool isBOM = false)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = "\r\n"
            };

            var goodses = GoodsCreater();
            using (var writer = new StreamWriter(file))
            {
                using (var csv = new CsvWriter(writer, config))
                {                  
                    csv.WriteRecords(goodses);
                }
            }
            if (isBOM)
            {
                //追究加BOM
                using (var writer = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    var bytes = new byte[writer.Length];
                    writer.Read(bytes, 0, bytes.Length);
                    byte[] BOM = { 0xEF, 0xBB, 0xBF };
                    var list = new List<byte>();
                    list.AddRange(BOM);
                    list.AddRange(bytes);
                    writer.Position = 0;
                    writer.Write(list.ToArray(), 0, list.Count);
                }
            }
        }
      
        static List<Goods> GoodsCreater()
        {
            var goodses = new List<Goods>();
            var options = new JsonSerializerOptions();
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All);
            for (var i = 0; i < 100; i++)
            {
                var goodsFaker = new Faker<Goods>("zh_CN")
                    .RuleFor(x => x.ID, x => x.Random.UInt())
                    .RuleFor(x => x.Name, x => x.Random.ArrayElement(
                        new string[]
                        {
 "甲销唑注射液",
"葡萄糖注射液",
"盐水（氯化钠）",
"利巴韦林病毒唑针",
"氟康唑氯化钠注射液(康锐）",
"安痛定针",
"维生素B12针",
"硫酸阿米卡星注射液",
"氯化钠注射液（塑料瓶）",
"转移因子注射液",
"血塞通粉针",
"苦碟子注射液",
"注射用培美曲塞二钠(爱立汀)",
"左克注射液",
"维生素C注射液",
"硫酸庆大霉素注射液",
"盐酸左氧氟沙星注射液",
"蔗糖铁注射液",
"注射用青霉素钠",
"维生素B1针",
"疏血通注射液",
"胃复安盐酸甲氧氯普胺注射液",
"硫酸庆大霉素注射液",
"葡萄糖注射液50%",
"银杏达莫注射液",
"硫酸庆大霉素注射液",
"5%葡萄糖注射液",
"注射用氨曲南",
                        }
                        ))
                    .RuleFor(x => x.Price, x => x.Random.Decimal())
                    .RuleFor(x => x.Unit, x => x.Random.ArrayElement(new string[] { "个", "片", "瓶", "盒", "支", "克" }))
                    .RuleFor(x => x.Spec, x => x.Random.ArrayElement(new string[]
                       {
 "5*20ml",
"250ml",
"1ml*10支",
"100ml:0.2g",
"2毫升*10支",
"10支",
"2ml*10.2克*10支",
"100ml",
"3毫升：2毫升*10支",
"20毫克",
"10mg*10支",
"0.5克",
"2ml:0.1g",
"0.25G*2ML",
"4万单位*10支",
"100ml*0.1g",
"1g",
"80万单位",
"2ml*10支",
"5毫克*60片",
"1ml:10mg",
"4万*2ml*10支",
"20ML*5支",
"5ml",
"2ml:40mg(4万单位)",
"250ml",
"0.5克*10支",
"5ML;17.5MG",
"2ML:0.5G*10支",
                       }))
                    .RuleFor(x => x.Manufacturer, x => x.Random.ArrayElement(
                        new string[]
                    {
 "山东齐都药业/山西云鹏",
"侯马霸王药业/山西晋新双鹤/天津新郑/西安/贵州",
"山西临汾云鹏药业、山东齐都药业/山西银湖/石家庄四药/河南科伦",
"安徽联谊/无锡",
"扬子江药业集团",
"新乡常乐制药/山西太原药业/天津焦作/郑州",
"天津药业焦作",
"山东方明药业集团股份有限公司",
"石家庄四药有限公司",
"湖南一格制药有限公司",
"哈尔滨",
"通化华夏制药有限责任公司",
"德州德药制药有限公司",
"扬子江药业",
"山西晋新双鹤药业/新乡市新辉药业/河南润弘",
"河南辅仁怀庆堂制药/濮阳市汇元药业/新乡常乐",
"广西裕源药业、四川科伦大药厂",
"成都天台山制药有限公司",
"华北制药",
"安阳九州药业、山东方明药业/石药银湖制药",
"牡丹江友博药业有限公司",
"濮阳汇元/天津药业集团新郑",
"天津药业焦作/侯马",
"湖南科伦/焦作民康",
"贵州益佰制药股份有限公司",
"新乡市新辉药业/新乡常乐制药/濮阳市汇元药业",
"山东华鲁制药/山东齐都/山西云鹏制药/西安汉丰",
"重庆圣华曦药业",
                    }));
                goodses.Add(goodsFaker.Generate());
            }
            return goodses;
        }

    }

    /// <summary>
    /// 商品
    /// </summary>
    public class Goods
    {
        /// <summary>
        /// 编号
        /// </summary>
        public uint ID
        { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Spec
        { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit
        { get; set; }
        /// <summary>
        /// 制造商
        /// </summary>
        public string Manufacturer
        { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price
        { get; set; }

        public override string ToString()
        {
            return $"ID={ID},Name={Name},Unit={Unit},Spec={Spec},Price={Price},Manufacturer={Manufacturer}";
        }
    }
}
