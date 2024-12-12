﻿using WildPath.Abstractions;

namespace WildPath.Strategies;

internal class ParentsSegmentStrategy : ISegmentStrategy
{
    private readonly string _segment;
    private readonly IFileSystem _fileSystem;

    public ParentsSegmentStrategy(string segment, IFileSystem fileSystem)
    {
        if (!string.Equals(segment, "...", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("ParentsSegmentStrategy can only be used with '...' segment", nameof(segment));
        }

        _segment = segment;
        _fileSystem = fileSystem;
    }

    public bool Matches(string path) => true;

    public IEnumerable<string> Evaluate(string currentDirectory, IPathEvaluatorSegment? child, CancellationToken token = default)
    {
        while (currentDirectory != null && !token.IsCancellationRequested)
        {
            if (child == null)
            {
                yield return currentDirectory;
                continue;
            }

            foreach (var result in child.Evaluate(currentDirectory, token))
            {
                yield return result;
            }

            currentDirectory = _fileSystem.GetDirectoryName(currentDirectory);
        }
    }
}