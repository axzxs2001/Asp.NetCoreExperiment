using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NLuaAndRoslynCompare
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExpand
    {
        #region 公司短语处理
        /// <summary>
        /// 公司短语
        /// </summary>
        static string[] _companyPhrase = new string[] {
                    "カブシキガイシャ"
                    , "カブシキカイシャ"
                    , "ユウゲンガイシャ"
                    , "ユウゲンカイシャ"
                    , "ゴウメイガイシャ"
                    , "ゴウメイカイシャ"
                    , "ゴウシガイシャ"
                    , "ゴウシカイシャ"
                    , "ゴウドウガイシャ"
                    , "ゴウドウカイシャ"
                    , "イリョウホウジン"
                    , "イリョウホウジンシャダン"
                    , "イリョウホウジンザイダン"
                    , "シャカイイリョウホウジン"
                    , "ザイダンホウジン"
                    , "イッパンザイダンホウジン"
                    , "コウエキザイダンホウジン"
                    , "シャダンホウジン"
                    , "イッパンシャダンホウジン"
                    , "コウエキシャダンホウジン"
                    , "シュウキョウホウジン"
                    , "ガッコウホウジン"
                    , "シャカイフクシホウジン"
                    , "キョウドウクミアイ"
                    , "セイカツキョウドウクミアイ"
                    , "ノウギョウキョウドウクミアイレンゴウカイ"
                    , "ギョギョウキョウドウクミアイ"
                    , "ギョョウキョウドウクミアイレンゴウカイ"
                    , "キョウドウクミアイ"
                };
        /// <summary>
        /// 空格分隔公司短语。例如：カブシキガイシャ 替换成空格
        /// </summary>
        /// <param name="originalString">原字符串</param>
        /// <returns></returns>
        public static string CompanyPhraseSplitWithSpace(this string originalString)
        {
            foreach (var ele in _companyPhrase)
            {
                if (ele.StartsWith(ele))
                {
                    originalString.Insert(ele.Length, " ");
                    break;
                }
                if (ele.EndsWith(ele))
                {
                    originalString = originalString.Insert(originalString.Length - ele.Length, " ");
                    break;
                }
            }
            return originalString;
        }
        /// <summary>
        /// 指定字符串分隔公司短语。例如：カブシキガイシャ 替换成指定字符
        /// </summary>
        /// <param name="originalString">原字符串</param>
        /// <param name="splitString">分割字符串</param>
        /// <returns></returns>
        public static string CompanyPhraseSplitWith(this string originalString, string splitString)
        {
            foreach (var ele in _companyPhrase)
            {
                if (ele.StartsWith(ele))
                {
                    originalString.Insert(ele.Length, splitString);
                    break;
                }
                if (ele.EndsWith(ele))
                {
                    originalString = originalString.Insert(originalString.Length - ele.Length, splitString);
                    break;
                }
            }
            return originalString;
        }
        #endregion

        #region 汉字替换 (株)->株式会社
        /// <summary>
        /// 公司字符替换集合
        /// </summary>
        static Dictionary<string, string> _characterReplaceSets = new Dictionary<string, string>()
        {
                {"(株)", "株式会社"},
                {"(有)", "有限会社"},
                {"(名)", "合名会社"},
                {"(資)", "合資会社"},
                {"(同)", "合同会社"},
                {"(医)", "医療法人"},

                {"(株", "株式会社"},
                {"(有", "有限会社"},
                {"(名", "合名会社"},
                {"(資", "合資会社"},
                {"(同", "合同会社"},
                {"(医", "医療法人"},

                {"株)", "株式会社"},
                {"有)", "有限会社"},
                {"名)", "合名会社"},
                {"資)", "合資会社"},
                {"同)", "合同会社"},
                {"医)", "医療法人"},

                {"(カ)", "株式会社"},
                {"(ユ)", "有限会社"},
                {"(メ)", "合名会社"},
                {"(シ)", "合資会社"},
                {"(ド)", "合同会社"},
                {"(イ)", "医療法人"},

                {"(カ", "株式会社"},
                {"(ユ", "有限会社"},
                {"(メ", "合名会社"},
                {"(シ", "合資会社"},
                {"(ド", "合同会社"},
                {"(イ", "医療法人"},

                {"カ)", "株式会社"},
                {"ユ)", "有限会社"},
                {"メ)", "合名会社"},
                {"シ)", "合資会社"},
                {"ド)", "合同会社"},
                {"イ)", "医療法人"},

                {"(財)", "財団法人"},
                {"(一財)", "一般財団法人"},
                {"(公財)", "公益財団法人"},
                {"(社)", "社団法人"},
                {"(一社)", "一般社団法人"},
                {"(公社)", "公益社団法人"},
                {"(宗)", "宗教法人"},
                {"(学)", "学校法人"},
                {"(福)", "社会福祉法人"},
                {"(協組)", "協同組合"},
                {"(生協)", "生活協同組合"},
                {"(農協連)", "農業協同組合連合会"},
                {"(漁協)", "漁業協同組合"},
                {"(漁連)", "漁業協同組合連合会"},
                {"(業)", "協業組合"},

                {"財)", "財団法人"},
                {"一財)", "一般財団法人"},
                {"公財)", "公益財団法人"},
                {"社)", "社団法人"},
                {"一社)", "一般社団法人"},
                {"公社)", "公益社団法人"},
                {"宗)", "宗教法人"},
                {"学)", "学校法人"},
                {"福)", "社会福祉法人"},
                {"協組)", "協同組合"},
                {"生協)", "生活協同組合"},
                {"農協連)", "農業協同組合連合会"},
                {"漁協)", "漁業協同組合"},
                {"漁連)", "漁業協同組合連合会"},
                {"業)", "協業組合"},

                {"(財", "財団法人"},
                {"(一財", "一般財団法人"},
                {"(公財", "公益財団法人"},
                {"(社", "社団法人"},
                {"(一社", "一般社団法人"},
                {"(公社", "公益社団法人"},
                {"(宗", "宗教法人"},
                {"(学", "学校法人"},
                {"(福", "社会福祉法人"},
                {"(協組", "協同組合"},
                {"(生協", "生活協同組合"},
                {"(農協連", "農業協同組合連合会"},
                {"(漁協", "漁業協同組合"},
                {"(漁連", "漁業協同組合連合会"},
                {"(業", "協業組合"},

                {"(相)", "相互会社"},
                {"(特非)", "特定非営利活動法人"},
                {"(独)", "独立行政法人"},
                {"(地独)", "地方独立行政法人"},
                {"(弁)", "弁護士法人"},
                {"(中)", "有限責任中間法人"},

                {"相)", "相互会社"},
                {"特非)", "特定非営利活動法人"},
                {"独)", "独立行政法人"},
                {"地独)", "地方独立行政法人"},
                {"弁)", "弁護士法人"},
                {"中)", "有限責任中間法人"},

                {"(相", "相互会社"},
                {"(特非", "特定非営利活動法人"},
                {"(独", "独立行政法人"},
                {"(地独", "地方独立行政法人"},
                {"(弁", "弁護士法人"},
                {"(中", "有限責任中間法人"},

                {"(行)", "行政書士法人"},
                {"(司)", "司法書士法人"},
                {"(税)", "税理士法人"},
                {"(大)", "国立大学法人"},

                {"行)", "行政書士法人"},
                {"司)", "司法書士法人"},
                {"税)", "税理士法人"},
                {"大)", "国立大学法人"},

                {"(行", "行政書士法人"},
                {"(司", "司法書士法人"},
                {"(税", "税理士法人"},
                {"(大", "国立大学法人"},

                {"(営)", "営業所"},
                {"(出)", "出張所"},
                {"(連)", "連合会"},
                {"(共済)", "共済組合"},
                {"(生命)", "生命保険"},
                {"(海上)", "海上火災保険"},
                {"(火災)", "火災海上保険"},
                {"(健保)", "健康保険組合"},
                {"(国保)", "国民健康保険組合"},
                {"(国保連)", "国民健康保険団体連合会"},
                {"(社保)", "社会保険診療報酬支払基金"},
                {"(厚年)", "厚生年金基金"},
                {"(従組)", "従業員組合"},
                {"(労組)", "労働組合"},
                {"(食販協)", "食糧販売協同組合"},
                {"(国共連)", "国家公務員共済組合連合会"},
                {"(経済連)", "経済農業協同組合連合会"},
                {"(共済連)", "共済農業協同組合連合会"},
                {"(職安)", "公共職業安定所"},
                {"(社協)", "社会福祉協議会"},
                {"(特養)", "特別養護老人ホーム"},
                {"(特財)", "特例財団法人"},
                {"(特社)", "特例社団法人"},

                {"営)", "営業所"},
                {"出)", "出張所"},
                {"連)", "連合会"},
                {"共済)", "共済組合"},
                {"生命)", "生命保険"},
                {"海上)", "海上火災保険"},
                {"火災)", "火災海上保険"},
                {"健保)", "健康保険組合"},
                {"国保)", "国民健康保険組合"},
                {"国保連)", "国民健康保険団体連合会"},
                {"社保)", "社会保険診療報酬支払基金"},
                {"厚年)", "厚生年金基金"},
                {"従組)", "従業員組合"},
                {"労組)", "労働組合"},
                {"食販協)", "食糧販売協同組合"},
                {"国共連)", "国家公務員共済組合連合会"},
                {"経済連)", "経済農業協同組合連合会"},
                {"共済連)", "共済農業協同組合連合会"},
                {"職安)", "公共職業安定所"},
                {"社協)", "社会福祉協議会"},
                {"特養)", "特別養護老人ホーム"},
                {"特財)", "特例財団法人"},
                {"特社)", "特例社団法人"},

                {"(営", "営業所"},
                {"(出", "出張所"},
                {"(連", "連合会"},
                {"(共済", "共済組合"},
                {"(生命", "生命保険"},
                {"(海上", "海上火災保険"},
                {"(火災", "火災海上保険"},
                {"(健保", "健康保険組合"},
                {"(国保", "国民健康保険組合"},
                {"(国保連", "国民健康保険団体連合会"},
                {"(社保", "社会保険診療報酬支払基金"},
                {"(厚年", "厚生年金基金"},
                {"(従組", "従業員組合"},
                {"(労組", "労働組合"},
                {"(食販協", "食糧販売協同組合"},
                {"(国共連", "国家公務員共済組合連合会"},
                {"(経済連", "経済農業協同組合連合会"},
                {"(共済連", "共済農業協同組合連合会"},
                {"(職安", "公共職業安定所"},
                {"(社協", "社会福祉協議会"},
                {"(特養", "特別養護老人ホーム"},
                {"(特財", "特例財団法人"},
                {"(特社", "特例社団法人"},
        };
        /// <summary>
        /// 小文字集合
        /// </summary>
        static Dictionary<string, string> _smallCharacter = new Dictionary<string, string>()
        {
            {"ァ","ア" },
            {"ィ","イ" },
            {"ゥ","ウ" },
            {"ェ","エ" },
            {"ォ","オ" },
            {"ッ","ツ" },
            {"ャ","ヤ" },
            {"ュ","ユ" },
            {"ョ","ヨ" },
            {"ー","カ" },
            {"ｧ","ｱ" },
            {"ｨ","ｲ" },
            {"ｩ","ｳ" },
            {"ｪ","ｴ" },
            {"ｫ","ｵ" },
            {"ｯ","ﾂ" },
            {"ｬ","ﾔ" },
            {"ｭ","ユ"},
            {"ｮ","ﾖ" },
            {"ｰ","ｶ" }
        };
        /// <summary>
        /// 公司字符集合替换。例如：(株) 替换成 株式会社
        /// </summary>
        /// <param name="originalString">原字符串</param>
        /// <returns></returns>
        public static string CompanyCharacterReplace(this string originalString)
        {
            return _characterReplaceSets.Aggregate(originalString, (current, value) => current.Replace(value.Key, value.Value));
        }
        #endregion

        #region 环境依存替换
        /// <summary>
        /// 环境依存集合
        /// </summary>
        static Dictionary<string, string> _environmentDependenceSets = new Dictionary<string, string>()
         {
                {"﨑", "崎" },
                {"德", "徳" },
                {"濵", "濱" },
                {"髙", "高" },
                {"㈱", "株式会社" },
                {"㈲", "有限会社" },
                {"㈴", "合名会社" },
                {"㈾", "合資会社" }
         };
        /// <summary>
        /// 环境依存字符串替换
        /// </summary>
        /// <param name="originalString">原字符串</param>
        /// <returns></returns>
        public static string EnvironmentDependenceReplace(this string originalString)
        {
            return _environmentDependenceSets.Aggregate(originalString, (current, value) => current.Replace(value.Key, value.Value));
        }
        #endregion

        #region 特列字符串替换
        /// <summary>
        /// 特殊字符  
        /// </summary>
        static string _specialCharacters = ".[、。，．・：；？！゛゜´｀¨＾￣＿ヽヾゝゞ〃仝々〆〇ー―‐／＼～∥｜…‥‘’“”（）〔〕［］｛｝〈〉《》「」『』【】＋－±×÷＝≠＜＞≦≧∞∴♂♀°′″℃￥＄￠￡％＃＆＊＠§☆★○●◎◇◆□■△▲▽▼※〒→←↑↓〓∈∋⊆⊇⊂⊃∪∩∧∨￢⇒⇔∀∃∠⊥⌒∂∇≡≒≪≫√∽∝∵∫∬Å‰♯♭♪†‡¶◯ΓΔΘΙΛΞΟΠΣΤΥΦΧΨΩαβγδεζηθικλμνξοπρστυφχψωабвгдеёжзийклмноптфцчшщъыьэюя─│┌┐┘└├┬┤┴┼━┃┏┓┛┗┣┳┫┻╋┠┯┨┷┿┝┰┥┸╂]";
        /// <summary>
        /// 空格替换特列字符。例如：＾￣＿ヽヾゝゞ〃仝々〆〇ー―‐／＼～∥｜…‥‘  等替换成空格
        /// </summary>
        /// <param name="originalString">原字符串</param>
        /// <returns></returns>
        public static string SpecialCharactersReplaceWithSpace(this string originalString)
        {
            var list = _specialCharacters.ToList();
            foreach (var c in list)
            {
                if (originalString.Contains(c))
                {
                    originalString = originalString.Replace(c, ' ');
                }
            }
            return originalString;
        }
        /// <summary>
        /// 指定字符串替换特列字符。例如：＾￣＿ヽヾゝゞ〃仝々〆〇ー―‐／＼～∥｜…‥‘  等替换成空格
        /// </summary>
        /// <param name="originalString">原字符串</param>
        /// <param name="replaceString">替换字符串</param>
        /// <returns></returns>
        public static string SpecialCharactersReplaceWithSpace(this string originalString, string replaceString)
        {
            return Regex.Replace(originalString, _specialCharacters, replaceString);
        }
        #endregion

        /// <summary>
        /// 日期转换（具体文化特征）成短格式 yyMMdd
        /// </summary>
        /// <param name="originalString">原字符串</param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ConvertDateToShortJapaneseCultureInfo(this string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime date))
            {
                var cultureInfo = new CultureInfo("ja-JP");
                var japaneseCalendar = new JapaneseCalendar();
                cultureInfo.DateTimeFormat.Calendar = japaneseCalendar;
                //获取和暦
                return date.ToString("yyMMdd", cultureInfo);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 日期年号
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string ConvertDateToYearNumber(this string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime date))
            {
                var cultureInfo = new CultureInfo("ja-JP");
                var japaneseCalendar = new JapaneseCalendar();
                cultureInfo.DateTimeFormat.Calendar = japaneseCalendar;
                //获取元号
                var strEraJpDate = cultureInfo.DateTimeFormat.GetAbbreviatedEraName(japaneseCalendar.GetEra(date));
                return strEraJpDate switch
                {
                    "令" => "4",
                    "平" => "4",
                    "昭" => "3",
                    "大" => "2",
                    "明" => "1",
                    _ => null
                };
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 长度切割
        /// </summary>
        /// <param name="originalString">原字符串</param>
        /// <param name="length">切割长度</param>
        /// <returns></returns>
        public static string LengthCut(this string originalString, int length)
        {
            return originalString.Length > length - 1 ? originalString.Substring(0, length) : originalString;
        }
        /// <summary>
        /// 将oldValue替换成空
        /// </summary>
        /// <param name="originalString">原字符串</param>
        /// <param name="oldValue">旧字符串</param>
        /// <returns></returns>
        public static string ReplaceSpace(this string originalString, string oldValue)
        {
            return originalString.Replace(oldValue, "");
        }
        /// <summary>
        /// 拼接到头部
        /// </summary>
        /// <param name="originalString">原字符串</param>
        /// <param name="head">头部字符串</param>
        /// <returns></returns>
        public static string AddHead(this string originalString, string head)
        {
            return $"{head}{originalString}";
        }
        /// <summary>
        /// 拼接到尾部
        /// </summary>
        /// <param name="originalString">原字符串</param>
        /// <param name="tail">尾部字符串</param>
        /// <returns></returns>
        public static string AddTail(this string originalString, string tail)
        {
            return $"{originalString}{tail}";
        }
        public static string IsSaturdayOrSunday(this string originalString)
        {
            //ApproxReleaseDate
            if (DateTime.TryParse(originalString, out DateTime result))
            {
                DayOfWeek dayofweek = result.DayOfWeek;
                if (dayofweek == DayOfWeek.Saturday)
                {
                    return result.AddDays(2).ToString("yyyy/MM/dd");
                }
                else if (dayofweek == DayOfWeek.Sunday)
                {
                    return result.AddDays(1).ToString("yyyy/MM/dd");
                }
                else
                {
                    return originalString;
                }
            }
            return originalString;
        }

        public static string ReplaceMark(this string originalString)
        {
            var newValue = Regex.Replace(originalString, _specialCharacters, string.Empty);
            return newValue;
        }
        /// <summary>
        /// 小文字转大文字
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string SmallCharacterReplaceBigCharacter(this string originalString)
        {
            return _smallCharacter.Aggregate(originalString, (current, value) => current.Replace(value.Key, value.Value));
        }
    }

}

