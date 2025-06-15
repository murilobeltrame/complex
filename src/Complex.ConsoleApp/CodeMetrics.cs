using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Complex.ConsoleApp;

internal static class CodeMetricsCalculator
{
    public static int CalculateComplexity(string code)
    {
        var tree = CSharpSyntaxTree.ParseText(code);
        var root = tree.GetRoot();
        var method = root.DescendantNodes().OfType<MethodDeclarationSyntax>().First();

        var complexity = 1; // default path

        // Decision points
        complexity += root.DescendantNodes().OfType<IfStatementSyntax>().Count();
        complexity += root.DescendantNodes().OfType<ForStatementSyntax>().Count();
        complexity += root.DescendantNodes().OfType<ForEachStatementSyntax>().Count();
        complexity += root.DescendantNodes().OfType<WhileStatementSyntax>().Count();
        complexity += root.DescendantNodes().OfType<DoStatementSyntax>().Count();
        complexity += root.DescendantNodes().OfType<CatchClauseSyntax>().Count();
        complexity += root.DescendantNodes().OfType<ConditionalExpressionSyntax>().Count(); // ternary
        complexity += root.DescendantNodes().OfType<CaseSwitchLabelSyntax>().Count();
        complexity += root.DescendantNodes().OfType<DefaultSwitchLabelSyntax>().Count();

        // Logical operators (&&, ||) in conditions
        var conditions = root.DescendantNodes()
            .OfType<ExpressionSyntax>()
            .Where(expr =>
                expr.Parent is
                    IfStatementSyntax or
                    WhileStatementSyntax or
                    DoStatementSyntax or
                    ForStatementSyntax or
                    ConditionalExpressionSyntax);

        complexity += conditions.Sum(CountLogicalOperators);

        return complexity;
    }

    private static int CountLogicalOperators(SyntaxNode node)
    {
        return node.DescendantTokens()
            .Count(token =>
                token.IsKind(SyntaxKind.AmpersandAmpersandToken) ||
                token.IsKind(SyntaxKind.BarBarToken));
    }
}
