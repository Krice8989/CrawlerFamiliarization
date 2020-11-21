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
            for (int i = 0; i < _frontier.Count; i ++)
            {
                _doc = _web.Load(_frontier[i]);
                foreach (HtmlNode node in _doc.DocumentNode.SelectNodes("//a"))
                {
                    if (node?.Attributes?["href"].Value == null || string.IsNullOrEmpty(node.Attributes["href"].Value.ToString()) || !node.Attributes["href"].Value.ToString().Contains("http"))
                    {
                        continue;
                    }
                    _frontier.Add(new Uri(node.Attributes["href"].Value.ToString()));
                    Console.WriteLine(_frontier.Count);
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n\n {_frontier.Count} Links found.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
