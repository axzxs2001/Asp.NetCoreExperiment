using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.NGram;
using Lucene.Net.Analysis.Util;
using Lucene.Net.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace MyAnalysis
{
    public class GSWAnalyzer : Analyzer
    {
        private LuceneVersion matchVersion;

        private int gramSize;

        private CharArraySet stopwordSet;
        public GSWAnalyzer(LuceneVersion version, int gramSize=0)
        {
            this.matchVersion = version;
            this.gramSize = gramSize;
            try
            {

                List<String> stopWords;
                String stopWordPath = "E://Java program//PatentRetrival//resouce//opwords.txt";

                stopWords = readStopwords(stopWordPath);

                this.stopwordSet = new CharArraySet(version, stopWords, true);

            }
            catch (IOException e)
            {
            

            }
        }
        protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
        {
            Tokenizer tokenizer = new NGramTokenizer(matchVersion, reader, 1, 1);
            TokenStream tokenStream = new NGramTokenFilter(matchVersion, tokenizer);
            tokenStream = new StopFilter(matchVersion, tokenStream, stopwordSet);
            return new TokenStreamComponents(tokenizer, tokenStream);
        }

        public List<String> readStopwords(String filepath)
        {
            List<String> stopwords = new List<String>();
            return stopwords;
        }
    }
}
