using ClientDemo01;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace DBControl
{

    public static class ControlExpand
    {
        static HttpClient _httpClient = new HttpClient();
        public static async Task DBControlInit(this ListControl control, string url, string dataSourceName, List<DBCondition>? conditions)
        {
            if (!control.IsAncestorSiteInDesignMode)
            {
                if (!string.IsNullOrWhiteSpace(url) && !string.IsNullOrWhiteSpace(dataSourceName) && !string.IsNullOrWhiteSpace(control.DisplayMember) && !string.IsNullOrWhiteSpace(control.ValueMember))
                {
                    url = $"{url.TrimEnd('/', '\\')}/{dataSourceName}?fields={Uri.EscapeDataString(control.ValueMember)},{Uri.EscapeDataString(control.DisplayMember)}";
                    if (conditions != null && conditions.Count > 0)
                    {
                        var arr = conditions.Select(s => $"({s.Name},{s.Symbol},{s.Value})").ToArray();
                        url += "&conditions=" + Uri.EscapeDataString(string.Join(',', arr));
                    }
                    var content = await _httpClient.GetStringAsync(url);
                    var table = JsonToDataTable(content);
                    control.DataSource = table;
                }
            }
        }
        static DataTable JsonToDataTable(string json)
        {
            var table = new DataTable();
            var list = JsonSerializer.Deserialize<IList<Dictionary<string, dynamic>>>(json);
            var columns = list?.First().Select(d => d.Key);
            if (list != null && columns != null)
            {
                foreach (var item in columns)
                {
                    table.Columns.Add(item);
                }
                foreach (var item in list)
                {
                    table.Rows.Add(item.Values.ToArray());
                }
            }
            return table;
        }


 


        public static async Task DBGridInit(this DataGridView control, string url, string dataSourceName, List<DBCondition>? conditions)
        {
            if (!control.IsAncestorSiteInDesignMode)
            {
                if (!string.IsNullOrWhiteSpace(url) && !string.IsNullOrWhiteSpace(dataSourceName) && control.Columns.Count > 0)
                {
                    var fieldList = new List<string>();
                    foreach (DataGridViewColumn column in control.Columns)
                    {
                        fieldList.Add(column.DataPropertyName);
                    }
                    url = $"{url.TrimEnd('/', '\\')}/{dataSourceName}?fields={Uri.EscapeDataString(string.Join(',', fieldList))}";
                    if (conditions != null && conditions.Count > 0)
                    {
                        var arr = conditions.Select(s => $"({s.Name},{s.Symbol},{s.Value})").ToArray();
                        url += "&conditions=" + Uri.EscapeDataString(string.Join(',', arr));
                    }
                    
                    var content = await _httpClient.GetStringAsync(url);
                    var table = JsonToDataTable(content);
                    control.DataSource = table;
                }
            }
        }
    }

    public class DBCondition
    {
        [Browsable(true)]
        [Description("查询条件名称"), Category("数据"), DefaultValue("")]
        [DisplayName]
        public string? Name { get; set; }
        [Browsable(true)]
        [Description("条件符号"), Category("数据"), DefaultValue("")]
        public string? Symbol { get; set; }
        [Browsable(true)]
        [Description("查询条件值"), Category("数据"), DefaultValue("")]
        public string? Value { get; set; }
    }

}

/*
 在.NET体系里，一直有一批很少发声的开发者，他们在默默的为行业贡献，那就是.NET CS开发者，即奋斗在WinForm，WPF框架下的开发者。为什么很少发声？以至于大家都把这部分开发者忘记了呢？
1、技术成熟，没有更多的新内容
2、行业软件，更多的解决业务的复杂性，技术问题相对较少
3、CS的三方库很多是商业产品：Dev控件，各种报表，有厂家提供更商质量的咨询服务
如果你用.NET做传统行业的，也可以私信一下你的感受。

 
 
 */