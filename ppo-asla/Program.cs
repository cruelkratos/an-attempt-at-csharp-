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
