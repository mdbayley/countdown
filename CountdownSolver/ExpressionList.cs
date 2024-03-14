using System.Collections;

namespace CountdownSolver
{
    // If we don't restrict the Expressions to a known-good set of results, the logarithmic permutation count goes bananas

    public class ExpressionList : IEnumerable
    {
        public List<Expression> Expressions { get; } = new();
        public HashSet<int> Results { get; } = new();

        public Expression this[int index] => Expressions[index];

        public int Count => Expressions.Count;

        public Expression Add(int index, int result)
        {
            return Expressions.Add(index, result);
        }

        public void Add(Expression expression)
        {
            if(Results.Contains(expression.Result)) return;
            Results.Add(expression.Result);
            
            Expressions.Add(expression);
        }

        public IEnumerator GetEnumerator()
        {
            return Expressions.GetEnumerator();
        }
    }
}
