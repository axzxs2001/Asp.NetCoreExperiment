#pragma warning disable 


using Dapper;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.SemanticKernel.Connectors.InMemory;
using Microsoft.SemanticKernel.Connectors.Redis;
using Npgsql;
using OllamaSharp;
using StackExchange.Redis;
using System.Data;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vector;



var jobDescriptions = new List<Job>
{
    new Job
    {
        JobId = "1",
        JobTitle = "软件开发工程师（Software Developer）",
        Description = @"**职责：**
- 根据产品需求设计、开发和优化高质量的软件功能模块。
- 编写高效、稳定、可扩展的代码，并负责代码评审和优化。
- 配合测试团队，进行单元测试和功能测试，确保产品的质量和性能。
- 维护和升级现有产品代码库，修复问题并实施新功能。
- 与产品经理、设计师和其他开发团队成员协作，推动产品开发流程。

**要求：**
- 计算机科学、软件工程或相关专业本科及以上学历。
- 熟练掌握一种或多种编程语言（如Java、Python、C++、JavaScript等）。
- 熟悉常用开发框架和工具（如Spring Boot、Django、React、Angular等）。
- 掌握数据库技术，具备SQL优化能力，有NoSQL数据库经验加分。
- 具备良好的问题分析和解决能力，注重细节，热爱技术。
- 具有良好的团队合作能力、沟通技巧和责任心。"
    },
    new Job
    {
        JobId = "2",
        JobTitle = "产品经理（Product Manager）",
        Description = @"**职责：**
- 深入理解用户需求和行业趋势，制定产品发展方向和战略。
- 负责产品需求分析、功能设计及PRD文档撰写。
- 协调开发、设计、运营等团队，推动产品开发进度。
- 跟踪产品上线后的用户反馈和数据分析，持续优化产品体验。
- 定期与客户和市场团队沟通，确保产品符合市场需求。

**要求：**
- 本科及以上学历，计算机、管理学、市场营销等相关专业优先。
- 至少3年产品经理工作经验，熟悉软件产品开发生命周期。
- 具备良好的数据分析能力，能够根据数据驱动决策。
- 有卓越的沟通能力和跨团队协作经验。
- 对用户体验和交互设计有敏锐洞察力，有成功产品案例者优先。"
    },
    new Job
    {
        JobId = "3",
        JobTitle = "测试工程师（Software Tester/QA Engineer）",
        Description = @"**职责：**
- 设计测试用例，执行功能测试、性能测试及自动化测试。
- 与开发团队紧密合作，确保测试覆盖率和产品质量。
- 撰写测试报告，分析测试结果并跟踪缺陷修复。
- 推动测试流程优化，制定测试规范和标准。
- 对产品线的稳定性和用户体验负责。

**要求：**
- 计算机相关专业本科及以上学历。
- 熟悉软件测试理论及方法，了解测试工具（如Selenium、JMeter等）。
- 具备自动化测试经验者优先。
- 了解主流开发语言和数据库，具备一定的代码阅读能力。
- 逻辑思维清晰，具有高度责任心和耐心。"
    },
    new Job
    {
        JobId = "4",
        JobTitle = "用户体验设计师（UX Designer）",
        Description = @"**职责：**
- 参与用户研究，深入理解用户需求和行为模式。
- 设计用户体验方案，制作线框图、用户流程图和原型。
- 与产品经理和开发团队合作，确保设计方案的可落地性。
- 对产品设计进行评估和迭代优化。
- 提供创意设计和美术支持，提高产品的用户体验。

**要求：**
- 本科及以上学历，设计、美术或人机交互相关专业。
- 熟练使用设计工具（如Figma、Sketch、Adobe XD等）。
- 熟悉用户研究方法，能分析用户行为并转化为设计方案。
- 对视觉设计有良好的审美能力，了解前端开发知识者优先。
- 善于沟通和团队协作，有较强的抗压能力。"
    },
    new Job
    {
        JobId = "5",
        JobTitle = "项目经理（Project Manager）",
        Description = @"**职责：**
- 制定项目计划，确保项目按时、按质、按量交付。
- 管理跨职能团队的工作协调，解决项目中出现的问题。
- 风险识别与管理，制定风险应对措施。
- 跟踪项目进度，编写项目报告并向管理层汇报。
- 提高团队效率，优化项目管理流程。

**要求：**
- 本科及以上学历，计算机、工程管理相关专业。
- 具备3年以上IT项目管理经验，有PMP认证优先。
- 熟悉敏捷开发流程，有使用Scrum、Kanban经验者优先。
- 具备优秀的组织协调能力、沟通能力和领导能力。
- 熟悉软件开发技术和工具，有开发或测试背景更佳。"
    },
    new Job
    {
        JobId = "6",
        JobTitle = "数据分析师（Data Analyst）",
        Description = @"**职责：**
- 收集、清理和分析数据，为产品和运营决策提供支持。
- 制定并追踪核心业务指标，提供定量分析结果。
- 开发数据报表和可视化工具，直观展示分析结果。
- 协助产品经理理解用户行为和需求，优化产品策略。
- 研究新兴数据技术和工具，提升分析效率。

**要求：**
- 本科及以上学历，统计学、数学、计算机等相关专业。
- 熟练使用SQL、Python或R进行数据处理。
- 熟悉数据可视化工具（如Tableau、Power BI）。
- 优秀的逻辑分析能力，对数据敏感，能够发现问题并提出解决方案。
- 具有良好的沟通能力，能够将复杂的分析结果用简单的方式呈现。"
    },
    new Job
    {
        JobId = "7",
        JobTitle = "运维工程师（DevOps Engineer）",
        Description = @"**职责：**
- 负责公司产品的部署、发布和监控，保障系统稳定运行。
- 开发和维护自动化运维工具，提高运维效率。
- 定期检查系统性能，优化系统架构。
- 快速响应并解决生产环境问题。
- 与开发团队协作，推动持续集成和持续交付（CI/CD）流程。

**要求：**
- 本科及以上学历，计算机相关专业。
- 熟悉Linux系统，熟练掌握Shell脚本编写。
- 熟悉常见的运维工具（如Docker、Kubernetes）。
- 熟悉主流云服务（如AWS、Azure、阿里云）。
- 对高并发、大数据处理有相关经验者优先。"
    },
    new Job
    {
        JobId = "8",
        JobTitle = "销售经理（Sales Manager）",
        Description = @"**职责：**
- 制定并执行销售计划，完成公司制定的销售目标。
- 负责开拓新市场，发展新客户，扩大产品市场覆盖率。
- 与客户建立并维持长期的合作关系，深度挖掘客户需求。
- 参与市场调研，收集并分析市场信息，提出销售策略优化建议。
- 协调内部资源，确保销售订单的顺利执行。
- 领导和培训销售团队，提高团队的销售能力和业绩。

**要求：**
- 本科及以上学历，市场营销、工商管理等相关专业优先。
- 3年以上销售经验，有软件或IT产品行业背景者优先。
- 优秀的沟通表达能力和客户服务意识。
- 具备团队管理经验，能够激励团队完成销售目标。
- 结果导向，能够在压力下工作并完成销售指标。
- 熟悉CRM工具（如Salesforce）者优先。"
    },
        new Job
    {
        JobId = "9",
        JobTitle = "客户经理（Key Account Manager）",
        Description = @"**职责：**
- 负责大客户的开发、维护及管理，建立长期合作关系。
- 理解客户的业务需求，为客户提供定制化的软件产品解决方案。
- 协调内部资源，推动项目的实施及交付。
- 负责客户的需求反馈，持续提升客户满意度。
- 对大客户销售目标负责，定期提交销售报告。

**要求：**
- 本科及以上学历，具有大客户销售经验者优先。
- 了解软件产品及相关行业趋势，能够进行技术与业务结合的沟通。
- 优秀的谈判技巧和跨部门协作能力。
- 较强的抗压能力和目标达成能力。
- 能接受出差，有客户资源者优先。"
    },
    new Job
    {
        JobId = "10",
        JobTitle = "商务拓展经理（Business Development Manager）",
        Description = @"**职责：**
- 负责拓展公司在新行业、新区域的合作机会。
- 制定商务拓展计划，与潜在合作伙伴建立联系并达成合作。
- 参与市场战略的制定，推动公司产品在市场中的覆盖率。
- 监控行业动态，研究竞争对手，提出优化建议。
- 配合产品团队，帮助制定符合市场需求的解决方案。

**要求：**
- 本科及以上学历，市场营销、国际贸易等相关专业。
- 具备3年以上商务拓展或市场开发经验，熟悉软件或科技领域。
- 优秀的商业谈判能力和敏锐的市场洞察力。
- 自驱力强，能够主动发现并推动合作机会。
- 具备良好的沟通能力和较强的逻辑思维。"
    },
    new Job
    {
        JobId = "11",
        JobTitle = "渠道销售经理（Channel Sales Manager）",
        Description = @"**职责：**
- 负责公司渠道合作伙伴的开发、管理和维护。
- 制定并执行渠道销售策略，实现销售目标。
- 为合作伙伴提供培训和支持，提升其销售能力。
- 协调公司内部资源，保障渠道伙伴的利益和目标达成。
- 定期分析渠道数据，优化渠道布局。

**要求：**
- 本科及以上学历，市场营销、管理类相关专业优先。
- 具有3年以上渠道销售经验，熟悉代理商、经销商管理模式。
- 较强的沟通能力和关系维护能力。
- 结果导向，能有效推动渠道伙伴完成目标。
- 熟悉软件行业渠道销售模式者优先。"
    },
    new Job
    {
        JobId = "12",
        JobTitle = "客户成功经理（Customer Success Manager）",
        Description = @"**职责：**
- 负责客户在产品使用过程中的全生命周期管理，确保客户成功。
- 跟进客户反馈，帮助客户解决问题，提高产品使用满意度。
- 为客户提供培训和咨询，帮助其更好地使用公司产品。
- 分析客户需求，推动产品优化和升级。
- 负责客户续约及增值销售目标的达成。

**要求：**
- 本科及以上学历，有客户支持或客户成功管理经验者优先。
- 熟悉软件产品或SaaS平台的使用和应用。
- 优秀的客户服务意识和问题解决能力。
- 具备良好的沟通技巧和团队协作能力。
- 有数据分析能力，能够以数据驱动客户决策。"
    },
    new Job
    {
        JobId = "13",
        JobTitle = "市场推广专员（Marketing Specialist）",
        Description = @"**职责：**
- 协助制定市场推广计划，并实施线上线下推广活动。
- 负责公司品牌推广，策划并执行营销活动。
- 配合销售团队，提供市场支持，提升品牌影响力。
- 分析市场数据及用户反馈，不断优化推广策略。
- 管理和维护公司的社交媒体账号及官网内容。

**要求：**
- 本科及以上学历，市场营销、传媒等相关专业。
- 熟悉市场推广方法，了解数字营销工具和策略。
- 有较强的文案能力和创意策划能力。
- 具备良好的沟通和组织能力。
- 有相关工作经验或成功案例者优先。"
    },
    new Job
    {
        JobId = "14",
        JobTitle = "售前解决方案顾问（Pre-Sales Solution Consultant）",
        Description = @"**职责：**
- 配合销售团队，为客户提供技术支持和解决方案。
- 深入理解客户需求，制定针对性的产品解决方案。
- 负责售前技术演示和产品培训。
- 跟踪行业技术趋势，为公司产品研发提供建议。
- 协助销售团队完成技术投标文件和技术协议。

**要求：**
- 本科及以上学历，计算机相关专业优先。
- 具有良好的技术背景，熟悉主流软件架构和技术。
- 优秀的沟通表达能力，能将复杂技术概念简化为客户可理解的语言。
- 具备较强的演讲能力和方案撰写能力。
- 有售前或技术支持经验者优先。"
    },
    new Job
    {
        JobId = "15",
        JobTitle = "电话销售专员（Inside Sales Representative）",
        Description = @"**职责：**
- 通过电话和邮件联系潜在客户，推广公司软件产品。
- 深入了解客户需求，挖掘销售机会并完成销售转化。
- 维护客户数据库，定期跟进潜在客户。
- 协助销售团队，完成线索管理和初步筛选工作。
- 达成个人销售业绩指标。

**要求：**
- 大专及以上学历，有电话销售经验者优先。
- 具备较强的沟通能力和销售技巧。
- 乐观积极，抗压能力强。
- 熟练使用CRM工具，能够高效管理客户信息。
- 对软件行业感兴趣，愿意长期发展。"
    },
    new Job
{
    JobId = "16",
    JobTitle = "后端开发工程师 (Back-End Developer)",
    Description = @"**职责：**
- 设计和开发高性能、可扩展的后端服务，支持前端功能和系统需求。
- 构建和维护API接口，确保系统的稳定性和可用性。
- 参与系统架构设计，选择适合的技术栈和框架。
- 优化数据库结构，编写高效的SQL语句，提升系统性能。
- 定期监控和维护服务，快速响应并解决问题。
- 与前端开发人员和其他团队成员协作，推动项目进度。

**要求：**
- 计算机科学、软件工程或相关专业本科及以上学历。
- 熟悉至少一种后端开发语言（如Java、Python、Go、C#）。
- 熟练掌握后端框架（如Spring Boot、Django、ASP.NET Core）。
- 深入理解数据库系统（SQL和NoSQL），具备优化数据库性能的经验。
- 了解分布式系统设计和微服务架构。
- 具备较强的分析能力、解决问题的能力以及团队协作精神。"
},
    new Job
{
    JobId = "17",
    JobTitle = "全栈开发工程师 (Full-Stack Developer)",
    Description = @"**职责：**
- 独立负责前端和后端功能开发，设计并实现高质量的软件模块。
- 根据产品需求，搭建和维护全栈技术架构。
- 优化前后端接口性能，确保数据的稳定传输和安全性。
- 配合测试团队，进行全流程测试，包括单元测试、集成测试等。
- 持续改进代码质量，参与技术方案讨论及评审。
- 学习和引入新技术，帮助团队保持技术先进性。

**要求：**
- 计算机科学、软件工程或相关专业本科及以上学历。
- 熟练掌握前端技术（如React、Vue.js、HTML、CSS、JavaScript）。
- 熟悉后端开发技术（如Node.js、Java、Python、Go等）。
- 掌握数据库设计和管理（SQL和NoSQL）。
- 了解DevOps和CI/CD工具（如Docker、Kubernetes、Jenkins）。
- 具备较强的跨团队协作能力，热爱技术和创新。"
},
new Job
{
    JobId = "18",
    JobTitle = "前端开发工程师 (Front-End Developer)",
    Description = @"**职责：**
- 根据设计需求开发高质量的用户界面，确保跨浏览器、跨平台的兼容性。
- 优化前端性能，提升用户体验，减少加载时间。
- 编写高效、模块化的代码，利用现代前端框架（如React、Vue.js、Angular）。
- 与后端开发人员协作，确保接口的正常对接及数据传递。
- 参与代码评审，优化现有代码库，推动技术提升。
- 跟踪前端技术的最新动态，并将合适的技术应用于项目中。

**要求：**
- 计算机科学、软件工程或相关专业本科及以上学历。
- 精通HTML、CSS、JavaScript，熟悉ES6及以上版本。
- 熟悉至少一种现代前端框架（如React、Vue.js或Angular）。
- 具备前端构建工具使用经验（如Webpack、Vite、Rollup）。
- 了解RESTful API设计，能够与后端协同工作。
- 具备良好的团队协作能力和问题解决能力，具有敏锐的用户体验意识。"
},
new Job
{
    JobId = "19",
    JobTitle = "销售代表 (Sales Representative)",
    Description = @"**职责：**
- 通过各种渠道挖掘潜在客户，完成销售目标。
- 与客户保持良好的沟通，了解客户需求并提供解决方案。
- 向客户介绍公司产品或服务的特点和优势，促成签单。
- 定期拜访客户，维护客户关系，增加客户满意度。
- 收集市场信息和客户反馈，为产品改进提供建议。
- 配合团队完成销售报告及市场分析。

**要求：**
- 大专及以上学历，市场营销、工商管理或相关专业优先。
- 具备1年以上销售相关经验，有B2B或B2C经验加分。
- 优秀的沟通技巧和人际交往能力，能够独立完成销售任务。
- 结果导向，具有较强的抗压能力和责任心。
- 熟悉CRM系统和办公软件使用优先。"
},
new Job
{
    JobId = "20",
    JobTitle = "大客户经理 (Key Account Manager)",
    Description = @"**职责：**
- 负责大客户的开发、谈判及长期合作关系的维护。
- 深入了解客户业务需求，提供定制化解决方案。
- 协调内部资源，为客户提供高质量的服务和支持。
- 参与合同洽谈及签订，确保项目的顺利执行。
- 定期拜访客户，收集行业动态及竞争信息，提出市场拓展策略。
- 实现公司下达的大客户销售目标及业绩指标。

**要求：**
- 本科及以上学历，市场营销、商务管理或相关专业优先。
- 3年以上大客户销售经验，具有IT、金融、制造行业背景优先。
- 优秀的商务谈判能力及项目管理能力。
- 具备强烈的客户导向意识，能够与客户建立信任关系。
- 熟悉大客户销售流程，具有团队协作和跨部门沟通能力。"
},
new Job
{
    JobId = "21",
    JobTitle = "销售主管 (Sales Manager)",
    Description = @"**职责：**
- 负责销售团队的日常管理和目标制定，带领团队完成销售任务。
- 制定销售计划和策略，推动市场拓展，挖掘新的业务机会。
- 指导并培训销售团队成员，提高销售技巧及专业知识。
- 监督销售活动的执行情况，分析销售数据并进行调整。
- 维护重要客户关系，处理客户投诉及疑难问题。
- 向上级汇报销售业绩和市场动态，提供合理化建议。

**要求：**
- 本科及以上学历，市场营销、企业管理或相关专业优先。
- 5年以上销售经验，2年以上团队管理经验。
- 具备良好的领导能力和沟通技巧，能够激励团队达成目标。
- 熟悉行业市场，能够制定有效的销售策略。
- 具有数据分析能力，能够从数据中发现问题并制定解决方案。
- 高度责任心，结果导向，能承受较大工作压力。"
},
new Job
{
    JobId = "22",
    JobTitle = "电话销售 (Telesales Representative)",
    Description = @"**职责：**
- 通过电话联系潜在客户，推广公司产品或服务。
- 解答客户咨询，了解客户需求，促成订单。
- 持续跟进客户意向，维护良好的客户关系。
- 定期完成电话回访，提升客户满意度。
- 更新并维护客户信息，确保CRM系统的准确性。
- 配合销售团队，完成销售目标和报告工作。

**要求：**
- 高中及以上学历，有电话销售经验者优先。
- 具备良好的沟通技巧和应变能力。
- 性格开朗，具备强烈的目标感和执行力。
- 熟悉电话销售技巧，能够有效处理客户异议。
- 能熟练使用办公软件，有CRM系统经验加分。"
},

};


