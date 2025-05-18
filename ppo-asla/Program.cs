using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ppo_asla
{
    class RunQueue {
        Queue<string> queue;
        WebCrawler w;
        public Dictionary<string, List<string>> graph;
        public HashSet<string> urls;
        string url;
        public RunQueue(string url) {
            this.url = url;
            graph = new Dictionary<string, List<string>>();
            queue = new Queue<string>();
            w = new WebCrawler();
            urls = new HashSet<string>();
            queue.Enqueue(url);
        }
        public async Task bfs() {
            while (queue.Count > 0) {
                await Task.Delay(190); // to avoid rate limit //use ache se 
                var top = queue.Dequeue();
                Console.WriteLine(top); // to see simultaneously
                var list = await this.w.Driver(top);
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
        public async Task dfs(string URL, int depth = 5) {
            if (depth == 0) {
                return;
            }
            urls.Add(URL);
            Console.WriteLine($"{URL}");
            await Task.Delay(190);
            var list = await this.w.Driver(URL);
            if (list.Count == 0 || list == null) {
                return;
            }
            if (!graph.ContainsKey(URL)) {
                graph[URL] = new List<string>();
            }
            foreach (var l in list) {
                if (l != null && l.Contains("iitr.ac.in")) {
                    graph[URL].Add(l);
                }
                if (l != null && !urls.Contains(l)) {
                    await dfs(l, depth - 1);
                }
            }
        }

        public Dictionary<string, double> PageRank(int iterations = 20, double damping = 0.85) {
            var nodes = graph.Keys.ToHashSet();
            foreach (var neighbors in graph.Values)
                foreach (var node in neighbors)
                    nodes.Add(node);

            var N = nodes.Count;
            var pr = nodes.ToDictionary(node => node, node => 1.0 / N);

            for (int it = 0; it < iterations; it++) {
                var newPr = nodes.ToDictionary(node => node, node => (1.0 - damping) / N);

                foreach (var node in graph.Keys) {
                    if (graph[node].Count == 0) continue;

                    double share = pr[node] / graph[node].Count;
                    foreach (var dest in graph[node]) {
                        if (newPr.ContainsKey(dest))
                            newPr[dest] += damping * share;
                    }
                }

                pr = newPr;
            }

            return pr;
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
                Console.WriteLine('\n');
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
                    if (hrefValue != null && hrefValue.Contains("iitr.ac.in")) {
                        // don't deviate from iitr links for now!!
                        result.Add(hrefValue);
                    }
                }
            }
            catch (Exception ex) { 
                //Console.WriteLine(ex.Message);
                Console.WriteLine('\n');
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
            await run.dfs("https://iitr.ac.in/");
            //foreach(var link in run.urls) {
            //    Console.WriteLine(link);
            //}
            var matrix = run.PageRank();
            foreach (var p in matrix) {
                Console.Write($"{p.Key} => {p.Value}");
            }
        }
    }
}
