using System;
using System.Collections;
using System.Collections.Generic;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it. Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case). If the value of
    /// n <= 0, just return 0. A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // Base case: if n <= 0, return 0
        if (n <= 0)
        {
            return 0;
        }

        // Recursive case: n^2 + SumSquaresRecursive(n - 1)
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // Base case: when word reaches the target size, add to results
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Recursive step: pick each remaining letter and recurse
        for (int i = 0; i < letters.Length; i++)
        {
            char choice = letters[i];
            string remainingLetters = letters.Remove(i, 1);
            PermutationsChoose(results, remainingLetters, size, word + choice);
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Count ways to climb stairs using recursion and memoization.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Initialize the memoization dictionary on first call
        if (remember == null)
        {
            remember = new Dictionary<int, decimal>();
        }

        // Base Cases
        if (s <= 0)
            return 0;
        if (s == 1)
            return 1;
        if (s == 2)
            return 2;
        if (s == 3)
            return 4;

        // Check if result is already memoized
        if (remember.ContainsKey(s))
        {
            return remember[s];
        }

        // Solve using recursion with memoization
        decimal ways = CountWaysToClimb(s - 1, remember) + 
                       CountWaysToClimb(s - 2, remember) + 
                       CountWaysToClimb(s - 3, remember);

        // Store result in memory before returning
        remember[s] = ways;
        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// Using recursion, insert all possible binary strings for a given pattern into the results list.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        int index = pattern.IndexOf('*');

        // Base case: no wildcard left in the pattern
        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        // Replace '*' with '0' and recurse
        string zeroPattern = pattern[..index] + "0" + pattern[(index + 1)..];
        WildcardBinary(zeroPattern, results);

        // Replace '*' with '1' and recurse
        string onePattern = pattern[..index] + "1" + pattern[(index + 1)..];
        WildcardBinary(onePattern, results);
    }

    /// <summary>
    /// #############
    /// # Problem 5 #
    /// #############
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // Initialize currPath on first call
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // Validate current move; return if boundary hit, wall hit, or already visited
        if (!maze.IsValidMove(currPath, x, y))
        {
            return;
        }

        // Add current coordinate to path
        currPath.Add((x, y));

        // Base case: reached the destination square
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
        }
        else
        {
            // Explore in 4 directions: Right, Left, Down, Up
            SolveMaze(results, maze, x + 1, y, currPath);
            SolveMaze(results, maze, x - 1, y, currPath);
            SolveMaze(results, maze, x, y + 1, currPath);
            SolveMaze(results, maze, x, y - 1, currPath);
        }

        // Backtrack: remove coordinate before returning to explore other branches
        currPath.RemoveAt(currPath.Count - 1);
    }
}