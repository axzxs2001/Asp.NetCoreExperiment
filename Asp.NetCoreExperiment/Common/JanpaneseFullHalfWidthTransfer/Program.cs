using Smart.Text.Japanese;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace JanpaneseFullHalfWidthTransfer
{
    class Program
    {
        private const string Hankana =
       "ｧｱｨｲｩｳｪｴｫｵｶｶﾞｷｷﾞｸｸﾞｹｹﾞｺｺﾞｻｻﾞｼｼﾞｽｽﾞｾｾﾞｿｿﾞﾀﾀﾞﾁﾁﾞｯﾂﾂﾞﾃﾃﾞﾄﾄﾞﾅﾆﾇﾈﾉﾊﾊﾞﾊﾟﾋﾋﾞﾋﾟﾌﾌﾞﾌﾟﾍﾍﾞﾍﾟﾎﾎﾞﾎﾟﾏﾐﾑﾒﾓｬﾔｭﾕｮﾖﾗﾘﾙﾚﾛﾜｦﾝｳﾞﾜﾞｦﾞ" +
       "ﾞﾟｰ" +
       "｡｢｣､･";

        private const string Katakana =
            "ァアィイゥウェエォオカガキギクグケゲコゴサザシジスズセゼソゾタダチヂッツヅテデトドナニヌネノハバパヒビピフブプヘベペホボポマミムメモャヤュユョヨラリルレロワヲンヴ\u30F7\u30FA" +
            "゛゜ー" +
            "。「」、・";

        private const string Hiragana =
            "ぁあぃいぅうぇえぉおかがきぎくぐけげこごさざしじすずせぜそぞただちぢっつづてでとどなにぬねのはばぱひびぴふぶぷへべぺほぼぽまみむめもゃやゅゆょよらりるれろわをん\u3094\u30F7\u30FA" +
            "゛゜ー" +
            "。「」、・";

        private const string KatakanaOdoriji =
            "ヽヾ";

        private const string HiraganaOdoriji =
            "ゝゞ";


        static void Main(string[] args)
        {
            //Console.WriteLine("+-*/".ToHalfWidth());
            //Console.WriteLine("+-*/".ToFullWidth());
            //Console.WriteLine("アイウエオ".ToHalfWidth());
            //Console.WriteLine("ｱｲｳｴｵ".ToFullWidth());
            //Console.ReadLine();
            Console.OutputEncoding = Encoding.UTF8;
            // Katakana/Hankana
            Console.WriteLine($"========================KatakanaToHankana======================");
            Console.WriteLine($"old Katakana:{Katakana}");
            Console.WriteLine($"converted Hankana:{KanaConverter.Convert(Katakana, KanaOption.KatakanaToHankana)}");
            Console.WriteLine();

            Console.WriteLine($"========================HankanaToKatakana=======================");
            Console.WriteLine($"old Hankana:{Hankana}");
            Console.WriteLine($"converted Katakana:{KanaConverter.Convert(Hankana, KanaOption.HankanaToKatakana)}");
            Console.WriteLine();

            // Hiragana/Hankana
            //Assert.Equal(Hankana, KanaConverter.Convert(Hiragana, KanaOption.HiraganaToHankana));
            Console.WriteLine($"========================HiraganaToHankana=======================");
            Console.WriteLine($"old Hiragana:{Hiragana}");
            Console.WriteLine($"converted Hankana:{KanaConverter.Convert(Hiragana, KanaOption.HiraganaToHankana)}");
            Console.WriteLine();

            //Assert.Equal(Hiragana, KanaConverter.Convert(Hankana, KanaOption.HankanaToHiragana));
            Console.WriteLine($"========================HankanaToHiragana=======================");
            Console.WriteLine($"old Hankana:{Hankana}");
            Console.WriteLine($"converted Hiragana:{KanaConverter.Convert(Hankana, KanaOption.HankanaToHiragana)}");
            Console.WriteLine();



            //// Hiragana/Katakana
            //Assert.Equal(Hiragana, KanaConverter.Convert(Katakana, KanaOption.KatakanaToHiragana));
            Console.WriteLine($"========================KatakanaToHiragana=======================");
            Console.WriteLine($"old Katakana:{Katakana}");
            Console.WriteLine($"converted Hiragana:{KanaConverter.Convert(Katakana, KanaOption.KatakanaToHiragana)}");
            Console.WriteLine();


            //Assert.Equal(Katakana, KanaConverter.Convert(Hiragana, KanaOption.HiraganaToKatakana));
            Console.WriteLine($"========================HiraganaToKatakana=======================");
            Console.WriteLine($"old Hiragana:{Hiragana}");
            Console.WriteLine($"converted Katakana:{KanaConverter.Convert(Hiragana, KanaOption.HiraganaToKatakana)}");
            Console.WriteLine();



            //Assert.Equal(HiraganaOdoriji, KanaConverter.Convert(KatakanaOdoriji, KanaOption.KatakanaToHiragana));
            Console.WriteLine($"========================KatakanaToHiragana=======================");
            Console.WriteLine($"old KatakanaOdoriji:{KatakanaOdoriji}");
            Console.WriteLine($"converted HiraganaOdoriji:{KanaConverter.Convert(KatakanaOdoriji, KanaOption.KatakanaToHiragana)}");
            Console.WriteLine();




            //Assert.Equal(KatakanaOdoriji, KanaConverter.Convert(HiraganaOdoriji, KanaOption.HiraganaToKatakana));
            Console.WriteLine($"========================HiraganaToKatakana=======================");
            Console.WriteLine($"old HiraganaOdoriji:{HiraganaOdoriji}");
            Console.WriteLine($"converted KatakanaOdoriji:{KanaConverter.Convert(HiraganaOdoriji, KanaOption.HiraganaToKatakana)}");
            Console.WriteLine();
            Console.ReadLine();


        }
    }
    public static class StringFullHalfExtand
    {
        private const uint LOCALE_SYSTEM_DEFAULT = 0x0800;
        private const uint LCMAP_HALFWIDTH = 0x00400000;
        private const uint LCMAP_FULLWIDTH = 0x00800000;
        public static string ToHalfWidth(this string fullWidth)
        {
            StringBuilder sb = new StringBuilder(256);
            LCMapString(LOCALE_SYSTEM_DEFAULT, LCMAP_HALFWIDTH, fullWidth, -1, sb, sb.Capacity);
            return sb.ToString();
        }

        public static string ToFullWidth(this string halfWidth)
        {
            StringBuilder sb = new StringBuilder(256);
            LCMapString(LOCALE_SYSTEM_DEFAULT, LCMAP_FULLWIDTH, halfWidth, -1, sb, sb.Capacity);
            return sb.ToString();
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int LCMapString(uint Locale, uint dwMapFlags, string lpSrcStr, int cchSrc, StringBuilder lpDestStr, int cchDest);
    }
}
