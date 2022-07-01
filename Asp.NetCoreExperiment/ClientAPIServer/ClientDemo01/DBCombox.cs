using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientDemo01
{
    public class DBCombox : ComboBox
    {

        private readonly HttpClient _httpclient;
        /// <summary>
        /// 后端Url
        /// </summary>
        public string? Url { get; set; }

        public string? TableName { get; set; }
        public DBCombox()
        {
            _httpclient = new HttpClient();
        }


        protected async override void CreateHandle()
        {
            base.CreateHandle();
            await DBControlInit();
        }

        private async Task DBControlInit()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Url) && !string.IsNullOrWhiteSpace(TableName) && !string.IsNullOrWhiteSpace(DisplayMember) && !string.IsNullOrWhiteSpace(ValueMember))
                {
                    var content = await _httpclient.GetStringAsync($"{Url}/{TableName}/{ValueMember}/{DisplayMember}");
                    var table = JsonToDataTable(content);
                    DataSource = table;
                }
            }
            catch { }
        }
        private DataTable JsonToDataTable(string json)
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


    public class DBListBox : ListBox
    {
        private readonly HttpClient _httpclient;
        /// <summary>
        /// 后端Url
        /// </summary>
        public string? Url { get; set; }

        public string? TableName { get; set; }
        public DBListBox()
        {
            _httpclient = new HttpClient();
        }
        protected async override void CreateHandle()
        {
            base.CreateHandle();
            await DBControlInit();
        }

        private async Task DBControlInit()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Url) && !string.IsNullOrWhiteSpace(TableName) && !string.IsNullOrWhiteSpace(DisplayMember) && !string.IsNullOrWhiteSpace(ValueMember))
                {
                    var content = await _httpclient.GetStringAsync($"{Url}/{TableName}/{ValueMember}/{DisplayMember}");
                    var table = JsonToDataTable(content);
                    DataSource = table;
                }
            }
            catch { }
        }
        private DataTable JsonToDataTable(string json)
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

