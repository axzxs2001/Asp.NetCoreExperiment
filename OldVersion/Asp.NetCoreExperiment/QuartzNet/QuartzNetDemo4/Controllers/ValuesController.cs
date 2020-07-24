using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuartzNetDemo4.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QuartzNetDemo4.Model.DataModel;
using Quartz;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace QuartzNetDemo4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IBackgroundRepository _backgroundRepository;
        IScheduler _scheduler;
        IOptionsSnapshot<List<CronMethod>> _cronMethod;
        IFileProvider _fileProvider;
        public ValuesController(IBackgroundRepository backgroundRepository, IScheduler scheduler, IOptionsSnapshot<List<CronMethod>> cronMethod, IFileProvider fileProvider)
        {
            _scheduler = scheduler;
            _cronMethod = cronMethod;
            _backgroundRepository = backgroundRepository;
            _fileProvider = fileProvider;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            return new string[] { "value1", "value1" };
        }
        [HttpGet("{s}")]
        public ActionResult Get(int s)
        {
            //获取路径方法
            var content=_fileProvider.GetFileInfo("appsettings.json");
            var path0=content.PhysicalPath;
            var path1 = AppDomain.CurrentDomain.BaseDirectory + "appsettings.json"; 

            var path =Directory.GetCurrentDirectory() + "/appsettings.json";
            var json = System.IO.File.ReadAllText(path);

            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
            obj.CronJob[0].CronExpression = $"0/{s} * * * * ?";
            _scheduler.Clear().Wait();
            foreach (var cronJob in obj.CronJob)
            {              
                Console.WriteLine($"{cronJob.MethodName},{cronJob.CronExpression}");
                QuartzServicesUtilities.StartJob<BackgroundJob>(_scheduler, cronJob.CronExpression.ToString(), cronJob.MethodName.ToString());
            }
            json = Newtonsoft.Json.JsonConvert.SerializeObject(obj,Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(path, json);
            return Ok();
        }
        // GET api/values/5
        //[HttpGet("{date}")]
        //public ActionResult<string> Get(string date)
        //{
        //    _backgroundRepository.StarPayDailyReport();
        //    return "value1";
        //}

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
