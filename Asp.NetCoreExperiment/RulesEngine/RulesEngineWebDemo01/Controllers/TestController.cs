using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using RulesEngine;
using RulesEngine.Models;
using RulesEngine.Extensions;

namespace RulesEngineWebDemo01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var workRules = new RulesEngine.Models.WorkflowRules();
            workRules.WorkflowName = "折扣";
            workRules.Rules = new List<Rule>
            {
                new Rule
                {
                    RuleName="活动一",
                    SuccessEvent= "10",
                    ErrorMessage="One or more adjust rules failed.",
                    ErrorType=ErrorType.Error,
                    RuleExpressionType= RuleExpressionType.LambdaExpression,
                    Expression= "input1.country == \"india\" AND input1.loyalityFactor <= 2 AND input1.totalPurchasesToDate >= 5000"
                },
                new Rule
                {
                    RuleName="活动二",
                    SuccessEvent= "20",
                    ErrorMessage="One or more adjust rules failed.",
                    ErrorType=ErrorType.Error,
                    RuleExpressionType= RuleExpressionType.LambdaExpression,
                    Expression= "input1.country == \"india\" AND input1.loyalityFactor >= 3 AND input1.totalPurchasesToDate >= 10000"
                },
            };
            var rulesEngine = new RulesEngine.RulesEngine(new WorkflowRules[] { workRules }, _logger, new ReSettings());
            var resultList = await rulesEngine.ExecuteAllRulesAsync("折扣", new Country
            {
                loyalityFactor = 5,
                totalPurchasesToDate = 20000
            });
            var discountOffered = "";
            resultList.OnSuccess((eventName) =>
            {
                discountOffered = $"Discount offered is {eventName} % over MRP.";
            });
            resultList.OnFail(() =>
            {
                discountOffered = "The user is not eligible for any discount.";
            });
            return discountOffered;
        }
    }

    class Country
    {
        public string country { get; set; } = "india";
        public int loyalityFactor { get; set; }
        public int totalPurchasesToDate { get; set; }
    }
}
