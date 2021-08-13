本案例是通过Fluentd收集文件日志，然后推送到Exceptionless中

1、Fluentd配置

<source>
  @type tail
  path C:/MyFile/testlog/*.log
  tag gsw.gsw
  <parse>
    @type none  
  </parse>
</source>

<match gsw.gsw>
  @type http
  endpoint http://localhost:6000/addlog
  open_timeout 2
  <format>
    @type json
  </format>
  json_array true
  <buffer>
    flush_interval 5s
  </buffer>
</match>

2、安装Exceptionless到docker中
docker run --rm -it -p 5000:80 exceptionless/exceptionless:latest 

3、配置本项目apppsetings.json
  增加节点
  "Exceptionless": {
    "ApiKey": "eM4ocjAJmOKOHSDf1ekIHCUoIueuY7eAOns0YZwD",
    "ServerUrl": "http://localhost:5000"
  },
  安装主Nuget Exceptionless.aspnetcore
  StarUp引入
  services.AddExceptionless(Configuration);

  下面方法是收集Fluentd的方法

   [HttpPost("/addlog")]
        public IActionResult AddFluendLog()
        {

            var b = new byte[(int)Request.ContentLength.Value];
            var r = Request.Body.ReadAsync(b, 0, b.Length).Result;
            var s = System.Text.Encoding.UTF8.GetString(b);
            var logbodys = System.Text.Json.JsonSerializer.Deserialize<LogBody[]>(s, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (DateTime.Now.Second % 3 == 0)
            {
                new Exception($"异常：{s }").ToExceptionless().Submit();
            }
            else
            {
                foreach (var logbody in logbodys)
                {
                    _logger.LogInformation(logbody.Message);
                    _client.SubmitLog("FluentdWebDemo01", logbody.Message);

                }
            }

            return Ok();

        }