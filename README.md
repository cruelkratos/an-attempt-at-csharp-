
# Web Crawler and PageRank for IITR Domain

A simple C# web crawler that performs a **depth-limited DFS** on the IIT Roorkee website domain, building a graph of internal links and calculating PageRank scores to identify the most important pages.


## Features

- Depth-limited DFS crawling strategy
- URL deduplication to avoid revisiting pages
- HTML parsing using the HtmlAgilityPack library
- Graph construction based on internal hyperlinks
- Simplified PageRank implementation
- HTTP request handling with automatic redirect following

## Requirements

- .NET Framework or .NET Core
- HtmlAgilityPack NuGet package

## Project Structure

- **Program.cs**: Entry point of the application; triggers crawl and PageRank computation
- **RunQueue.cs**: Implements DFS and builds the graph OR just crawl the entire site with BFS and extract the links
- **WebCrawler.cs**: Handles HTTP requests and extracts links from HTML

## Code Examples

### Initializing the Crawler

```csharp
var run = new RunQueue("https://iitr.ac.in/");
await run.dfs("https://iitr.ac.in/", 5);
```

### Computing PageRank

```csharp
var ranks = run.PageRank();
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
