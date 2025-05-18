
# Web Crawler and PageRank for IITR Domain

A simple C# web crawler that performs a **depth-limited DFS** on the IIT Roorkee website domain, building a graph of internal links and calculating PageRank scores to identify the most important pages.

## Overview

This application starts at the IIT Roorkee homepage (https://iitr.ac.in/) and systematically explores linked pages within the domain using a **depth-limited depth-first search** (DFS). It collects all URLs that contain the string `"iitr"` and builds a directed graph from the discovered links. A simplified **PageRank algorithm** is then applied to rank the importance of each page.

## Features

- Depth-limited DFS crawling strategy
- URL deduplication to avoid revisiting pages
- HTML parsing using the HtmlAgilityPack library
- Graph construction based on internal hyperlinks
- Simplified PageRank implementation
- HTTP request handling with automatic redirect following
- SSL certificate validation bypassing (for development purposes)

## Requirements

- .NET Framework or .NET Core
- HtmlAgilityPack NuGet package

## Project Structure

- **Program.cs**: Entry point of the application; triggers crawl and PageRank computation
- **RunQueue.cs**: Implements DFS and builds the graph OR just crawl the entire site with BFS and extract the links
- **WebCrawler.cs**: Handles HTTP requests and extracts links from HTML

## How It Works

1. The application starts with the seed URL `https://iitr.ac.in/`
2. A **depth-limited DFS** (max depth = 5) explores all internal links containing `"iitr.ac.in"`
3. A **directed graph** is constructed with URLs as nodes and hyperlinks as edges
4. A simplified PageRank algorithm runs on the graph for a fixed number of iterations
5. Final PageRank scores are displayed in descending order

## Code Examples

### Initializing the Crawler

```csharp
var run = new RunQueue("https://iitr.ac.in/");
await run.dfs("https://iitr.ac.in/", 5);
```

### Computing PageRank

```csharp
var ranks = run.PageRank();

foreach (var pair in ranks.OrderByDescending(p => p.Value).Take(10)) {
    Console.WriteLine($"{pair.Key} => {pair.Value:F10}");
}
```

## Sample PageRank Output

| URL                                                                                  | PageRank Score     |
|---------------------------------------------------------------------------------------|--------------------|
| [https://newwebmail.iitr.ac.in/](https://newwebmail.iitr.ac.in/)                     | 0.0014530104       |
| [https://www.iitr.ac.in/](https://www.iitr.ac.in/)                                   | 0.0011748830       |
| [https://iitr.ac.in/Departments/index.html](https://iitr.ac.in/Departments/index.html) | 0.0009834198       |
| [https://iitr.ac.in/Institute/Contact%2520Us.html](https://iitr.ac.in/Institute/Contact%2520Us.html) | 0.0007755391       |

## Notes

- This is a minimal crawler focused only on internal links containing `"iitr.ac.in"`
- SSL validation is bypassed for simplicity — not recommended for production
- More accurate PageRank results require a larger crawl and more iterations

## Future Improvements

- Add support for multi-threaded or parallel crawling
- Expand to other domains or allow user-defined domain constraints
- Improve HTML parsing to extract page content, not just links
- Enhance ranking logic with backlink weight or content-based features
