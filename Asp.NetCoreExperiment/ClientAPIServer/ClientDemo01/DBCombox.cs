using System;
using System.Collections.Generic;
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
        public string? Url { get; set; }
        /// <summary>
        /// 数据源名称
        /// </summary>
        public string? DataSourceName { get; set; }

        protected async override void CreateHandle()
        {
            base.CreateHandle();
            await this.DBControlInit(Url, DataSourceName);
        }
    }



    public class DBListBox : ListBox
    {
        /// <summary>
        /// 后端Url
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// 数据源名称
        /// </summary>
        public string? DataSourceName { get; set; }

        protected async override void CreateHandle()
        {
            base.CreateHandle();
            await this.DBControlInit(Url, DataSourceName);
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