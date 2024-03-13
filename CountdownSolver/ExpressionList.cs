namespace CountdownSolver
{
    // If we don't restrict the Expressions to a known-good set of results, the logarithmic permutation count goes bananas

    internal class ExpressionList
    {
        public List<Expression> Expressions { get; } = new();
        public HashSet<int> Results { get; } = new();
    }
}
