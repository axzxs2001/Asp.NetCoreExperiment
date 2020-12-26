
namespace JaegerSharp
{
    /// <summary>
    /// Jaeger选项
    /// </summary>
    public class JaegerOptions
    {
        /// <summary>
        /// 是否启用Form数据转成Span
        /// </summary>
        public bool IsFormSpan { get; set; } = false;
        /// <summary>
        /// Form数据最大长度
        /// </summary>
        public int FormValueMaxLength { get; set; } = 100;
        /// <summary>
        /// 是否启用Query数据转成Span
        /// </summary>
        public bool IsQuerySpan { get; set; } = false;
        /// <summary>
        /// Query最大长度
        /// </summary>
        public int QueryValueMaxLength { get; set; } = 100;
        /// <summary>
        /// Form或Query中不作为Span的key集合
        /// </summary>
        public string[] NoSpanKeys { get; set; } = new string[] { "password", "pwd" };

    }
}
