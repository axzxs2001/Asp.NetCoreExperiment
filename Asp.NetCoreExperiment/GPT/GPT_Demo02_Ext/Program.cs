
using GPT_Demo02_Ext;

public class 我的类
{
    public int 编号 { get; set; }
    public string 名称 { get; set; }
    public bool 性别 { get; set; }

    [YBDateTime("yyyyMMdd")]
    public DateTime 生日 { get; set; }
    public override string ToString()
    {
        return $"编号：{编号}，名称：{名称}，性别：{性别}";
    }
}
//参照上面类把下面数据生成类
//租户：
//租户ID
//租户名称
//租户类型
//租户状态
//租户创建时间
public class 租户
{
    public int 租户ID { get; set; }
    public string 租户名称 { get; set; }
    public string 租户类型 { get; set; }
    public string 租户状态 { get; set; }

    [YBDateTime("yyyyMMdd")]
    public DateTime 租户创建时间 { get; set; }
    public override string ToString()
    {
        return $"租户ID：{租户ID}，租户名称：{租户名称}，租户类型：{租户类型}，租户状态：{租户状态}";
    }
}