//var modelPath = @"C:\GPT\ONNX\jina-reranker-v2-base-multilingual\onnx\model.onnx";
//var tokenizerPath = @"C:\GPT\ONNX\jina-reranker-v2-base-multilingual\tokenizer.json";

//var modelPath = @"C:\GPT\ONNX\bge-reranker-v2-m3\model.onnx";
//var tokenizerPath = @"C:\GPT\ONNX\bge-reranker-v2-m3\tokenizer.json";

//var modelPath = @"C:\GPT\ONNX\gte-multilingual-reranker-base\onnx\model.onnx";
//var tokenizerPath = @"C:\GPT\ONNX\gte-multilingual-reranker-base\tokenizer.json";


while (true)
{
    Console.WriteLine("******************Ollam****************");
    await MatchingJobsRerankerAsync();
    Console.WriteLine("*****************Onnx*****************");
    // await MatchingJobsReranker1Async();
}
//await MatchingJobsAsync();
//await PGVector();
//await RedisVector();

void Reranker(string query, List<Job> jobs)
{
    var modelPath = @"C:\GPT\ONNX\gte-multilingual-reranker-base-onnx-op14-opt-gpu\model.onnx";
    var tokenizerPath = @"C:\GPT\ONNX\gte-multilingual-reranker-base-onnx-op14-opt-gpu\tokenizer.json";
    var tokenizer = Tokenizers.HuggingFace.Tokenizer.Tokenizer.FromFile(tokenizerPath);

    using var session = new InferenceSession(modelPath);
    // query = query.Length >= 1024 ? query[..1023] : query;
    var newlist = new List<(string Title, float Score)>();
    foreach (var job in jobs)
    {
        //Console.WriteLine(job.JobTitle);
        var encodingResult = tokenizer.Encode(
            query,//.Replace(" ", ""),
            add_special_tokens: true,
            input2: job.Description,
            include_type_ids: true,
            include_attention_mask: true
        );
        var enc = encodingResult.Encodings[0];
        int seqLen = enc.Ids.Count;
        var inputIdsTensor = new DenseTensor<long>(enc.Ids.Select(i => (long)i).ToArray(), new[] { 1, seqLen });
        var typeIdsTensor = new DenseTensor<long>(enc.TypeIds.Select(i => (long)i).ToArray(), new[] { 1, seqLen });
        var attentionMaskTensor = new DenseTensor<long>(enc.AttentionMask.Select(i => (long)i).ToArray(), new[] { 1, seqLen });
        var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("input_ids",     inputIdsTensor),
                NamedOnnxValue.CreateFromTensor("attention_mask",attentionMaskTensor)
            };
        if (session.InputMetadata.ContainsKey("token_type_ids"))
        {
            inputs.Add(NamedOnnxValue.CreateFromTensor("token_type_ids", typeIdsTensor));
        }
        using var results = session.Run(inputs);
        var logitsArr = results.First(x => x.Name == "logits")
                                .AsTensor<float>()
                                .ToArray();
        float posLogit = logitsArr.Length == 1
            ? logitsArr[0]
            : logitsArr[1];

        float prob = 1f / (1f + MathF.Exp(-posLogit));

        newlist.Add((Title: job.JobTitle, Socre: prob));

    }

    foreach (var item in newlist.OrderByDescending(s => s.Score))
    {
        Console.WriteLine($"{item.Title}:{item.Score}");
    }

}




