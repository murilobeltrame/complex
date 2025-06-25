using CommandLine;

namespace Complex.ConsoleApp;

internal class Options
{
    [Value(0, MetaName = "input", Required = true, HelpText = "Path to the .sln or .csproj file.")]
    public string InputFile { get; set; } = string.Empty;

    [Option('t', "threshold", Required = false, HelpText = "Optional complexity threshold. If set, files exceeding this value will cause a violation.")]
    public int? Threshold { get; set; }
}
