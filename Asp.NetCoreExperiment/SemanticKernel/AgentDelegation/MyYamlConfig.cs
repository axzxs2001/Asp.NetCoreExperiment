using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace AgentDelegation
{
    public class MyYamlConfig
    {
        /// <summary>
        /// The agent name
        /// </summary>
        [YamlMember(Alias = "name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The agent description
        /// </summary>
        [YamlMember(Alias = "description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The agent instructions template
        /// </summary>
        [YamlMember(Alias = "template")]
        public string Instructions { get; set; } = string.Empty;

        [YamlMember(Alias = "template_format")]
        public string TemplateFormat { get; set; }

        //[YamlMember(Alias = "input_variables")]
        //public InputVariables[] InputVariables { get; set; }

    }

    public class InputVariables
    {
        [YamlMember(Alias = "name")]
        public string Name { get; set; } = string.Empty;
        [YamlMember(Alias = "description")]
        public string Description { get; set; } = string.Empty;
        [YamlMember(Alias = "is_required")]
        public bool IsRequired { get; set; }
    }


}
