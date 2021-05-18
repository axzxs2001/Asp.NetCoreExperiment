using Microsoft.Extensions.Logging;
using RulesEngine.Extensions;
using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngineSample.Services
{
    public class CouponService : ICouponService
    {
        private readonly ILogger<CouponService> _logger;
        private readonly List<Coupon> _userCoupons;
        private readonly Order _order;
        private readonly User _user;
        private readonly List<Goods> _goodses;
        public CouponService(ILogger<CouponService> logger)
        {
            _logger = logger;
            //假装用户用的优惠券
            _userCoupons = new List<Coupon>() {
                new Coupon
                {
                    ID = 1,
                    Code="YH01",
                    Name = "满500减20",
                    BeginTime = DateTime.Parse("2021-01-01"),
                    EndTime = DateTime.Parse("2029-06-01"),
                    Expression = "input1.Details.Sum(s => s.Price * Convert.ToDecimal(s.Quantity))>= 500",
                    Symbol="-",
                    Number=20

                },
                new Coupon
                {
                    ID = 2,
                    Code="YH02",
                    Name = "新人5元券",
                    BeginTime = DateTime.Parse("2021-01-01"),
                    EndTime = DateTime.Parse("2029-06-01"),
                    Expression = "input1.Details.Count>0",
                    Symbol="-",
                    Number=5
                },
                new Coupon
                {
                    ID = 3,
                    Code="YH03",
                    Name = "商品C五折",
                    BeginTime = DateTime.Parse("2021-01-01"),
                    EndTime = DateTime.Parse("2029-06-01"),
                    Expression = "input1.Details.Where(s=>s.GoodsID==\"SP0000003\").Count()>0",
                    Symbol="*",
                    Number=0.5f,
                    GoodsID="SP0000003"
                },
                 new Coupon
                {
                    ID = 4,
                    Code="YH04",
                    Name = "A类商品，满1000减200百",
                    BeginTime = DateTime.Parse("2021-01-01"),
                    EndTime = DateTime.Parse("2029-06-01"),
                    Expression = "input1.Details.Join(input3,o=>o.GoodsID,g=>g.goodsID,(o,g)=>new{o.Price,o.Quantity,g.TypeID}).Where(s=>s.TypeID==\"A\").Sum(s=>s.Price*s.Quantity)>1000",
                    Symbol="*",
                    Number=0.5f,
                    GoodsID="SP0000003"
                },
            };
            //假装当前用户的订单
            _order = new Order
            {
                OrderNo = "NO00000001",
                OrderTime = DateTime.Now,
                Details = new List<Detail>
                {
                    new Detail{ GoodsID="SP0000001", Name="商品A", Price=100m, Quantity=3},
                    new Detail{ GoodsID="SP0000002", Name="商品B", Price=100m, Quantity=4},
                    new Detail{ GoodsID="SP0000003", Name="商品C", Price=100m, Quantity=5},
                }
            };
            _user = new User { ID = 1, UserName = "zhangsan" };

            _goodses = new List<Goods>
            {
                new Goods{ GoodsID="SP0000001",TypeID="A"},
                new Goods{ GoodsID="SP0000002",TypeID="B"},
                new Goods{ GoodsID="SP0000002",TypeID="C"},
            };

        }
        /// <summary>
        /// 适配优惠券
        /// </summary>
        /// <returns></returns>
        public async Task<string> SelectCouponAsync()
        {
            var workRules = new RulesEngine.Models.WorkflowRules();
            workRules.WorkflowName = "优惠券";
            var rules = new List<Rule>();
            foreach (var coupon in _userCoupons.Where(s => s.BeginTime < DateTime.Now && s.EndTime > DateTime.Now))
            {
                var rule = new Rule
                {
                    RuleName = coupon.Name,
                    SuccessEvent = coupon.Code,
                    //ErrorMessage = "规则应用失败",
                    ErrorType = ErrorType.Error,
                    RuleExpressionType = RuleExpressionType.LambdaExpression,
                    Expression = coupon.Expression
                };
                rules.Add(rule);
            }
            workRules.Rules = rules;
            var rulesEngine = new RulesEngine.RulesEngine(new WorkflowRules[] { workRules }, _logger, new ReSettings());
            var ruleResults = await rulesEngine.ExecuteAllRulesAsync("优惠券", _order, _user, _goodses);
            // var valueCoupons = new List<string>();
            //处理结果
            var discountCoupons = new StringBuilder();
            foreach (var ruleResult in ruleResults)
            {
                if (ruleResult.IsSuccess)
                {
                    discountCoupons.AppendLine($"可以使用的优惠券 “{_userCoupons.SingleOrDefault(s => s.Code == ruleResult.Rule.SuccessEvent)?.Name}”, Code是：{ruleResult.Rule.SuccessEvent}");
                    //valueCoupons.Add(ruleResult.Rule.SuccessEvent);
                }
                else
                {
                    Console.WriteLine(ruleResult.ExceptionMessage);
                }
            }
            //resultList.OnSuccess((eventName) =>
            //{
            //    discountOffered += $"可以使用的优惠券“{userCoupons.SingleOrDefault(s => s.Code == eventName)?.Name}”,Code是：{eventName} ";
            //});
            ruleResults.OnFail(() =>
            {
                discountCoupons.AppendLine("您没有适合的优惠券！");
            });
            return discountCoupons.ToString();
        }
        /// <summary>
        /// 计算订单支付总额
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetOrderAmount(string code)
        {
            var selectCoupon = _userCoupons.SingleOrDefault(s => s.Code == code);
            var orderAmount = 0m;
            switch (selectCoupon.Symbol)
            {
                case "-":
                    orderAmount = _order.Details.Sum(s => s.Price * Convert.ToDecimal(s.Quantity)) - Convert.ToDecimal(selectCoupon.Number);
                    break;
                case "*":
                    if (!string.IsNullOrWhiteSpace(selectCoupon.GoodsID))
                    {
                        orderAmount = _order.Details.Sum(s => s.Price * Convert.ToDecimal(s.Quantity));
                        var detail = _order.Details.SingleOrDefault(s => s.GoodsID == selectCoupon.GoodsID);
                        if (detail != null)
                        {
                            orderAmount -= detail.Price * Convert.ToDecimal(detail.Quantity) * Convert.ToDecimal(1 - selectCoupon.Number);
                        }
                    }
                    else
                    {
                        orderAmount = _order.Details.Sum(s => s.Price * Convert.ToDecimal(s.Quantity)) * Convert.ToDecimal(selectCoupon.Number);
                    }
                    break;
            }
            return $"订单总金额：{_order.Details.Sum(s => s.Price * Convert.ToDecimal(s.Quantity)) }，优惠后请支付：{orderAmount}";
        }

    }
    public class Coupon
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Expression { get; set; }
        public string Symbol { get; set; }
        public float Number { get; set; }
        public string GoodsID { get; set; }
    }

    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
    }
    public class Order
    {
        public string OrderNo { get; set; }
        public DateTime OrderTime { get; set; }
        public List<Detail> Details { get; set; }
    }
    public class Detail
    {
        public string GoodsID { get; set; }
        public float Quantity { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }

    }

    public class Goods
    {
        public string GoodsID { get; set; }
        public string TypeID { get; set; }
    }
}
