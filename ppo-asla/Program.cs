using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ppo_asla
{
    class RunQueue {
        Queue<string> queue;
        WebCrawler w;
        public HashSet<string> urls;
        public RunQueue(string url) {
            queue = new Queue<string>();
            w = new WebCrawler();
            urls = new HashSet<string>();
            queue.Enqueue(url);
        }
        public async Task bfs() {
            while (queue.Count > 0) {
                await Task.Delay(1000);
                var top = queue.Dequeue();
                Console.WriteLine(top);
                var list = await w.Driver(top);
                if ( list.Count == 0 || list == null) {
                    continue;
                }
                foreach(var l in list) {
                    if (l != null && !urls.Contains(l)) {
                    queue.Enqueue(l);
                    urls.Add(l);
                    }
                }
            }
        }
    }
    class WebCrawler{
        public async Task<List<string>> Driver(string url) {
            if (url == null) {
                List<string> l = new List<string>();
                return l;
            }
            HttpClientHandler handler = new HttpClientHandler {
                AllowAutoRedirect = true, // Follow redirects (301, 302, etc.)
                MaxAutomaticRedirections = 5, // Prevent infinite redirect loops
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true // Ignore SSL errors (not recommended for production)
            };
            HttpClient client = new HttpClient(handler);
            List<string> result = new List<string>();
            string html;
            try {
                html = await client.GetStringAsync(url);
            }
            catch (Exception e) {
                //Console.WriteLine(e);
                Console.WriteLine("BTTTTTTTTTTTTTTTTTTTT");
                return result;
            }
            var htmldoc = new HtmlAgilityPack.HtmlDocument();
            htmldoc.LoadHtml(html);
            var links = htmldoc.DocumentNode.SelectNodes("//a[@href]"); // lise of nodes having links
            
            if (links == null) {
                return result;
            }


            try {
                foreach (var link in links) {
                    string hrefValue = link.GetAttributeValue("href", "");
                    if (hrefValue != null && hrefValue.Contains("iitr")) {
                        // don't deviate from iitr links for now!!
                        result.Add(hrefValue);
                    }
                }
            }
            catch (Exception ex) { 
                //Console.WriteLine(ex.Message);
                Console.WriteLine("BT");
            }
            return result;
        }
    }
    
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("hello");
            var run = new RunQueue("https://iitr.ac.in/");
            await run.bfs();
            foreach(var link in run.urls) {
                Console.WriteLine(link);
            }
        }
    }
}
