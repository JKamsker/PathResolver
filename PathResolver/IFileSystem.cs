namespace PathResolver;

public interface IFileSystem
{
    IEnumerable<string> EnumerateDirectories(string path);
    string? GetDirectoryName(string path);
    char DirectorySeparatorChar { get; }
    string CurrentDirectory { get; }
    string Combine(params string[] paths);
}