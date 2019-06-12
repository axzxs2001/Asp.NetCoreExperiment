using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFGlobalizationLocalization
{
    public static class FormLocalizationExtension
    {
        /// <summary>
        /// 设置当前程序的界面语言
        /// </summary>
        /// <param name="form">窗体实例</param>   
        /// <param name="language">language:zh-CN, ja-JP</param>
        public static void Localize(this Form form, string language)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            var resources = new ComponentResourceManager(form.GetType());
            resources.ApplyResources(form, "$this");
            ApplyControl(form, resources);
            ApplyToolControl(form, resources);
        }

        #region MenuTool

        /// <summary>
        /// 遍历窗体所有控件
        /// </summary>
        /// <param name="control"></param>
        /// <param name="resources"></param>
        private static void ApplyToolControl(Control control, ComponentResourceManager resources)
        {
            if (control is ToolStrip)
            {
                resources.ApplyResources(control, control.Name);
                foreach (ToolStripItem toolStripItem in ((ToolStrip)control).Items)
                {
                    ApplyToolItme(toolStripItem, resources);
                }
            }
            else
            {
                foreach (Control control1 in control.Controls)
                {
                    resources.ApplyResources(control1, control1.Name);
                    ApplyToolControl(control1, resources);
                }
            }
        }
        /// <summary>
        /// 遍历菜单
        /// </summary>
        /// <param name="item"></param>
        /// <param name="resources"></param>
        private static void ApplyToolItme(ToolStripItem item, ComponentResourceManager resources)
        {
            resources.ApplyResources(item, item.Name);
            if (item is ToolStripDropDownButton)
            {
                foreach (ToolStripItem toolStripItem in ((ToolStripDropDownButton)item).DropDownItems)
                {
                    if (toolStripItem is ToolStripMenuItem)
                    {
                        ApplyToolItme(toolStripItem, resources);
                    }
                }
            }           
        }
        #endregion


        #region menu
        /// <summary>
        /// 遍历窗体所有控件
        /// </summary>
        /// <param name="control"></param>
        /// <param name="resources"></param>
        private static void ApplyControl(Control control, ComponentResourceManager resources)
        {
            if (control is MenuStrip)
            {
                resources.ApplyResources(control, control.Name);
                foreach (ToolStripMenuItem toolStripMenuItem in ((MenuStrip)control).Items)
                {
                    ApplyItme(toolStripMenuItem, resources);
                }
            }
            else
            {
                foreach (Control control1 in control.Controls)
                {
                    resources.ApplyResources(control1, control1.Name);
                    ApplyControl(control1, resources);
                }
            }
        }
        /// <summary>
        /// 遍历菜单
        /// </summary>
        /// <param name="item"></param>
        /// <param name="resources"></param>
        private static void ApplyItme(ToolStripItem item, ComponentResourceManager resources)
        {
            resources.ApplyResources(item, item.Name);
            foreach (ToolStripItem toolStripItem in ((ToolStripMenuItem)item).DropDownItems)
            {
                if (toolStripItem is ToolStripMenuItem)
                {
                    ApplyItme(toolStripItem, resources);
                }
            }
        }
        #endregion
    }
}
