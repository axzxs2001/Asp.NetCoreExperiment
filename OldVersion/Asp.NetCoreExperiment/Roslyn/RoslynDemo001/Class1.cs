using Microsoft.CodeAnalysis.CSharp.Syntax;
using Smart.Text.Japanese;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RoslynDemo001_1
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
                    "カブシキガイシャ/カブシキカイシャ"
                    ,"ユウゲンガイシャ/ユウゲンカイシャ"
                    ,"ゴウメイガイシャ/ゴウメイカイシャ"
                    ,"ゴウシガイシャ/ゴウシカイシャ"
                    ,"ゴウドウガイシャ/ゴウドウカイシャ"
                    ,"カブシキガイシャ"
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
                    , "セイカツキョウドウクミアイ"
                    , "ノウギョウキョウドウクミアイレンゴウカイ"
                    , "ギョギョウキョウドウクミアイ"
                    , "ギョョウキョウドウクミアイレンゴウカイ"
                    , "キョウドウクミアイ"
                    ,"ドクリツギョウセイホウジン"
                };

        static string[] _companyPhrase2 = new string[]
            {
                "株式会社",
                "有限会社",
                "合名会社",
                "合資会社",
                "合同会社",
                "医療法人",
                "医療法人社団",
                "医療法人財団	",
                "社会医療法人	",
                "財団法人",
                "一般財団法人",
                "公益財団法人	",
                "社団法人",
                "一般社団法人",
                "公益社団法人	",
                "宗教法人",
                "学校法人",
                "社会福祉法人",
                "協同組合",
                "生活協同組合",
                "農業協同組合連合会",
                "漁業協同組合	",
                "漁業協同組合連合会",
                "協業組合",
                "更生保護法人",
                "相互会社",
                "特定非営利活動法人",
                "独立行政法人	",
                "地方独立行政法人",
                "弁護士法人",
                "有限責任中間法人",
                "無限責任中間法人",
                "行政書士法人	",
                "司法書士法人",
                "税理士法人",
                "国立大学法人	",
                "公立大学法人	",
                "農事組合法人	",
                "管理組合法人	",
                "社会保険労務士法人",
                "営業所",
                "出張所",
                "連合会",
                "共済組合",
                "生命保険",
                "海上火災保険",
                "火災海上保険	",
                "健康保険組合	",
                "国民健康保険組合",
                "国民健康保険団体連合",
                "社会保険診療報酬支払",
                "厚生年金基金",
                "従業員組合",
                "労働組合",
                "食糧販売協同組合",
                "国家公務員共済組合連",
                "経済農業協同組合連合",
                "共済農業協同組合連合",
                "公共職業安定所",
                "社会福祉協議会",
                "特別養護老人ホーム",
                "特例財団法人	",
                "特例社団法人	",
                "監査法人",
                "自主規制法人",
                "準学校法人",
                "職業訓練法人	",
                "大学共同利用機関法人",
                "投資法人",
                "土地家屋調査法人",
                "特許業務法人",
            };
        /// <summary>
        /// 空格分隔公司短语。例如：カブシキガイシャ 替换成空格
        /// </summary>
        /// <param name="originalString">原字符串</param>
        /// <returns></returns>
        public static string CompanyPhraseSplitWithNarrowSpace(this string originalString)
        {
            var companyPhrase = _companyPhrase.OrderByDescending(r => r.Length);
            foreach (var ele in companyPhrase)
            {
                if (originalString.StartsWith(ele))
                {
                    originalString = originalString.Insert(ele.Length, " ");
                    break;
                }
                if (originalString.EndsWith(ele))
                {
                    originalString = originalString.Insert(originalString.Length - ele.Length, " ");
                    break;
                }
            }
            return originalString;
        }
        /// <summary>
        /// PayPay 法人名、个人事业主移除 法人格
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string RemoveCompanyPhrase(this string originalString)
        {
            var list = _companyPhrase.OrderByDescending(r => r.Length);
            foreach (var c in list)
            {
                var n = KanaConverter.Convert(c.ToString(), KanaOption.Wide);
                if (originalString.Contains(n))
                {
                    originalString = originalString.Replace(n, "");
                }
            }
            return originalString;
        }
        /// <summary>
        ///PayPay 法人格移除 
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string RemoveCompanyPhrase2(this string originalString)
        {
            var list = _companyPhrase2.OrderByDescending(r => r.Length);
            foreach (var c in list)
            {
                var n = KanaConverter.Convert(c.ToString(), KanaOption.Wide);
                if (originalString.Contains(n))
                {
                    originalString = originalString.Replace(n, "");
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

        #region 公司假名替换
        static Dictionary<string, string> _companyPhraseKana = new Dictionary<string, string>()
        {
                {"カ）", "カブシキガイシャ/カブシキカイシャ"},
                {"（カ）", "カブシキガイシャ/カブシキカイシャ"},
                {"（カ", "カブシキガイシャ/カブシキカイシャ"},
                {"ユ）", "ユウゲンガイシャ/ユウゲンカイシャ"},
                {"（ユ）", "ユウゲンガイシャ/ユウゲンカイシャ"},
                {"（ユ", "ユウゲンガイシャ/ユウゲンカイシャ"},
                {"メ）", "ゴウメイガイシャ/ゴウメイカイシャ"},
                {"（メ）", "ゴウメイガイシャ/ゴウメイカイシャ"},
                {"（メ", "ゴウメイガイシャ/ゴウメイカイシャ"},
                {"シ）", "ゴウシガイシャ/ゴウシカイシャ"},
                {"（シ）", "ゴウシガイシャ/ゴウシカイシャ"},
                {"（シ", "ゴウシガイシャ/ゴウシカイシャ"},
                {"ド）", "ゴウドウガイシャ/ゴウドウカイシャ"},
                {"（ド）", "ゴウドウガイシャ/ゴウドウカイシャ"},
                {"（ド", "ゴウドウガイシャ/ゴウドウカイシャ"},
                {"ザイ）", "ザイダンホウジン"},
                {"シャ）", "シャダンホウジン"},
                {"シユウ）", "シュウキョウホウジン"},
                {"ガク）", "ガッコウホウジン"},
                {"フク）", "シャカイフクシホウジン"},
                {"ドク）", "ドクリツギョウセイホウジン"},
        };
        public static string ReplaceCompanyAliasKana(this string originalString)
        {
            return _companyPhraseKana.Aggregate(originalString, (current, value) => current.Replace(value.Key, value.Value));
        }
        #endregion



        #region 汉字替换 (株)->株式会社
        /// <summary>
        /// 小文字集合
        /// </summary>
        static Dictionary<string, string> _smallCharacter = new Dictionary<string, string>()
        {
            /* 半角小文字 Mapping 半角大文字*/
            {"ァ","ア" },
            {"ィ","イ" },
            {"ゥ","ウ" },
            {"ェ","エ" },
            {"ォ","オ" },
            {"ッ","ツ" },
            {"ャ","ヤ" },
            {"ュ","ユ"},
            {"ョ","ヨ" }
        };
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
        static string _specialCharacters = $@"「""'.\/」""・.,-&＆／＼";
        /// <summary>
        /// 空格替换特列字符。例如：＾￣＿ヽヾゝゞ〃仝々〆〇ー―‐／＼～∥｜…‥‘  等替换成空格
        /// </summary>
        /// <param name="originalString">原字符串</param>
        /// <returns></returns>
        public static string SpecialCharactersReplaceNarrowSpace(this string originalString)
        {
            var list = _specialCharacters.ToList();
            foreach (var c in list)
            {
                var n = KanaConverter.Convert(c.ToString(), KanaOption.Wide);
                if (originalString.Contains(n))
                {
                    originalString = originalString.Replace(n, " ");
                }
            }
            return originalString;
        }
        /// <summary>
        /// 移除特殊字符集
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(this string originalString)
        {
            var list = _specialCharacters.ToList();
            foreach (var c in list)
            {
                var n = KanaConverter.Convert(c.ToString(), KanaOption.Wide);
                if (originalString.Contains(n))
                {
                    originalString = originalString.Replace(n, "");
                }
            }
            return originalString;
        }
        static string _specialChanractersKana = "｡｢｣､･";
        public static string SpecialKanaCharactersReplaceNarrowSpace(this string originalString)
        {
            var list = _specialChanractersKana.ToList();
            foreach (var c in list)
            {
                var n = KanaConverter.Convert(c.ToString(), KanaOption.Wide);
                if (originalString.Contains(n))
                {
                    originalString = originalString.Replace(n, " ");
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
        public static string SpecialCharactersReplaceWideSpace(this string originalString, string replaceString)
        {
            var list = _specialCharacters.ToList();
            foreach (var c in list)
            {
                var n = KanaConverter.Convert(c.ToString(), KanaOption.Wide);
                if (originalString.Contains(n))
                {
                    originalString = originalString.Replace(n, replaceString);
                }
            }
            return originalString;
        }
        #endregion

        static Dictionary<string, string> _numberMappingNumberKana = new Dictionary<string, string>()
         {
            {"１","イチ" },
            {"２","ニ" },
            {"３","サン" },
            {"４","ヨン" },
            {"５","ゴ" },
            {"６","ロク"},
            {"７","ナナ" },
            {"８","ハチ" },
            {"９","キュウ" },
            {"０","ゼロ"}
         };
        /// <summary>
        /// 将数字替换成Kana
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string ReplaceFromNumberToNumberKana(this string originalString)
        {
            return _numberMappingNumberKana.Aggregate(originalString, (current, value) => current.Replace(value.Key, value.Value));
        }

        static Dictionary<string, string> _alphabetMappingNumberKana = new Dictionary<string, string>()
         {
        {"Ａ",        "エイ"},
        {"Ｂ",       "ビー"},
        {"Ｃ",       "シー"},
        {"Ｄ",       "ディー"},
        {"Ｅ",       "イー"},
        {"Ｆ",       "エフ"},
        {"Ｇ",       "ジー"},
        {"Ｈ",       "エイチ"},
        {"Ｉ",       "アイ"},
        {"Ｊ",       "ジェイ"},
        {"Ｋ",       "ケイ"},
        {"Ｌ",       "エル"},
        {"Ｍ",       "エム"},
        {"Ｎ",       "エヌ"},
        {"Ｏ",       "オー"},
        {"Ｐ",       "ピー"},
        {"Ｑ",       "キュー"},
        {"Ｒ",       "アール"},
        {"Ｓ",       "エス"},
        {"Ｔ",       "ティー"},
        {"Ｕ",       "ユー"},
        {"Ｖ",       "ヴィー"},
        {"Ｗ",       "ダブリュー"},
        {"Ｘ",       "エックス"},
        {"Ｙ",       "ワイ"},
        {"Ｚ",       "ゼッド"},

        {"ａ" ,      "エイ"},
        {"ｂ" ,      "ビー"},
        {"ｃ" ,      "シー"},
        {"ｄ" ,      "ディー"},
        {"ｅ" ,      "イー"},
        {"ｆ" ,      "エフ"},
        {"ｇ" ,      "ジー"},
        {"ｈ" ,      "エイチ"},
        {"ｉ" ,      "アイ"},
        {"ｊ" ,      "ジェイ"},
        {"ｋ" ,      "ケイ"},
        {"ｌ" ,      "エル"},
        {"ｍ" ,      "エム"},
        {"ｎ" ,      "エヌ"},
        {"ｏ" ,      "オー"},
        {"ｐ" ,      "ピー"},
        {"ｑ" ,      "キュー"},
        {"ｒ" ,      "アール"},
        {"ｓ" ,      "エス"},
        {"ｔ" ,      "ティー"},
        {"ｕ" ,      "ユー"},
        {"ｖ" ,      "ヴィー"},
        {"ｗ" ,      "ダブリュー"},
        {"ｘ" ,      "エックス"},
        {"ｙ" ,      "ワイ"},
        {"ｚ" ,      "ゼッド"}
         };

        /// <summary>
        /// 字母替换成字母kana
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string ReplaceFromAlphabetToAlphabetKana(this string originalString)
        {
            return _alphabetMappingNumberKana.Aggregate(originalString, (current, value) => current.Replace(value.Key, value.Value));
        }
        static Dictionary<string, string> _oldWordMappingNewWord = new Dictionary<string, string>()
        {
           {"增","増"},
           {"寬","寛"},
           {"德","徳"},
           {"悅","悦"},
           {"敎","教"},
           {"橫","横"},
           {"淸","清"},
           {"瀨","瀬"},
           {"甁","瓶"},
           {"綠","緑"},
           {"緖","緒"},
           {"薰","薫"},
           {"賴","頼"},
           {"郞","郎"},
           {"鄕","郷"},
           {"閒","間"},
           {"靑","青"},
           {"髙","高"},
           {"黑","黒"} ,
        };
        /// <summary>
        /// 将就文字替换成新文字
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string TurnOldWordIntoNewWord(this string originalString)
        {
            return _oldWordMappingNewWord.Aggregate(originalString, (currentm, value) => currentm.Replace(value.Key, value.Value));
        }
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
        /// 全角小文字转半角大文字
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string SmallCharacterReplaceBigCharacter(this string originalString)
        {
            return _smallCharacter.Aggregate(originalString, (current, value) => current.Replace(value.Key, value.Value));
        }
        /// <summary>
        /// 全角16文字以上的情况下，全角变成半角（半角32btye以内）　　 
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string IsFromNarrowConvertWideByLength(this string originalString)
        {
            Encoding shiftJis = Encoding.GetEncoding("Shift-JIS");
            //byte[] bytes = shiftJis.GetBytes(originalString);
            //if (bytes.Length > 16)
            //{
            //    originalString = KanaConverter.Convert(originalString, KanaOption.Narrow);
            byte[] bytes = shiftJis.GetBytes(originalString);
            if (bytes.Length > 32)
            {
                int n = 0; //  表示当前的字节数
                int i = 0; //  要截取的字节数
                for (; i < bytes.Length && n < 32; i++)
                {
                    //  偶数位置，如0、2、4等，为UCS2编码中两个字节的第一个字节
                    if (i % 2 == 0)
                    {
                        n++; //  在UCS2第一个字节时n加1
                    }
                    else
                    {
                        //  当UCS2编码的第二个字节大于0时，该UCS2字符为汉字，一个汉字算两个字节
                        if (bytes[i] > 0)
                        {
                            n++;
                        }
                    }
                }
                //  如果i为奇数时，处理成偶数
                if (i % 2 == 1)
                {
                    //  该UCS2字符是汉字时，去掉这个截一半的汉字
                    if (bytes[i] > 0)
                        i = i - 1;

                    //  该UCS2字符是字母或数字，则保留该字符
                    else
                        i = i + 1;
                }
                originalString = shiftJis.GetString(bytes, 0, i);
                return originalString;
            }
            return originalString;
            //}
        }

    }
}

