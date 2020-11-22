using System;

namespace CrawlerFamiliarization
{
    partial class Program
    {
        static void Main()
        {
            WikiCrawler wc = new WikiCrawler();
            wc.crawl();
        }
    }
}
