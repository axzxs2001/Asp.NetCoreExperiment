using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Models
{
    public class BlogViewModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Title
        { get; set; }

        [Required]
        [Display(Name = "作者")]
        public string Author
        { get; set; }

        [Display(Name = "浏览次数")]
        public int ViewTimes
        { get; set; }

        [Required]
        [Display(Name = "内容")]
        public string Content
        { get; set; }
    }
}
