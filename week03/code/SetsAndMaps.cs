using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// Problem 1: Find Pairs
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        var seen = new HashSet<string>();
        var results = new List<string>();

        foreach (var word in words)
        {
            if (word.Length != 2 || word[0] == word[1])
            {
                continue;
            }

            string reversed = $"{word[1]}{word[0]}";

            if (seen.Contains(reversed))
            {
                results.Add($"{reversed} & {word}");
            }
            else
            {
                seen.Add(word);
            }
        }

        return results.ToArray();
    }

    /// <summary>
    /// Problem 2: Degree Summary
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        if (!File.Exists(filename))
        {
            return degrees;
        }

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(',');
            if (fields.Length >= 4)
            {
                string degree = fields[3].Trim();
                if (degrees.ContainsKey(degree))
                {
                    degrees[degree]++;
                }
                else
                {
                    degrees[degree] = 1;
                }
            }
        }

        return degrees;
    }

    /// <summary>
    /// Problem 3: Anagrams
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        string clean1 = word1.Replace(" ", "").ToLower();
        string clean2 = word2.Replace(" ", "").ToLower();

        if (clean1.Length != clean2.Length)
        {
            return false;
        }

        var letterCounts = new Dictionary<char, int>();

        foreach (char c in clean1)
        {
            if (letterCounts.ContainsKey(c))
            {
                letterCounts[c]++;
            }
            else
            {
                letterCounts[c] = 1;
            }
        }

        foreach (char c in clean2)
        {
            if (!letterCounts.ContainsKey(c) || letterCounts[c] == 0)
            {
                return false;
            }
            letterCounts[c]--;
        }

        return true;
    }

    /// <summary>
    /// Problem 5: Earthquake JSON Data
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        
        try
        {
            using var client = new HttpClient();
            using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            using var response = client.Send(getRequestMessage);
            using var jsonStream = response.Content.ReadAsStream();
            using var reader = new StreamReader(jsonStream);
            var json = reader.ReadToEnd();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

            var summaries = new List<string>();

            if (featureCollection?.Features != null)
            {
                foreach (var feature in featureCollection.Features)
                {
                    if (feature?.Properties != null)
                    {
                        string place = feature.Properties.Place ?? "Unknown Location";
                        double? mag = feature.Properties.Mag;

                        summaries.Add($"{place} - Mag {mag}");
                    }
                }
            }

            return summaries.ToArray();
        }
        catch
        {
            return Array.Empty<string>();
        }
    }
}