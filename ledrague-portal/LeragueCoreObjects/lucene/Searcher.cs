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
using System.Text.RegularExpressions;

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
                query.Add(new PrefixQuery(term), Occur.SHOULD);
            }

            return query;
        }

        private BooleanQuery getPrefixQuery(String pTerm, String pField, Analyzer pAnalyzer)
        {
            BooleanQuery termQuery = new BooleanQuery();
            termQuery.Add(getPrefixQuery(pField, 15, pTerm, pAnalyzer), Occur.SHOULD);

            return termQuery;
        }

        public List<int> searchTitles(String pTerm)
        {
            return search(pTerm, Constants.TITLE_FIELD, Constants.SONG_ID_FIELD);
        }

        public List<int> searchArtist(String pTerm)
        {
            return search(pTerm, Constants.ARTIST_FIELD, Constants.ARTIST_ID_FIELD);
        }

        public List<int> searchCategories(String pTerm) {
            return search(pTerm, Constants.CATEGORY_FIELD, Constants.CATEGORY_ID_FIELD);
        }

        public List<int> search(String pTerm, String pField, String pIdField)
        {
            Analyzer analyzer = new ASCIIFoldingAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

            String filteredTerm = Regex.Replace(pTerm, "!@#$%?&*()\\'\"", " ");
            // Perform a search
            var searcher = getSearcher();
            BooleanQuery finalQuery = new BooleanQuery();

            finalQuery.Add(getPrefixQuery(filteredTerm, pField, analyzer), Occur.SHOULD);
            ScoreDoc[] hits = searcher.Search(finalQuery, null, int.MaxValue, Sort.RELEVANCE).ScoreDocs;

            List<int> results = new List<int>();
            foreach (ScoreDoc hit in hits)
            {
                var document = searcher.IndexReader.Document(hit.Doc);
                results.Add(int.Parse(document.Get(pIdField)));
            }

            return results;
        }

        public class SongItem
        {
            public Int32 id;
            public String title;
            public Double score;

            public SongItem(Int32 pId, String pTitle, Double pScore)
            {
                id = pId;
                title = pTitle;
                score = pScore;
            } 

            public Boolean equals(SongItem pItem)
            {
                if (this.id == pItem.id)
                {
                    return true;
                }
                return false;
            }


        }

        public List<SongItem> KeywordSearch(string term)
        {
            Analyzer analyzer = new ASCIIFoldingAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

            // Perform a search
            var searcher = getSearcher();
            var hits_limit = 50;

            BooleanQuery finalQuery = new BooleanQuery();

            finalQuery.Add(getPrefixQueryAllFields(term, analyzer), Occur.SHOULD);
            searcher.SetDefaultFieldSortScoring(true, true);

            ScoreDoc[] hits = searcher.Search(finalQuery, null, hits_limit, Sort.RELEVANCE).ScoreDocs;

            List<ScoreDoc> scoreDocs = new List<ScoreDoc>();
            scoreDocs.AddRange(hits);
            if (hits.Length < hits_limit)
            {
                ScoreDoc[] fuzzyHits = searcher.Search(getFuzzyQuery(term, analyzer), null, hits_limit, Sort.RELEVANCE).ScoreDocs;
                scoreDocs.AddRange(fuzzyHits);
            }

            List<SongItem> searchResults = new List<SongItem>();
            foreach (ScoreDoc hit in scoreDocs)
            {
                var document = searcher.IndexReader.Document(hit.Doc);
                var item = new SongItem(Int32.Parse(document.Get(Constants.SONG_ID_FIELD)),
                    document.Get(Constants.TITLE_FIELD) + " par " + document.Get(Constants.ARTIST_FIELD),
                    hit.Score);
//                if (!searchResults.ContainsKey(item.id)) {
                    searchResults.Add(item);
//                }
            }
            searchResults.Sort((x, y) => y.score.CompareTo(x.score));
            return searchResults;
        }

        private BooleanQuery getPrefixQueryAllFields(string pField, float pBoost, string pSearchQuery, Analyzer pAnalyzer)
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

        private BooleanQuery getFuzzyQuery(string pField, string searchTerms, Analyzer pAnalyzer)
        {
            BooleanQuery resultQuery = new BooleanQuery();

            String[] terms = searchTerms.Split(" ");
            foreach (string term in terms)
            {
                resultQuery.Add(new FuzzyQuery(new Term(pField, term.ToLower())), Occur.SHOULD);
            }
            return resultQuery;
        }

        private BooleanQuery getPrefixQueryAllFields(string searchQuery, Analyzer analyzer)
        {
            BooleanQuery termQuery = new BooleanQuery();
            termQuery.Add(getPrefixQuery(Constants.TITLE_FIELD, 15, searchQuery, analyzer), Occur.SHOULD);
            termQuery.Add(getPrefixQuery(Constants.ARTIST_FIELD, 5, searchQuery, analyzer), Occur.SHOULD);
            termQuery.Add(getPrefixQuery(Constants.CATEGORY_FIELD, 1, searchQuery, analyzer), Occur.SHOULD);

            return termQuery;
        }

        private BooleanQuery getFuzzyQuery(string searchTerms, Analyzer pAnalyzer)
        {
            BooleanQuery resultQuery = new BooleanQuery();

            resultQuery.Add(getFuzzyQuery(Constants.ARTIST_FIELD, searchTerms, pAnalyzer), Occur.SHOULD);
            resultQuery.Add(getFuzzyQuery(Constants.TITLE_FIELD, searchTerms, pAnalyzer), Occur.SHOULD);
            resultQuery.Add(getFuzzyQuery(Constants.CATEGORY_FIELD, searchTerms, pAnalyzer), Occur.SHOULD);

            return resultQuery;
        }
    }
}
