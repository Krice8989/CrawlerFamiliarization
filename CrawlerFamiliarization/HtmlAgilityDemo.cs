using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace CrawlerFamiliarization
{
    public class HtmlAgility
    {
        private List<Uri> _frontier = new List<Uri>();
        private HtmlWeb _web = new HtmlWeb();
        private HtmlDocument _doc;
        public void Crawl()
        {
            BuildInitialFrontier();
            ExpandFront();
        }

        private void BuildInitialFrontier()
        {
            Console.Write("Input Initial Seed: ");
            var input = Console.ReadLine();

            while (string.IsNullOrEmpty(input))
            {
                Console.Write("Invalid Input...\n\nInput Initial Seed:");
                input = Console.ReadLine();
            }

            Uri uri = new Uri(input);

            _doc = _web.Load(uri);

            foreach (HtmlNode node in _doc.DocumentNode.SelectNodes("//a"))
            {
                if (string.IsNullOrEmpty(node.Attributes["href"].Value.ToString()) || !node.Attributes["href"].Value.ToString().Contains("http"))
                {
                    continue;
                }
                _frontier.Add(new Uri(node.Attributes["href"].Value.ToString()));

            }
        }

        private void ExpandFront()
        {
            for (int i = 0; i < _frontier.Count; i++)
            {
                _doc = _web.Load(_frontier[i]);

                if (_doc.DocumentNode.SelectNodes("//a") == null)
                {
                    continue;
                }

                string newlink = _doc.DocumentNode.SelectNodes("//a").ToString();

                foreach (HtmlNode node in _doc.DocumentNode.SelectNodes("//a"))
                {
                    if (node.Attributes["href"]?.Value == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("LINK IGNORED --- CASE: INVALID VALUE. --- LINK: Null");
                        continue;
                    }
                    if (!node.Attributes["href"].Value.ToString().StartsWith("http"))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"LINK IGNORED --- CASE: NO HTTP. --- LINK: {node.Attributes["href"].Value.ToString()}");
                        continue;
                    }
                    if (_frontier.Contains(new Uri(node.Attributes["href"].Value.ToString())))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"LINK IGNORED --- CASE: DUPLICATE --- LINK: {node.Attributes["href"].Value.ToString()}");
                        continue;
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    _frontier.Add(new Uri(node.Attributes["href"].Value.ToString()));
                    Console.WriteLine($"New Link Found: {node.Attributes["href"].Value.ToString()}---Total Links: {_frontier.Count}");
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n/n{_frontier.Count} Links found.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
