
using GPT_Demo02_Ext;

/// <summary>
/// 人员
/// </summary>
public class RenYuan
{
    /// <summary>
    /// 编号
    /// </summary>
    public int BianHao { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string MingCheng { get; set; }
    /// <summary>
    /// 性别
    /// </summary>
    public bool XingBie { get; set; }
    /// <summary>
    /// 生日
    /// </summary>
    [YBDateTime("yyyyMMdd")]
    public DateTime ShengRi { get; set; }
    public override string ToString()
    {
        return $"编号：{BianHao}，名称：{MingCheng}，性别：{XingBie}，生日：{ShengRi}";
    }
}
