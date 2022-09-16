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

namespace GSWControls
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

