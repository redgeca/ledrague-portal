﻿using LeDragueCoreObjects.Karaoke;
using Lucene.Net.Documents;
using Lucene.Net.Store;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using LeDragueCoreObjects.misc;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using KaraokeImport.analyzers;
using KaraokeImport.dbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KaraokeImport
{
    class Program
    {
        static List<Category> getCategories(String pBaseUrl, int pPageNumber)
        {
            HttpClient categoriesClient = new HttpClient();
            categoriesClient.BaseAddress = new Uri(pBaseUrl + "categories");
            categoriesClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage categoriesResponse = categoriesClient.GetAsync("?fields=name&page=" + pPageNumber).Result;

            if (categoriesResponse.IsSuccessStatusCode)
            {
                var dataObjects = categoriesResponse.Content.ReadAsStringAsync().Result;

                JObject converted = JsonConvert.DeserializeObject<JObject>(dataObjects);
                JValue foundObjects = (JValue)converted["found"];
                JArray categories = (JArray)converted["categories"];

                return categories.ToObject<List<Category>>();
            }

            // In case of error, return an empty list...
            return new List<Category>();
        }

        static List<Song> getSongs(String pBaseUrl, int pPageNumber)
        {
            HttpClient songsClient = new HttpClient();
            songsClient.BaseAddress = new Uri(pBaseUrl + "posts");
            songsClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage songsResponse = songsClient.GetAsync("?fields=title,tags,categories,content&number=100&page=" + pPageNumber).Result;

            if (songsResponse.IsSuccessStatusCode)
            {
                var dataObjects = songsResponse.Content.ReadAsStringAsync().Result;

                JObject converted = JsonConvert.DeserializeObject<JObject>(dataObjects);
                JValue foundObjects = (JValue)converted["found"];
                JArray songs = (JArray)converted["posts"];

                return songs.ToObject<List<Song>>();
            }

            // In case of error, return an empty list...
            return new List<Song>();
        }


        static void Main(string[] args)
        {
            using (var db = new ApplicationDbContext())
            {
                /*
                db.Database.ExecuteSqlCommand("delete from KaraokeSongs");
                db.Database.ExecuteSqlCommand("delete from KaraokeCategories");
                db.Database.ExecuteSqlCommand("delete from KaraokeArtists");

                String repository = "https://public-api.wordpress.com/rest/v1.1/sites/lekaraoke.ca/";
                int pageNumber = 1;

                List<Category> categoryList = getCategories(repository, pageNumber);
                while (categoryList.Count > 0)
                {
                    pageNumber++;
                    foreach (Category category in categoryList)
                    {

                        db.KaraokeCategories.Add(category);
                        
                        Console.WriteLine("Category " + category.Name);
                    }
                    db.SaveChanges();

                    categoryList = getCategories(repository, pageNumber);
                }

                pageNumber = 1;
                List<Song> songList = getSongs(repository, pageNumber);
                while (songList.Count > 0)
                {
                    pageNumber++; // = 123456789;
                    foreach (Song song in songList)
                    {
                        String artistName = HttpUtility.HtmlDecode(song.ArtistName);
                        String categoryName = HttpUtility.HtmlDecode(song.CategoryName);
                        String content = HttpUtility.HtmlDecode(song.Content);

                        try
                        {
                            Category category = (Category)db.KaraokeCategories.Where(c => c.Name == categoryName).First();
                            song.Category = category;
                        }
                        catch (InvalidOperationException ce)
                        {
                            // Got an Invalid category, Add it to 'None'
                            try
                            {
                                Category category = (Category)db.KaraokeCategories.Where(c => c.Name == "Aucune").First();
                                song.Category = category;
                            }
                            catch (InvalidOperationException e)
                            {
                                Category newCategory = new Category();
                                newCategory.Name = "Aucune";
                                song.Category = newCategory;
                                db.SaveChanges();
                            }
                        }

                        song.Title = HttpUtility.HtmlDecode(song.Title);
                        if (artistName == null)
                        {
                            artistName = song.Title.Substring(song.Title.IndexOf(" par ") + 5);
                            song.Title = song.Title.Substring(0, song.Title.IndexOf(" par "));
                        }

                        try
                        {
                            Artist artist = (Artist) db.KaraokeArtists.Where(a => a.Name == artistName).First();
                            song.Artist = artist;
                        }
                        catch (InvalidOperationException e)
                        {
                            Artist newArtist = new Artist();
                            newArtist.Name = artistName;
                            db.KaraokeArtists.Add(newArtist);
                            song.Artist = newArtist;
                            db.SaveChanges();
                        }

                        db.KaraokeSongs.Add(song);
                        Console.WriteLine("Song " + song.Title + " from " + song.Artist.Name);
                    }
                    db.SaveChanges();

                    songList = getSongs(repository, pageNumber);
                }
                Console.WriteLine("DONE");
                */

                Directory indexDirectory = FSDirectory.Open(Constants.INDEX_FOLDER);

                Analyzer analyzer = new ASCIIFoldingAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

                var writer = new IndexWriter(indexDirectory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED);

                // First, we clear index so we won't get duplicates...
                writer.DeleteAll();

                int x = 0;
                foreach (Song song in db.KaraokeSongs.Include(s => s.Artist).Include(s => s.Category).ToList())
                {
                    writer.AddDocument(getSongDocument(song.Id.ToString(), song.Title,
                        song.Artist == null ? "" : song.Artist.Name,
                        song.Category == null ? "" : song.Category.Name));
                    x++;
                    Console.WriteLine("Song " + song.Title + " by " + song.Artist.Name);
                }
                Console.WriteLine(x + " songs loaded ");
                writer.Dispose();
            }
        }

        private static Document getSongDocument(String pId, String pName, String pArtist, String pCategory)
        {
            Document luceneDocument = new Document();
            Field idField = new Field("Id", pId, Field.Store.YES, Field.Index.ANALYZED);
            Field titleField = new Field("Title", pName, Field.Store.YES, Field.Index.ANALYZED);
            Field categoryField = new Field("Category", pCategory == null ? "" : pCategory, Field.Store.YES, Field.Index.ANALYZED);
            Field artistField = new Field("Artist", pArtist == null ? "" : pArtist, Field.Store.YES, Field.Index.ANALYZED);

            titleField.Boost = 5;
            artistField.Boost = 3;
            categoryField.Boost = 1;

            luceneDocument.Add(idField);
            luceneDocument.Add(titleField);
            luceneDocument.Add(categoryField);
            luceneDocument.Add(artistField);

            return luceneDocument;
        }
    }
}
