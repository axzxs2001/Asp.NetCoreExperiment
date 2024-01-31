using Ganss.Xss;
using OWASP.AntiSamy.Html;
using System.Globalization;
using System.Reflection;
using System.Xml;

List<int> subset = new List<int> { 1, 2, 3 ,0};
List<int> superset = new List<int> { 1, 2, 3, 4, 5, 6 };

// 使用LINQ判断subset是否是superset的子集
bool isSubset = subset.All(element => superset.Contains(element));

// 输出结果
Console.WriteLine(isSubset ? "是子集" : "不是子集");



var assembly = Assembly.GetExecutingAssembly();
var resourceName = "AntiXSSDemo.AntiSamyPolicyExamples.antisamy.xml";
var html = "";
using (Stream stream = assembly.GetManifestResourceStream(resourceName))
//using (StreamReader reader = new StreamReader(stream))
{
    //string result = reader.ReadToEnd();


    //Policy policy = Policy.GetInstance(TestConstants.DEFAULT_POLICY_PATH);


    html = "test<script>alert(document.cookie)</script>";
    html = "test<a onClick='alert(document.cookie)'>pee</a>";


    //Thread.CurrentThread.CurrentCulture = new CultureInfo("ja-JP", true);
    //Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP", true);
    html = "<script>al()</script><p>各位观众</p><p><br></p><a onClick='alert(document.cookie)' onFocus=\"alter()\" target=\"_blank\"></a>\r\n";
    //    var antiSamy = new AntiSamy();
    //    CleanResults results = antiSamy.Scan(html, stream);

    //    Console.WriteLine(results.GetNumberOfErrors());

    //    foreach (var message in results.GetErrorMessages())
    //    {
    //        Console.WriteLine(message);
    //    }


    //    Console.WriteLine(results.GetCleanHtml()); // Some custom function
    //}
    //Console.ReadLine();

    Console.WriteLine("-------------------------");
    var sanitizer = new HtmlSanitizer();
    sanitizer.RemovingAttribute += (s, e) =>
    {
        Console.WriteLine($"Removing attribute {e.Attribute} from element {e.Tag}");
    };
    sanitizer.RemovingTag += (s, e) =>
    {
        Console.WriteLine($"Removing tag {e.Tag}");
    };
    sanitizer.RemovingStyle += (s, e) =>
    {
        Console.WriteLine($"Removing style {e.Style}");
    };
    sanitizer.RemovingAtRule += (s, e) =>
    {
        Console.WriteLine($"Removing at-rule {e.Rule}");
    };
    sanitizer.RemovingComment += (s, e) =>
    {
        Console.WriteLine($"Removing comment {e.Comment}");
    };
    sanitizer.RemovingCssClass += (s, e) =>
    {
        Console.WriteLine($"Removing css class {e.CssClass}");
    };

    var formatter = AngleSharp.Xhtml.XhtmlMarkupFormatter.Instance;

    Console.WriteLine(sanitizer.Sanitize(html, outputFormatter: formatter));


}

public static class TestConstants
{
    public static readonly string DEFAULT_POLICY_PATH = "AntiSamyPolicyExamples/antisamy.xml";
    public static readonly string ANYTHINGGOES_POLICY_PATH = "AntiSamyPolicyExamples/antisamy-anythinggoes.xml";
    public static readonly string EBAY_POLICY_PATH = "AntiSamyPolicyExamples/antisamy-ebay.xml";
    public static readonly string MYSPACE_POLICY_PATH = "AntiSamyPolicyExamples/antisamy-myspace.xml";
    public static readonly string SLASHDOT_POLICY_PATH = "AntiSamyPolicyExamples/antisamy-slashdot.xml";
    public static readonly string TINYMCE_POLICY_PATH = "AntiSamyPolicyExamples/antisamy-tinymce.xml";
    public static readonly string POLICY_HEADER = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\" ?>\n" +
                                     "<anti-samy-rules xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                                     "xsi:noNamespaceSchemaLocation=\"antisamy.xsd\">\n";
    public static readonly string POLICY_DIRECTIVES = "<directives>\n</directives>\n";
    public static readonly string POLICY_COMMON_ATTRIBUTES = "<common-attributes>\n</common-attributes>\n";
    public static readonly string POLICY_GLOBAL_TAG_ATTRIBUTES = "<global-tag-attributes>\n</global-tag-attributes>\n";
    public static readonly string POLICY_DYNAMIC_TAG_ATTRIBUTES = "<dynamic-tag-attributes>\n</dynamic-tag-attributes>\n";
    public static readonly string POLICY_TAG_RULES = "<tag-rules>\n</tag-rules>";
    public static readonly string POLICY_CSS_RULES = "<css-rules>\n</css-rules>\n";
    public static readonly string POLICY_COMMON_REGEXPS = "<common-regexps>\n</common-regexps>";
    public static readonly string POLICY_FOOTER = "</anti-samy-rules>";
}