async Task MatchingJobsRerankerAsync()
{
    var client = new OllamaApiClient(new Uri("http://localhost:11434/"), "snowflake-arctic-embed2");
    //while (true)
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("请选择简历：");
            Console.WriteLine("1、cv1.md   2、cv2.md    3、cv3.md");
            var cvPath = "";
            var cvNo = Console.ReadLine();
            switch (cvNo)
            {
                case "1":
                    cvPath = "cv1.md";
                    break;
                case "2":
                    cvPath = "cv2.md";
                    break;
                case "3":
                    cvPath = "cv3.md";
                    break;
                default:
                    Console.WriteLine("请输入简历路径：");
                    cvPath = Console.ReadLine();
                    break;
            }
            var arr = File.ReadLines(cvPath).ToArray()[5..];
            var search = string.Join("", arr);
            Console.ResetColor();
            var sw = Stopwatch.StartNew();
            Console.WriteLine($"=======================简历 {search.Length}个字========================");
            Console.WriteLine(search);
            var searchVector = await client.GenerateVectorAsync(search);

            Console.WriteLine("=======================Vector 搜索结果排序========================");
            var first = 0d;
            var vectorResult = QueryVector(searchVector.ToArray()).Take(15);
            var list = new List<Job>();
            foreach (var item in vectorResult)
            {
                if (first == 0)
                {
                    first = double.Parse(item.Result);
                }
                Console.WriteLine("职位：" + item.Name + "   匹配得分值：" + Math.Round((double.Parse(item.Result) / first) * 100).ToString("0") + "%");
                list.Add(jobDescriptions.SingleOrDefault(s => s.JobTitle == item.Name));

                //Console.WriteLine($"职位: {item.Name},相似度: {item.Result}");
                //list.Add(jobDescriptions.SingleOrDefault(s => s.JobTitle == item.Name));
            }
            sw.Stop();
            Console.WriteLine($"========================Vector 搜索用时：{sw.ElapsedMilliseconds}毫秒==============================");

            sw = Stopwatch.StartNew();
            Console.WriteLine("=======================Reranker 搜索结果排序========================");
            Reranker(search, list);
            sw.Stop();
            Console.WriteLine($"==========================Reranker 搜索用时：{sw.ElapsedMilliseconds}毫秒============================");
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.Message);
        }
    }

}
IEnumerable<QueryResult> QueryVector(float[] imageVector)
{
    var ds = new List<double>();
    foreach (var item in imageVector)
    {
        ds.Add((double)item);
    }
    using (IDbConnection db = new NpgsqlConnection(File.ReadAllText("C://GPT/just-agi-db.txt")))
    {
        string sqlQuery = $@"select id,name,1-(cast(@embedding as vector) <=> embedding) as result from public.imagevector 
-- where createtime>'2024-12-06'
order by 1-(cast(@embedding as vector) <=> embedding) desc ";
        return db.Query<QueryResult>(sqlQuery, new { embedding = ds });
    }
}
void InsertImageVector(Job imageVector)
{
    using (IDbConnection db = new NpgsqlConnection(File.ReadAllText("C://GPT/just-agi-db.txt")))
    {
        string sqlQuery = @"
                INSERT INTO public.imagevector (name, embedding,createtime) 
                VALUES (@Name, @Embedding,@CreateTime) 
                RETURNING id;";
        var ds = new List<double>();
        foreach (var item in imageVector.DescriptionEmbedding.Value.ToArray())
        {
            ds.Add((double)item);
        }
        var parameters = new
        {
            Name = imageVector.JobTitle,
            Embedding = ds.AsReadOnly<double>(),
            CreateTime = DateTime.Now
        };

        var id = db.ExecuteScalar<int>(sqlQuery, parameters); // ExecuteScalar returns the inserted id

    }
}
async Task MatchingJobsReranker1Async()
{
    var modelPath = @"C:\GPT\ONNX\jina-embeddings-v3\onnx\model.onnx";
    var tokenizerPath = @"C:\GPT\ONNX\jina-embeddings-v3\tokenizer.json";
    var vectorGenerator = new TextVectorGenerator(modelPath, tokenizerPath);

    //while (true)
    {
        Console.WriteLine("请选择简历：");
        Console.WriteLine("1、cv1.md   2、cv2.md    3、cv3.md");
        var cvPath = "";
        var cvNo = Console.ReadLine();
        switch (cvNo)
        {
            case "1":
                cvPath = "cv1.md";
                break;
            case "2":
                cvPath = "cv2.md";
                break;
            case "3":
                cvPath = "cv3.md";
                break;
            default:
                return;
        }
        var arr = File.ReadLines(cvPath).ToArray()[5..];
        var search = string.Join("", arr);
        Console.WriteLine($"简历: {search}");

        var sw = Stopwatch.StartNew();
        var searchVector = vectorGenerator.GenerateVector(search);
        Console.WriteLine("=======================Vector 搜索结果排序========================");
        var list = new List<Job>();
        foreach (var item in QueryVector(searchVector))
        {
            //Console.WriteLine("--------------------------------");
            Console.WriteLine($"职位: {item.Name},相似度: {item.Result}");
            list.Add(jobDescriptions.SingleOrDefault(s => s.JobTitle == item.Name));
        }
        sw.Stop();
        Console.WriteLine($"========================Vector 搜索用时：{sw.ElapsedMilliseconds}毫秒==============================");

        //sw = Stopwatch.StartNew();
        //Console.WriteLine("=======================Reranker 搜索结果排序========================");
        //Reranker(search, list);
        //sw.Stop();
        //Console.WriteLine($"==========================Reranker 搜索用时：{sw.ElapsedMilliseconds}毫秒============================");
    }
    vectorGenerator.Dispose();
}



