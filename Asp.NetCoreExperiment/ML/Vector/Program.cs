#pragma warning disable SKEXP0020 
#pragma warning disable SKEXP0001 

using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Connectors.InMemory;
using Microsoft.SemanticKernel.Connectors.Redis;
using Microsoft.SemanticKernel.Embeddings;

using StackExchange.Redis;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Data;
using Npgsql;
using Dapper;
using System.Diagnostics;
using Microsoft.SemanticKernel.Data;

var jobDescriptions = new List<Job>
{
    new Job
    {
        JobId = "1",
        JobTitle = "软件开发工程师",
        Description = "负责设计、开发和维护高性能的应用程序。参与项目需求分析，与团队协作制定技术方案。利用主流开发语言（如Java、Python或C#）编写高质量的代码，确保软件的可扩展性和稳定性。通过单元测试和集成测试排查问题，并优化系统性能。岗位需要较强的编程能力和逻辑思维，同时熟悉数据库技术和分布式系统的架构设计是加分项。公司提供完善的培训机制和职业发展规划，为员工成长提供支持。",
        Tags ="开发,编程,技术"
    },
    new Job
    {
        JobId = "2",
        JobTitle = "数据分析师",
        Description = "负责收集、处理和分析企业的数据，为业务决策提供数据支持。利用统计工具（如Excel、Python、SQL）处理数据，并生成可视化的报告。协助团队建立数据分析模型，通过挖掘潜在的数据价值，提出改善业务流程的建议。需要对数据敏感，并能快速识别趋势和异常。熟悉大数据平台（如Hadoop、Spark）或机器学习算法者优先。公司注重数据文化，提供开放的学习环境和技术交流机会。",
        Tags =  "数据,分析,决策"
    },
    new Job
    {
        JobId = "3",
        JobTitle = "产品经理",
        Description = "负责产品的全生命周期管理，包括市场调研、需求分析、产品设计、开发跟进和上线后的迭代优化。通过与用户沟通和市场调研，定义产品功能和目标。撰写详细的需求文档，与开发、设计团队协作实现产品落地。持续监测产品的市场表现，分析数据并提出优化建议。需要具备优秀的沟通能力和跨团队协作能力，同时对用户体验和市场趋势有敏锐的洞察力。公司提供良好的发展空间和跨部门交流的机会。",
       Tags ="产品,管理,创新"
    },
    new Job
    {
        JobId = "4",
        JobTitle = "市场营销专员",
        Description = "协助制定并执行市场营销计划，提升品牌知名度和市场占有率。参与市场活动的策划和执行，包括线上推广、广告投放和线下活动组织。分析市场数据和竞争对手动态，优化营销策略。通过社交媒体和其他数字渠道建立品牌形象，吸引潜在客户并维护现有客户关系。需要对市场趋势敏感，具备创意和分析能力。公司提供良好的职业晋升通道和培训资源。",
        Tags = "营销,品牌,活动"
    },
    new Job
    {
        JobId = "5",
        JobTitle = "人力资源专员",
        Description = "负责招聘、培训和员工关系管理工作，协助公司搭建高效的团队。根据公司需求制定招聘计划，通过多种渠道寻找合适的人才。参与设计并实施员工培训计划，帮助员工职业发展。关注员工的需求和满意度，解决工作中遇到的问题。协助管理公司人事制度，确保符合劳动法规定。需要具备优秀的沟通能力和亲和力，熟悉人力资源相关政策和工具者优先。公司注重员工关怀，提供丰富的福利和成长机会。",
        Tags ="招聘,培训,员工关系"
    },
    new Job
    {
        JobId = "6",
        JobTitle = "财务分析师",
        Description = "负责公司财务数据的收集、整理和分析，为高层提供财务决策支持。编制月度、季度和年度财务报告，监测公司财务状况和运营效率。协助预算和成本控制，提供改进建议。通过分析关键财务指标，帮助公司优化资源配置和实现盈利目标。需要熟悉财务管理软件（如SAP、Oracle）以及具有较强的数据处理能力。公司支持职业认证（如CPA、CFA）并提供广阔的发展空间。",
        Tags ="财务,分析,预算"
    },
    new Job
    {
        JobId = "7",
        JobTitle = "UI/UX设计师",
        Description = "负责公司产品的界面设计和用户体验优化。根据需求绘制线框图、设计草图和高保真原型，确保产品美观且易于使用。与产品经理和开发团队紧密合作，实现设计方案的落地。通过用户研究和数据分析，不断改进界面设计，提升用户满意度。熟悉常用设计工具（如Sketch、Figma）和前端技术（如HTML、CSS）者优先。公司为设计师提供创意自由和开放的设计氛围。",
        Tags = "设计,用户体验,创意"
    },
    new Job
    {
        JobId = "8",
        JobTitle = "客户服务专员",
        Description = "负责为客户提供优质的服务和支持，解决客户在使用产品过程中遇到的问题。通过电话、邮件或在线聊天与客户沟通，确保客户满意度。记录并跟踪客户反馈，协助优化产品和服务流程。需要具备耐心、良好的沟通能力和问题解决能力。熟悉CRM工具（如Salesforce）者优先。公司重视客户体验，提供完整的培训体系和职业发展规划。",
       Tags = "客户服务,支持,沟通"
    },
    new Job
    {
        JobId = "9",
        JobTitle = "网络安全工程师",
        Description = "负责公司网络和系统的安全保护，防止网络攻击和数据泄露。设计和实施安全策略，定期进行漏洞扫描和渗透测试。监控网络活动，快速响应安全事件并修复漏洞。协助开发团队设计安全的系统架构，确保数据安全。熟悉网络协议和常见安全工具（如Wireshark、Nmap），并具备信息安全认证（如CISSP）者优先。公司提供先进的技术平台和丰富的项目实践机会。",
        Tags = "网络安全,防护,漏洞"
    },
    new Job
    {
        JobId = "10",
        JobTitle = "供应链管理专员",
        Description = "负责优化公司供应链流程，确保物流和库存的高效管理。与供应商和合作伙伴保持良好的沟通，协调采购、运输和库存控制。分析供应链数据，发现并解决潜在问题，降低成本并提升效率。熟悉ERP系统和供应链管理软件（如SAP SCM）。需要良好的组织能力和抗压能力。公司为员工提供全面的职业发展支持和全球化的业务视野。",
        Tags ="供应链,管理,优化"
    },
};
await RedisVector();

