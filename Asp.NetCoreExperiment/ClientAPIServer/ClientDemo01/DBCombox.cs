using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DBExpand;

namespace ClientDemo01
{
    public class DBCombox : ComboBox
    {

        /// <summary>
        /// 后端Url
        /// </summary> 
        [Browsable(true)]
        [Description("后端Url"), Category("远程数据"), DefaultValue("")]
        public string? Url
        {
            get;
            set;
        }

        /// <summary>
        /// 数据源名称
        /// </summary>
        [Browsable(true)]
        [Description("访问Url后端数据源名称"), Category("远程数据"), DefaultValue("")]
        public string? DataSourceName { get; set; }

        protected async override void CreateHandle()
        {
            base.CreateHandle();
            if (!string.IsNullOrWhiteSpace(Url) && !string.IsNullOrWhiteSpace(DataSourceName))
            {
                await this.DBControlInit(Url, DataSourceName);
            }
        }
    }



    public class DBListBox : ListBox
    {
        /// <summary>
        /// 后端Url
        /// </summary>
        [Browsable(true)]
        [Description("后端Url"), Category("远程数据"), DefaultValue("")]
        public string? Url { get; set; }
        /// <summary>
        /// 数据源名称
        /// </summary>
        [Browsable(true)]
        [Description("访问Url后端数据源名称"), Category("远程数据"), DefaultValue("")]
        public string? DataSourceName { get; set; }

        protected async override void CreateHandle()
        {
            base.CreateHandle();
            if (!string.IsNullOrWhiteSpace(Url) && !string.IsNullOrWhiteSpace(DataSourceName))
            {
                await this.DBControlInit(Url, DataSourceName);
            }
        }
    }
}
namespace DBExpand
{
    public static class ControlExpand
    {
        static HttpClient _httpClient = new HttpClient();
        public static async Task DBControlInit(this ListControl control, string url, string dataSourceName)
        {
            if (!control.IsAncestorSiteInDesignMode)
            {
                if (!string.IsNullOrWhiteSpace(url) && !string.IsNullOrWhiteSpace(dataSourceName) && !string.IsNullOrWhiteSpace(control.DisplayMember) && !string.IsNullOrWhiteSpace(control.ValueMember))
                {
                    var content = await _httpClient.GetStringAsync($"{url}/{dataSourceName}/{control.ValueMember}/{control.DisplayMember}");
                    var table = JsonToDataTable(content);
                    control.DataSource = table;
                }
            }
        }
        static DataTable JsonToDataTable(string json)
        {
            var table = new DataTable();
            var list = JsonSerializer.Deserialize<IList<Dictionary<String, Object>>>(json);
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
    }

}

/*
 在.NET体系里，一直有一批很少发声的开发者，他们在默默的为行业贡献，那就是.NET CS开发者，即奋斗在WinForm，WPF框架下的开发者。为什么很少发声？以至于大家都把这部分开发者忘记了呢？
1、技术成熟，没有新内容
2、行业间通用性的技术较少
3、社区少
4、CS上一般都是商业产品：控件Dev，报表
 
 
 */