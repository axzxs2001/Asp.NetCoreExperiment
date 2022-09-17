using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSWControls
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(DBDataGridView), "ggrid.png")]
    public class DBDataGridView : DataGridView
    {        
        [Browsable(true)]
        [Description("后端Url"), Category("远程数据"), DefaultValue("")]
        public string? Url { get; set; }    
        
        [Browsable(true)]
        [Description("访问Url后端数据源名称"), Category("远程数据"), DefaultValue("")]
        public string? DataSourceName { get; set; }

        [Browsable(true)]
        [Description("查询数据源条件参数"), Category("远程数据"), DefaultValue("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<DBCondition>? Conditions { get; set; } = new List<DBCondition>();

        protected async override void CreateHandle()
        {
            base.CreateHandle();
            if (!string.IsNullOrWhiteSpace(Url) && !string.IsNullOrWhiteSpace(DataSourceName) && Conditions != null)
            {
                await this.DBGridInit(Url, DataSourceName, Conditions);
            }
        }
    }
}