async Task RedisVector()
{
    #region
    //var redisConfiguration = new ConfigurationOptions
    //{
    //    EndPoints = { "localhost:6379" },
    //    User = "",
    //    Password = "",
    //    ConnectTimeout = 1000,
    //    ConnectRetry = 3
    //};
    //var connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(redisConfiguration);
    //var database = connectionMultiplexer.GetDatabase();
    //var vectorStore = new RedisVectorStore(database);
    #endregion
    var vectorStore = new RedisVectorStore(ConnectionMultiplexer.Connect("localhost:6379").GetDatabase());
    var jobStore = vectorStore.GetCollection<string, Job>("1234565");

    var generator = new OllamaEmbeddingGenerator(new Uri("http://localhost:11434/"), "mxbai-embed-large");
    foreach (var job in jobDescriptions)
    {
        job.DescriptionEmbedding = await generator.GenerateEmbeddingVectorAsync(job.JobTitle + "。"
             + job.Tags + "。"
            + job.Description);

        await jobStore.UpsertAsync(job);
    };
    Console.WriteLine("开始搜索");
    var vectorSearchOptions = new VectorSearchOptions
    {
        VectorPropertyName = nameof(Job.DescriptionEmbedding),
        Top = 3
    };

    while (true)
    {
        Console.WriteLine("请输入搜索内容");
        Console.ForegroundColor = ConsoleColor.Yellow;
        var search = Console.ReadLine();
        Console.ResetColor();
        var sw = Stopwatch.StartNew();
        var searchVector = await generator.GenerateEmbeddingVectorAsync(search);
        sw.Stop();
        Console.WriteLine(sw.ElapsedMilliseconds);
        var results = await jobStore.VectorizedSearchAsync(searchVector, vectorSearchOptions);

        await foreach (var result in results.Results)
        {
            Console.WriteLine($"Title: {result.Record.JobTitle}");
            Console.WriteLine($"Description: {result.Record.Description}");
            Console.WriteLine($"Score: {result.Score}");
            Console.WriteLine();
        }
    }
}
async Task PGVector()
{
    var generator = new OllamaEmbeddingGenerator(new Uri("http://localhost:11434/"), "mxbai-embed-large");
    foreach (var hotel in jobDescriptions)
    {
        hotel.DescriptionEmbedding = await generator.GenerateEmbeddingVectorAsync(hotel.JobTitle + "。"
            + hotel.Tags + "。"
            + hotel.Description);
        InsertImageVector(hotel);
    };
    while (true)
    {
        Console.WriteLine("请输入搜索内容");
        Console.ForegroundColor = ConsoleColor.Yellow;
        var search = Console.ReadLine();
        Console.ResetColor();
        var sw = Stopwatch.StartNew();
        var searchVector = await generator.GenerateEmbeddingVectorAsync(search);
        sw.Stop();
        Console.WriteLine(sw.ElapsedMilliseconds);
        foreach (var item in QueryImageVector(searchVector.ToArray()))
        {
            Console.WriteLine(item.Id + "  " + item.Name + " " + item.Result);
        }
        Console.WriteLine("===============================================");
    }
    IEnumerable<QueryResult> QueryImageVector(float[] imageVector)
    {
        var ds = new List<double>();
        foreach (var item in imageVector)
        {
            ds.Add((double)item);
        }
        using (IDbConnection db = new NpgsqlConnection(File.ReadAllText("C://GPT/just-agi-db.txt")))
        {
            string sqlQuery = "select id,name,1-(cast(@embedding as vector) <=> embedding) as result from public.imagevector order by 1-(cast(@embedding as vector) <=> embedding) desc ";
            return db.Query<QueryResult>(sqlQuery, new { embedding = ds });

        }
    }
    void InsertImageVector(Job imageVector)
    {
        using (IDbConnection db = new NpgsqlConnection(File.ReadAllText("C://GPT/just-agi-db.txt")))
        {
            string sqlQuery = @"
                INSERT INTO public.imagevector (name, embedding) 
                VALUES (@Name, @Embedding) 
                RETURNING id;";
            var ds = new List<double>();
            foreach (var item in imageVector.DescriptionEmbedding.Value.ToArray())
            {
                ds.Add((double)item);
            }
            var parameters = new
            {
                Name = imageVector.JobTitle,
                Embedding = ds.AsReadOnly<double>()
            };

            var id = db.ExecuteScalar<int>(sqlQuery, parameters); // ExecuteScalar returns the inserted id

        }
    }
}
class QueryResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Result { get; set; }
}
public class Job
{
    [VectorStoreRecordKey]
    public string JobId { get; set; }

    [VectorStoreRecordData(IsFilterable = true)]
    public string JobTitle { get; set; }

    [VectorStoreRecordData(IsFullTextSearchable = true)]
    public string Description { get; set; }

    [VectorStoreRecordVector(1024, DistanceFunction.CosineSimilarity)]
    public ReadOnlyMemory<float>? DescriptionEmbedding { get; set; }

    [VectorStoreRecordData(IsFilterable = true)]
    public string Tags { get; set; }
}

