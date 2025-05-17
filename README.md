# Web Crawler for IITR Domain

A simple C# web crawler that performs a breadth-first search on the IIT Roorkee website domain, collecting and displaying all discovered URLs that contain "iitr".

## Overview

This application starts at the IIT Roorkee homepage (https://iitr.ac.in/) and systematically explores all linked pages within the domain, following a breadth-first search approach. It collects all URLs that contain the string "iitr" and prints them to the console.

## Features

- Breadth-first crawling strategy to explore web pages
- URL deduplication to avoid processing the same URL multiple times
- HTML parsing using the HtmlAgilityPack library
- HTTP request handling with automatic redirect following
- SSL certificate validation bypassing (for development purposes)

## Requirements

- .NET Framework or .NET Core
- HtmlAgilityPack package

## Project Structure

- **Program.cs**: Contains the main entry point and executes the crawler
- **RunQueue.cs**: Implements the breadth-first search algorithm for web crawling
- **WebCrawler.cs**: Handles HTTP requests and HTML parsing to extract links

## How It Works

1. The application initializes a queue with the starting URL (https://iitr.ac.in/)
2. For each URL in the queue:
   - The URL is fetched using an HTTP GET request
   - The HTML content is parsed to extract all links (`<a href>` tags)
   - Links containing "iitr" are filtered and added to the queue if they haven't been processed before
3. The process continues until the queue is empty (all discoverable links have been processed)
4. All discovered URLs are printed to the console

## Code Examples

### Initializing the Crawler

```csharp
var run = new RunQueue("https://iitr.ac.in/");
await run.bfs();
```

### Processing URLs

```csharp
// Queue initialization
queue = new Queue<string>();
urls = new HashSet<string>();
queue.Enqueue(url);

// BFS implementation
while (queue.Count > 0) {
    await Task.Delay(1000);
    var top = queue.Dequeue();
    Console.WriteLine(top);
    var list = await w.Driver(top);
    // Process discovered links...
}
```

## NOTES

- You could pick up the data of the website by using the html tree root from all `p` tags `hi` tags and `img` tags my web crawler is a simple graph walker maybe i can add page rank functionality to it as well. 

## Future Improvements

- walk in graph so page rankings