async Task MatchingJobsAsync()
{
    var client = new OllamaApiClient(new Uri("http://localhost:11434/"), "snowflake-arctic-embed2");

    //IEmbeddingGenerator<string, Embedding<float>>
    // var generator = new OllamaEmbeddingGenerator(new Uri("http://localhost:11434/"), "snowflake-arctic-embed2");
    while (true)
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("请输简历路径：");
            var arr = File.ReadLines(Console.ReadLine()).ToArray()[5..];
            var search = string.Join('\n', arr);
            Console.ResetColor();
            var sw = Stopwatch.StartNew();
            var searchVector = await client.GenerateVectorAsync(search);
            sw.Stop();
            Console.WriteLine($"搜索用时：{sw.ElapsedMilliseconds}毫秒");
            Console.WriteLine("=======================搜索结果排序========================");
            var first = 0d;
            foreach (var item in QueryImageVector(searchVector.ToArray()).Take(3))
            {
                if (first == 0)
                {
                    first = double.Parse(item.Result);
                }
                Console.WriteLine("职位：" + item.Name + "   匹配得分值：" + Math.Round((double.Parse(item.Result) / first) * 100).ToString("0") + "%");
            }
            Console.WriteLine("======================================================");
            Console.WriteLine("\n\n");
            Thread.Sleep(2000);
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.Message);
        }
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
            string sqlQuery = $@"select id,name,1-(cast(@embedding as vector) <=> embedding) as result from public.imagevector 
