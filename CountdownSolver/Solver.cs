
namespace CountdownSolver
{
    public class Solver
    {
        private Solver()
        {

        }

        public static Expression? Solve(IList<int> inputs, int target, out IList<Expression> expressions)
        {
            expressions = new List<Expression>();

            Expression? answer = null;

            // Initialise expressions with basic single-value inputs

            for (var i = 0; i < inputs.Count; i++)
            {
                var expression = expressions.Add(i, inputs[i]);
                answer = expression.Evaluate(answer, target);
                if (answer.Result == target) return answer;
            }

            // Evaluate expressions against each other

            bool done;
            var count = 0;
            do
            {
                var start = count;
                count = expressions.Count;
                done = true;

                for (var lhs = 0; lhs < count; lhs++)
                {
                    for (var rhs = start; rhs < count; rhs++)
                    {
                        if (expressions.TryAdd(lhs, rhs, out var result))
                        {
                            done = false;
                            answer = result?.Evaluate(answer, target);
                            if (answer?.Result == target) return answer;
                        }

                        if (expressions.TrySubtract(lhs, rhs, out result))
                        {
                            done = false;
                            answer = result?.Evaluate(answer, target);
                            if (answer?.Result == target) return answer;
                        }

                        if (expressions.TryMultiply(lhs, rhs, out result))
                        {
                            done = false;
                            answer = result?.Evaluate(answer, target);
                            if (answer?.Result == target) return answer;
                        }

                        if (expressions.TryDivide(lhs, rhs, out result))
                        {
                            done = false;
                            answer = result?.Evaluate(answer, target);
                            if (answer?.Result == target) return answer;
                        }
                    }

                    Console.Write('.');
                }

            } while (!done);

            return answer;
        }
    }
}
