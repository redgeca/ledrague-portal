using KaraokeClient.lucene.analyzers;
using LeDragueCoreObjects.misc;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeDragueCoreObjects.lucene
{
    public class Searcher
    {
        private IndexSearcher getSearcher()
        {
            String indexLocation = Constants.INDEX_FOLDER;
            String directory = System.IO.Directory.GetCurrentDirectory();

            Directory indexDirectory = FSDirectory.Open(indexLocation);

            // Perform a search
            return new IndexSearcher(indexDirectory, false);
        }

        private BooleanQuery getPrefixQuery(string pField, float pBoost, string pSearchQuery, Analyzer pAnalyzer)
        {
            var escapedTerm = QueryParser.Escape(pSearchQuery);
            var prefixedTerm = String.Concat("\"", escapedTerm, "\"");

            var queryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, pField, pAnalyzer);
            Query query = queryParser.Parse(prefixedTerm);

            ISet<Term> terms = new HashSet<Term>();
            query.ExtractTerms(terms);

            BooleanQuery termQuery = new BooleanQuery();
            termQuery.Add(getQueryFromTerms(terms), Occur.SHOULD);
            termQuery.Boost = pBoost;
            return termQuery;
        }

        private BooleanQuery getQueryFromTerms(ISet<Term> pTerms)
        {

            BooleanQuery query = new BooleanQuery();
            foreach (Term term in pTerms)
            {
                query.Add(new PrefixQuery(term), Occur.MUST);
            }

            return query;
        }

        private BooleanQuery getPrefixQuery(String pTerm, Analyzer pAnalyzer)
        {
            BooleanQuery termQuery = new BooleanQuery();
            termQuery.Add(getPrefixQuery(Constants.TITLE_FIELD, 15, pTerm, pAnalyzer), Occur.SHOULD);

            return termQuery;
        }

        public List<int> searchTitles(String pTerm)
        {
            Analyzer analyzer = new ASCIIFoldingAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

            // Perform a search
            var searcher = getSearcher();
            BooleanQuery finalQuery = new BooleanQuery();

            finalQuery.Add(getPrefixQuery(pTerm, analyzer), Occur.SHOULD);
            ScoreDoc[] hits = searcher.Search(finalQuery, null, int.MaxValue, Sort.RELEVANCE).ScoreDocs;

            List<int> results = new List<int>();
            foreach (ScoreDoc hit in hits)
            {
                var document = searcher.IndexReader.Document(hit.Doc);
                results.Add(int.Parse(document.Get(Constants.ID)));
            }

            return results;
        }
    }
}