-- where createtime>'2024-12-06'
order by 1-(cast(@embedding as vector) <=> embedding) desc ";
            return db.Query<QueryResult>(sqlQuery, new { embedding = ds });
        }
    }
    void InsertImageVector(Job imageVector)
    {
        using (IDbConnection db = new NpgsqlConnection(File.ReadAllText("C://GPT/just-agi-db.txt")))
        {
            string sqlQuery = @"
                INSERT INTO public.imagevector (name, embedding,createtime) 
                VALUES (@Name, @Embedding,@CreateTime) 
                RETURNING id;";
            var ds = new List<double>();
            foreach (var item in imageVector.DescriptionEmbedding.Value.ToArray())
            {
                ds.Add((double)item);
            }
            var parameters = new
            {
                Name = imageVector.JobTitle,
                Embedding = ds.AsReadOnly<double>(),
                CreateTime = DateTime.Now
            };

            var id = db.ExecuteScalar<int>(sqlQuery, parameters); // ExecuteScalar returns the inserted id

        }
    }
}

#region 
async Task PGVector()
{
    var client = new OllamaApiClient(new Uri("http://localhost:11434/"), "snowflake-arctic-embed2");
    //var generator = new OllamaEmbeddingGenerator(new Uri("http://localhost:11434/"), "snowflake-arctic-embed2");
    #region
    //foreach (var hotel in jobDescriptions)
    //{
    //    hotel.DescriptionEmbedding = await generator.GenerateEmbeddingVectorAsync(hotel.Description);
    //    InsertImageVector(hotel);
    //};
    #endregion
    while (true)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        var search = @"编程语言: Python, Java, C++, JavaScript/TypeScript, Go
前端: HTML, CSS, React, Vue.js
后端: Node.js, Django, Spring Boot, Flask
数据库和云服务: MySQL, PostgreSQL, MongoDB, AWS, Azure
工具和基础设施: Git, Docker, Kubernetes, Jenkins, Linux
大数据和人工智能: Pandas, NumPy, TensorFlow, PyTorch, Spark
其他: RESTful API, GraphQL, WebSocket, Agile/Scrum
能打电话把产品卖出去
管理公司的整体销售状况";
        var arr = search.Split("\n").ToList();
        var sn = 1;
        foreach (var item in arr)
        {
            Console.WriteLine($"{sn++}、{item}");
        }
        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine("请输入序号，自定义输入搜索内容，请按回车：");
        var no = Console.ReadLine();
        Console.WriteLine("------------------------------------------------------------");
        switch (no)
        {
            case "1":
                search = "Python, Java, C++, JavaScript/TypeScript, Go";
                break;
            case "2":
                search = "HTML, CSS, React, Vue.js";
                break;
            case "3":
                search = "Node.js, Django, Spring Boot, Flask";
                break;
            case "4":
                search = "MySQL, PostgreSQL, MongoDB, AWS, Azure";
                break;
            case "5":
                search = "Git, Docker, Kubernetes, Jenkins, Linux";
                break;
            case "6":
                search = "Pandas, NumPy, TensorFlow, PyTorch, Spark";
                break;
            case "7":
                search = "RESTful API, GraphQL, WebSocket, Agile/Scrum";
                break;
            case "8":
                search = "能打电话把产品卖出去";
                break;
            case "9":
                search = "管理公司的整体销售状况";
                break;
            default:
                Console.WriteLine("请输入搜索内容：");
                search = Console.ReadLine();
                break;
        }
        Console.ResetColor();
        var sw = Stopwatch.StartNew();
        var searchVector = await client.GenerateVectorAsync(search);
        sw.Stop();
        Console.WriteLine($"搜索用时：{sw.ElapsedMilliseconds}毫秒");
        Console.WriteLine("=======================搜索结果排序========================");
        var first = 0d;
        foreach (var item in QueryImageVector(searchVector.ToArray()))
        {
            if (first == 0)
            {
                first = double.Parse(item.Result);
            }
            Console.WriteLine("职位：" + item.Name + "   匹配得分值：" + Math.Round((double.Parse(item.Result) / first) * 100).ToString("0") + "%");
        }
        Console.WriteLine("======================================================");
        Console.WriteLine("\n\n");
        Thread.Sleep(2000);
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
            string sqlQuery = $@"select id,name,1-(cast(@embedding as vector) <=> embedding) as result from public.imagevector 
-- where createtime>'2024-12-06'
order by 1-(cast(@embedding as vector) <=> embedding) desc ";
            return db.Query<QueryResult>(sqlQuery, new { embedding = ds });
        }
    }
    void InsertImageVector(Job imageVector)
    {
        using (IDbConnection db = new NpgsqlConnection(File.ReadAllText("C://GPT/just-agi-db.txt")))
        {
            string sqlQuery = @"
                INSERT INTO public.imagevector (name, embedding,createtime) 
                VALUES (@Name, @Embedding,@CreateTime) 
                RETURNING id;";
            var ds = new List<double>();
            foreach (var item in imageVector.DescriptionEmbedding.Value.ToArray())
            {
                ds.Add((double)item);
            }
            var parameters = new
            {
                Name = imageVector.JobTitle,
                Embedding = ds.AsReadOnly<double>(),
                CreateTime = DateTime.Now
            };

            var id = db.ExecuteScalar<int>(sqlQuery, parameters); // ExecuteScalar returns the inserted id

        }
    }
}
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
    var jobStore = vectorStore.GetCollection<string, Job>("vector");
    var client = new OllamaApiClient(new Uri("http://localhost:11434/"), "snowflake-arctic-embed2");
    // var generator = new OllamaEmbeddingGenerator(new Uri("http://localhost:11434/"), "mxbai-embed-large");
    foreach (var job in jobDescriptions)
    {
        job.DescriptionEmbedding = await client.GenerateVectorAsync(job.JobTitle + "。"
             + job.Tags + "。"
            + job.Description);

        await jobStore.UpsertAsync(job);
    }
    ;
    Console.WriteLine("开始搜索");
    var vectorSearchOptions = new VectorSearchOptions<Job>
    {

    };

    while (true)
    {
        Console.WriteLine("请输入搜索内容");
        Console.ForegroundColor = ConsoleColor.Yellow;
        var search = Console.ReadLine();
        Console.ResetColor();
        var sw = Stopwatch.StartNew();
        var searchVector = await client.GenerateVectorAsync(search);
        sw.Stop();
        Console.WriteLine(sw.ElapsedMilliseconds);
        //var results = await jobStore.SearchAsync(searchVector,3);

        //await foreach (var result in results.Results)
        //{
        //    Console.WriteLine($"Title: {result.Record.JobTitle}");
        //    Console.WriteLine($"Description: {result.Record.Description}");
        //    Console.WriteLine($"Score: {result.Score}");
        //    Console.WriteLine();
        //}
    }
}
#endregion

class QueryResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Result { get; set; }
}
public class Job
{
    [VectorStoreKey]
    public string JobId { get; set; }

    [VectorStoreData(IsIndexed = true)]
    public string JobTitle { get; set; }

    [VectorStoreData(IsFullTextIndexed = true)]
    public string Description { get; set; }

    [VectorStoreVector(1024, DistanceFunction = DistanceFunction.CosineSimilarity)]
    public ReadOnlyMemory<float>? DescriptionEmbedding { get; set; }

    [VectorStoreData(IsIndexed = true)]
    public string Tags { get; set; }
}

