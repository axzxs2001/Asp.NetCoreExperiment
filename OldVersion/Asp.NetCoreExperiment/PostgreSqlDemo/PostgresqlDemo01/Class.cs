using Dapper;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System.Data.SqlClient;
using System.Text;

namespace PostgresqlDemo01
{
    public class ABC
    {
        public void Write(string name)
        {
            var indexDir = new System.IO.DirectoryInfo(System.IO.Directory.GetCurrentDirectory());
            var returnIndexDir = FSDirectory.Open(indexDir);
            var ramDir = new RAMDirectory();
            var iwc = new IndexWriterConfig(LuceneVersion.LUCENE_48,
                    new StandardAnalyzer(LuceneVersion.LUCENE_48));

            using (var writer = new IndexWriter(ramDir, iwc))
            {

                using (var con = new SqlConnection("server=.;database=testdb;uid=sa;pwd=1;"))
                {
                    var list = con.Query<dynamic>("SELECT * FROM t_bx_feeitem");
                    foreach (var item in list)
                    {
                        var doc = new Lucene.Net.Documents.Document {
                            new StringField("fname","item.FName.ToString()",Field.Store.YES)
                        };
                        var fpy = item.FPy == null ? "" : item.FPy?.ToString();
                        doc.Add(new Field("fpy", fpy, new FieldType() { IsIndexed = true, IsStored = true }));
                        writer.AddDocument(doc);
                    }
                    writer.Flush(true, true);
                    writer.Commit();
                }



                var iss = new IndexSearcher(DirectoryReader.Open(ramDir));
                QueryParser queryParser = new MultiFieldQueryParser(LuceneVersion.LUCENE_48,
               new[] { "fname", "fpy" }, new StandardAnalyzer(LuceneVersion.LUCENE_48));
                QueryParser parser = new QueryParser(LuceneVersion.LUCENE_48, "fname", new StandardAnalyzer(LuceneVersion.LUCENE_48));
                Query query = parser.Parse($"{name}");
                var tops = iss.Search(query, 100, Sort.INDEXORDER);
                var f = tops.Fields;

            }



        }
    }
}