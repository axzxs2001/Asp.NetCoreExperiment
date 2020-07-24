using Dapper;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.NGram;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.Util;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace CustomerAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var cx = new CX();
            cx.Write();
            Console.WriteLine("数据加载完成");
            while (true)
            {
                Console.WriteLine("输入查询项目：");
                foreach (var item in cx.Read(Console.ReadLine()))
                {
                    Console.WriteLine($"{item.ID}   ---   {item.Name}  ---  {item.Py}  ---  {item.Score}");
                }
            }
        }
    }

    public class CX
    {
        // Dictionary<char, string> _cxDic;
        public CX()
        {
            //_cxDic = new Dictionary<char, string>();
            //_cxDic.Add('A', "獘");
            //_cxDic.Add('B', "獙");
            //_cxDic.Add('C', "獚");
            //_cxDic.Add('D', "獛");
            //_cxDic.Add('E', "獜");
            //_cxDic.Add('F', "獝");
            //_cxDic.Add('G', "獞");
            //_cxDic.Add('H', "獟");
            //_cxDic.Add('I', "獠");
            //_cxDic.Add('J', "獡");
            //_cxDic.Add('K', "獢");
            //_cxDic.Add('L', "獣");
            //_cxDic.Add('M', "獤");
            //_cxDic.Add('N', "獥");
            //_cxDic.Add('O', "獦");
            //_cxDic.Add('P', "獧");
            //_cxDic.Add('Q', "獩");
            //_cxDic.Add('R', "狯");
            //_cxDic.Add('S', "猃");
            //_cxDic.Add('T', "獬");
            //_cxDic.Add('U', "獭");
            //_cxDic.Add('V', "狝");
            //_cxDic.Add('W', "獯");
            //_cxDic.Add('X', "獗");
            //_cxDic.Add('Y', "獱");
            //_cxDic.Add('Z', "獳");
            //_cxDic.Add('1', "獴");
            //_cxDic.Add('2', "獶");
            //_cxDic.Add('3', "獹");
            //_cxDic.Add('4', "獽");
            //_cxDic.Add('5', "獾");
            //_cxDic.Add('6', "獿");
            //_cxDic.Add('7', "猡");
            //_cxDic.Add('8', "玁");
            //_cxDic.Add('9', "玂");
            //_cxDic.Add('0', "玃");
        }
        RAMDirectory ramDir;
        public void Write()
        {
            ramDir = new RAMDirectory();
            //var indexwRiteCfg = new IndexWriterConfig(LuceneVersion.LUCENE_48,
            //        new ClassicAnalyzer(LuceneVersion.LUCENE_48));
            var indexwRiteCfg = new IndexWriterConfig(LuceneVersion.LUCENE_48,
            new MyAnalyzer(LuceneVersion.LUCENE_48));
            var writer = new IndexWriter(ramDir, indexwRiteCfg);
            using (var con = new SqlConnection("server=.;database=testdb;uid=sa;pwd=1;"))
            {
                var list = con.Query<dynamic>("SELECT FMEFeeItemID, FName,FPy FROM t_bx_feeitem");
                foreach (var item in list)
                {
                    var doc = new Document();
                    var fname = item.FName == null ? "" : item.FName.ToString();
                    doc.Add(new Field("fname", fname, new FieldType() { IsIndexed = true, IsStored = true }));

                    string fpy = item.FPy == null ? "" : item.FPy?.ToString();
                    //var newpy = new StringBuilder();
                    //for (int i = 0; i < fpy.Length; i++)
                    //{
                    //    if (_cxDic.ContainsKey(fpy[i]))
                    //    {
                    //        newpy.Append(_cxDic[fpy[i]]);
                    //    }
                    //    else
                    //    {
                    //        newpy.Append(fpy[i]);
                    //    }
                    //}
                    //doc.Add(new Field("fpy", newpy.ToString(), new FieldType() { IsIndexed = true, IsStored = false, }));
                    doc.Add(new Field("fpy", fpy, new FieldType() { IsIndexed = true, IsStored = true, }));
                    var fmefeeitemid = item.FMEFeeItemID == null ? "" : item.FMEFeeItemID?.ToString();
                    doc.Add(new Field("fmefeeitemid", fmefeeitemid, new FieldType() { IsIndexed = false, IsStored = true }));
                    writer.AddDocument(doc);
                }
                writer.Flush(true, true);
                writer.Commit();
            }
        }
        public List<dynamic> Read(string name)
        {
            //name = name.ToUpper();
            //var newpy = new StringBuilder();
            //for (int i = 0; i < name.Length; i++)
            //{
            //    if (_cxDic.ContainsKey(name[i]))
            //    {
            //        newpy.Append(_cxDic[name[i]]);
            //    }
            //    else
            //    {
            //        newpy.Append(name[i]);
            //    }
            //}
            //name = newpy.ToString();
            var list = new List<dynamic>();
            //QueryParser queryParser = new MultiFieldQueryParser(LuceneVersion.LUCENE_48,
            //new[] { "fname", "fpy" }, new ClassicAnalyzer(LuceneVersion.LUCENE_48));
            QueryParser queryParser = new MultiFieldQueryParser(LuceneVersion.LUCENE_48,
       new[] { "fname", "fpy" }, new MyAnalyzer(LuceneVersion.LUCENE_48));
            var query = queryParser.Parse(name);
            var searcher = new IndexSearcher(DirectoryReader.Open(ramDir));
            try
            {
                var topDocs = searcher.Search(query, 100);
                foreach (var result in topDocs.ScoreDocs)
                {
                    var doc = searcher.Doc(result.Doc);
                    list.Add(new
                    {
                        ID = doc.GetField("fmefeeitemid")?.GetStringValue(),
                        Name = doc.GetField("fname")?.GetStringValue(),
                        Py = doc.GetField("fpy")?.GetStringValue(),
                        result.Score,
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                searcher = null;
            }
            return list;
        }
    }
    public sealed class MyAnalyzer : Analyzer
    {
        CharArraySet stopwordSet;
        private readonly LuceneVersion matchVersion;

        public MyAnalyzer(LuceneVersion matchVersion)
        {
            this.matchVersion = matchVersion;
            this.stopwordSet = new CharArraySet(matchVersion, new List<string>(), true);
        }
        protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
        {
            var ts = new MyCharTokenizer(matchVersion, reader);
            // var stream = new MyFilter(matchVersion, ts);        
            TokenStream stream = new LowerCaseFilter(matchVersion, ts);
            return new TokenStreamComponents(ts, stream);

            // Tokenizer tokenizer = new NGramTokenizer(matchVersion, reader, 1, 2);
            // TokenStream tokenStream = new NGramTokenFilter(matchVersion, tokenizer);
            //// tokenStream = new StopFilter(matchVersion, tokenStream, stopwordSet);
            // return new TokenStreamComponents(tokenizer, tokenStream);
        }
    }

    public class MyCharTokenizer : CharTokenizer
    {
        public MyCharTokenizer(LuceneVersion matchVersion, TextReader reader) : base(matchVersion, reader)
        {

        }
        protected override bool IsTokenChar(int c)
        {
            return true;
        }
    }

    public sealed class MyFilter : TokenFilter
    {
        private readonly LuceneVersion _matchVersion;
        //private readonly ICharTermAttribute termAtt;
        //private readonly IPositionIncrementAttribute posAtt;

        public MyFilter(LuceneVersion version, TokenStream input) : base(input)
        {
            _matchVersion = version;
            //termAtt = AddAttribute<ICharTermAttribute>();
            //posAtt = AddAttribute<IPositionIncrementAttribute>();
        }
        public override bool IncrementToken()
        {
            return true;
        }
    }
    public sealed class MyTokenizer : Tokenizer
    {
        private readonly LuceneVersion _matchVersion;
        TextReader _reader;
        public MyTokenizer(LuceneVersion version, TextReader reader) : base(reader)
        {
            _matchVersion = version;
            _reader = reader;
        }
        public override bool IncrementToken()
        {
            var str = _reader.ReadToEnd();
            return true;
        }
    }
}
