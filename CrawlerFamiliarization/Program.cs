using System;

namespace CrawlerFamiliarization
{
    partial class Program
    {
        static void Main()
        {
            HtmlAgility htmlAgility = new HtmlAgility();
            htmlAgility.Crawl();
        }
    }
}
