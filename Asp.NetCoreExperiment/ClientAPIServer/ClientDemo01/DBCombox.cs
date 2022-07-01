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
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
        }
        protected override async void OnCreateControl()
        {
            base.OnCreateControl();
            if (!string.IsNullOrWhiteSpace(Url) && !string.IsNullOrWhiteSpace(TableName) && !string.IsNullOrWhiteSpace(DisplayMember) && !string.IsNullOrWhiteSpace(ValueMember))
            {
                var content = await _httpclient.GetStringAsync($"{Url}/{TableName}/{ValueMember}/{DisplayMember}");


                var list = JsonSerializer.Deserialize<dynamic>(content);
                var table = new DataTable();
                table.Columns.Add(ValueMember, typeof(string));
                table.Columns.Add(DisplayMember, typeof(string));
                foreach (var item in list)
                {
                    table.Rows.Add(
                        item.GetProperty(ValueMember).GetRawText(),
                        item.GetProperty(DisplayMember).GetRawText().Trim('"')
                        );
                    //var value = ((JsonElement)item).GetProperty(ValueMember).GetRawText();
                    //var display = ((JsonElement)item).GetProperty(DisplayMember).GetRawText().Trim('"');
                    //newlist.Add(new { Value = value, Display = display });
                }

                DataSource = table;

            }
        }

    }
}
