namespace Complex.ConsoleApp.Tests;

public class CodeMetricsTests
{
    [Fact]
    public void CalculateComplexityShouldReturnCorrectComplexity()
    {
        // Arrange
        const string code = """
                            public class SampleClass
                            {
                                public void TestMethod(int x, int y)
                                {
                                    if (x > 0) { }
                                    for (int i = 0; i < x; i++) { }
                                    while (x > 1) { }
                                    switch (x) { case 1: break; case 2: break; }
                                }
                            }
                            """;

        // Act
        var complexity = CodeMetricsCalculator.CalculateComplexity(code);

        // Assert
        Assert.Equal(6, complexity);
    }

    [Fact]
    public void CalculateComplexityOfIfWithOrShouldReturnCorrectComplexity()
    {
        // Arrange
        const string code = """
                            public class SampleClass
                            {
                                public void TestMethod(int x, int y)
                                {
                                    if (x > 0 || y < 0) { }
                                }
                            }
                            """;

        // Act
        var complexity = CodeMetricsCalculator.CalculateComplexity(code);

        // Assert
        Assert.Equal(3, complexity);
    }
}
