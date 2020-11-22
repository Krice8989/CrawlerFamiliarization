using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace CrawlerFamiliarization
{
    partial class Program
    {
        public class WikiCrawler
        {
            private List<Uri> _front = new List<Uri>();
            private HtmlWeb _web = new HtmlWeb();
            private HtmlDocument _doc;

            public void crawl()
            {
                BuildInitialFront();
            }

            private void BuildInitialFront()
            {

                Console.Write("Please Enter \"en.wikipedia.org/...\" url.: ");

                var input = Console.ReadLine();

                while (string.IsNullOrEmpty(input) && !input.ToLower().StartsWith("https://en.wikipedia.org/"))
                {
                    Console.WriteLine("Invalid Input");
                    input = Console.ReadLine();
                }

                Uri uri = new Uri(input);
                _doc = _web.Load(uri.AbsoluteUri);

                foreach (HtmlNode node in _doc.DocumentNode.SelectNodes("//a"))
                {

                    if (NodeVerification(node))
                    {
                        Console.WriteLine($"{node.Attributes["href"].Value.ToString()} passed");
                        _front.Add(PathMerger(node.Attributes["href"].Value));
                    }
                }
                foreach(var member in _front)
                {
                    Console.WriteLine(member.AbsoluteUri);
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{_front.Count} unique links found!");
                Console.ForegroundColor = ConsoleColor.White;
            }

            private bool NodeVerification(HtmlNode TargetNode)
            {
                if (TargetNode.Attributes["href"] != null)
                {
                    if (PathVerification(TargetNode.Attributes["href"].Value))
                    {
                        return true;
                    }
                }
                return false;
            }

            private bool PathVerification(string TargetPath)
            {
                if (TargetPath.StartsWith("/wiki"))
                {
                    if (!TargetPath.Contains(":") && NotDuplicateVerification(TargetPath))
                    {
                        return true;
                    }
                }
                return false;
            }
            private bool NotDuplicateVerification(string Target)
            {
                if (_front.Contains(PathMerger(Target)))
                {
                    return false;
                }
                return true;
            }
            private Uri PathMerger(string TailEnd)
            {
                Uri output = new Uri("https://en.wikipedia.org" + TailEnd);
                return output;
            }
        }
    }
}
