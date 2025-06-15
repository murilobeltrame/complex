using Complex.ConsoleApp;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

var directory = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
var files = Directory.GetFiles(directory, "*.cs", SearchOption.AllDirectories);

foreach (var file in files)
{
    var code = File.ReadAllText(file);
    var tree = CSharpSyntaxTree.ParseText(code);
    var root = tree.GetRoot();

    var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
    foreach (var method in methods)
    {
        var complexity = CodeMetricsCalculator.CalculateComplexity(method.ToFullString());
        Console.WriteLine($"{file}::{method.Identifier} - Cyclomatic Complexity: {complexity}");
    }
}
