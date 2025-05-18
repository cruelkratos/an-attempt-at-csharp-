using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ppo_asla {
    class WebCrawler {
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
}
