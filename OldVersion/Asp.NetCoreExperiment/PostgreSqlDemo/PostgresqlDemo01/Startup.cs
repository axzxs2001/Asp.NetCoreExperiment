using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Dapper;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Cjk;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.TokenAttributes;
using Lucene.Net.Analysis.Util;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PostgresqlDemo01
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           // var cx = new CXDic();
           // cx.Write();
            //services.AddSingleton(cx);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
    public class CXDic
    {
        Dictionary<char, string> _cxDic;       
        public CXDic()
        {            
            _cxDic = new Dictionary<char, string>();           
            _cxDic.Add('A', "獘");
            _cxDic.Add('B', "獙");
            _cxDic.Add('C', "獚");
            _cxDic.Add('D', "獛");
            _cxDic.Add('E', "獜");
            _cxDic.Add('F', "獝");
            _cxDic.Add('G', "獞");
            _cxDic.Add('H', "獟");
            _cxDic.Add('I', "獠");
            _cxDic.Add('J', "獡");
            _cxDic.Add('K', "獢");
            _cxDic.Add('L', "獣");
            _cxDic.Add('M', "獤");
            _cxDic.Add('N', "獥");
            _cxDic.Add('O', "獦");
            _cxDic.Add('P', "獧");
            _cxDic.Add('Q', "獩");
            _cxDic.Add('R', "狯");
            _cxDic.Add('S', "猃");
            _cxDic.Add('T', "獬");
            _cxDic.Add('U', "獭");
            _cxDic.Add('V', "狝");
            _cxDic.Add('W', "獯");
            _cxDic.Add('X', "獗");
            _cxDic.Add('Y', "獱");
            _cxDic.Add('Z', "獳");
            _cxDic.Add('1', "獴");
            _cxDic.Add('2', "獶");
            _cxDic.Add('3', "獹");
            _cxDic.Add('4', "獽");
            _cxDic.Add('5', "獾");
            _cxDic.Add('6', "獿");
            _cxDic.Add('7', "猡");
            _cxDic.Add('8', "玁");
            _cxDic.Add('9', "玂");
            _cxDic.Add('0', "玃");  
        }
        RAMDirectory ramDir;
        public void Write()
        {
            //var indexDir = new System.IO.DirectoryInfo(System.IO.Directory.GetCurrentDirectory() + "/abc");
            //var returnIndexDir = FSDirectory.Open(indexDir);
            ramDir = new RAMDirectory();

            //var c = new CharArraySet(LuceneVersion.LUCENE_48, 1, true);
            //c.Add(' ');
            //var iwc = new IndexWriterConfig(LuceneVersion.LUCENE_48,
            //        new StandardAnalyzer(LuceneVersion.LUCENE_48, c));
            var indexwRiteCfg = new IndexWriterConfig(LuceneVersion.LUCENE_48,
                    new ClassicAnalyzer(LuceneVersion.LUCENE_48));
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
                    var newpy = new StringBuilder();
                    for (int i = 0; i < fpy.Length; i++)
                    {
                        if (_cxDic.ContainsKey(fpy[i]))
                        {
                            newpy.Append(_cxDic[fpy[i]]);
                        }
                        else
                        {
                            newpy.Append(fpy[i]);
                        }
                    }
                    doc.Add(new Field("fpy", newpy.ToString(), new FieldType() { IsIndexed = true, IsStored = false, }));

                    doc.Add(new Field("py", fpy, new FieldType() { IsIndexed = false, IsStored = true, }));

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
            name = name.ToUpper();
            var newpy = new StringBuilder();
            for (int i = 0; i < name.Length; i++)
            {
                if (_cxDic.ContainsKey(name[i]))
                {
                    newpy.Append(_cxDic[name[i]]);
                }
                else
                {
                    newpy.Append(name[i]);
                }
            }
            name = newpy.ToString();
            var list = new List<dynamic>();
            //var c = new CharArraySet(LuceneVersion.LUCENE_48, 1, true);
            //c.Add(' ');
            //QueryParser queryParser = new MultiFieldQueryParser(LuceneVersion.LUCENE_48,
            //new[] { "fname", "fpy" }, new StandardAnalyzer(LuceneVersion.LUCENE_48, c));
            QueryParser queryParser = new MultiFieldQueryParser(LuceneVersion.LUCENE_48,
            new[] { "fname", "fpy" }, new ClassicAnalyzer(LuceneVersion.LUCENE_48));
            var query = queryParser.Parse(name);

            // Execute the search with a fresh indexSearcher
            //var searcherManager = new SearcherManager(writer, true, null);
            // searcherManager.MaybeRefreshBlocking();

            var searcher = new IndexSearcher(DirectoryReader.Open(ramDir));// searcherManager.Acquire();
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
                        Py = doc.GetField("py")?.GetStringValue(),
                        result.Score,
                        
                    });
                    // Console.WriteLine($"{doc.GetField("fmefeeitemid")?.GetStringValue()}   ***   {doc.GetField("fname")?.GetStringValue()}   ***   {doc.GetField("fpy")?.GetStringValue()}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                //searcherManager.Release(searcher);
                searcher = null; // Don't use searcher after this point!
            }
            return list;
        }
    }
    public sealed class MyAnalyzer : Analyzer
    {
        private readonly LuceneVersion matchVersion;



        public MyAnalyzer(LuceneVersion matchVersion)
        {
            this.matchVersion = matchVersion;
        }
        protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
        {
            var ts = new MyTokenizer(LuceneVersion.LUCENE_48, reader);
            var stream = new MyFilter(matchVersion, ts);
            return new TokenStreamComponents(ts, stream);
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

