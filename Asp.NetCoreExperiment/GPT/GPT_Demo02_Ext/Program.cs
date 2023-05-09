//人员的属性可以包括：姓名、性别、年龄、联系方式、职位、工作经验、学历、技能、职业目标、个人爱好等。前面的数据分别对应编程语言的什么类型？并生成对应的正则表达式
//姓名：字符串，正则表达式：/^[\u4e00-\u9fa5] { 2,4}$/

//性别：字符串，正则表达式：/^(男|女)$/

//年龄：数字，正则表达式：/^[1-9]\d ?$/

//联系方式：字符串，正则表达式：/^1[3456789]\d{9}$/

//职位：字符串，正则表达式：/^[\u4e00-\u9fa5] { 2,4}$/

//工作经验：数字，正则表达式：/^[1-9]\d ?$/

//学历：字符串，正则表达式：/^(大专|本科|硕士|博士)$/

//技能：字符串，正则表达式：/^[\u4e00-\u9fa5] { 2,4}$/

//职业目标：字符串，正则表达式：/^[\u4e00-\u9fa5] { 2,4}$/

//个人爱好：字符串，正则表达式：/^[\u4e00-\u9fa5] { 2,4}$/

//人员的属性可以包括：姓名、性别、年龄、联系方式、职位、工作经验、学历、技能、职业目标、个人爱好等。生成一个C#的实体类，适合定义枚举的，定义成枚举类型。
public class Person
{
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public string Contact { get; set; }
    public string Position { get; set; }
    public int WorkExperience { get; set; }
    public Education Education { get; set; }
    public string Skills { get; set; }
    public string CareerGoal { get; set; }
    public string Hobby { get; set; }
}

public enum Gender
{
    Male,
    Female
}

public enum Education
{
    Bachelor,
    Master,
    Doctor
}

