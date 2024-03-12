
using System.Diagnostics;

namespace CountdownSolver
{
    public class Solver
    {
        private Solver()
        {

        }

        public static IList<Expression> Solve(IList<int> inputs)
        {
            // Initialise expressions with basic single-value inputs
            var expressions = inputs.Select((input, i) => new Expression(input, $"{input}", new[] { i })).ToList();

            // First-Order iterations, Input v Input

            var count = expressions.Count;
            
            for (var lhs = 0; lhs < count; lhs++)
            {
                for (var rhs = 0; rhs < count; rhs++)
                {
                    DoAdd(lhs, expressions[lhs], rhs, expressions[rhs], expressions);
                    DoSubtract(lhs, expressions[lhs], rhs, expressions[rhs], expressions);
                    DoMultiply(lhs, expressions[lhs], rhs, expressions[rhs], expressions);
                    DoDivide(lhs, expressions[lhs], rhs, expressions[rhs], expressions);
                }
            }

            /*
            foreach (var expression in expressions)
            {
                Console.WriteLine(expression);
            }
            */


            // Second-Order iterations, Expression v Expression

            count = expressions.Count;

            for (var lhs = 0; lhs < count; lhs++)
            {
                for (var rhs = 0; rhs < count; rhs++)
                {
                    Debug.Assert(lhs < 32, "LHS");
                    Debug.Assert(rhs < 32, "RHS");

                    DoAdd(lhs, expressions[lhs], rhs, expressions[rhs], expressions);
                    DoSubtract(lhs, expressions[lhs], rhs, expressions[rhs], expressions);
                    DoMultiply(lhs, expressions[lhs], rhs, expressions[rhs], expressions);
                    DoDivide(lhs, expressions[lhs], rhs, expressions[rhs], expressions);
                }
            }

            /*
            foreach (var expression in expressions)
            {
                Console.WriteLine(expression);
            }
            */


            // TO BE SORTED
            return expressions;
        }

        private static int DoAdd(int ilhs, Expression lhs, int irhs, Expression rhs, List<Expression> expressions)
        {
            if (lhs.Indexes == rhs.Indexes)
            {
                return 0;
            }

            var result = lhs.Result + rhs.Result;
            var exposition = $"({lhs.Exposition})+({rhs.Exposition})";
            expressions.Add(new Expression(result, exposition, new[] { ilhs, irhs }));

            return 1;
        }

        private static int DoSubtract(int ilhs, Expression lhs, int irhs, Expression rhs, List<Expression> expressions)
        {
            if (lhs.Indexes == rhs.Indexes)
            {
                return 0;
            }

            var result = Math.Abs(lhs.Result - rhs.Result);
            var exposition = lhs.Result > rhs.Result
                ? $"({lhs.Exposition})-({rhs.Exposition})"
                : $"({rhs.Exposition})-({lhs.Exposition})";
            
            expressions.Add(new Expression(result, exposition, new[] { ilhs, irhs }));

            return 1;
        }

        private static int DoMultiply(int ilhs, Expression lhs, int irhs, Expression rhs, List<Expression> expressions)
        {
            if (lhs.Indexes == rhs.Indexes)
            {
                return 0;
            }

            var result = lhs.Result * rhs.Result;
            var exposition = $"({lhs.Exposition})*({rhs.Exposition})";
            expressions.Add(new Expression(result, exposition, new[] { ilhs, irhs }));

            return 1;
        }

        private static int DoDivide(int ilhs, Expression lhs, int irhs, Expression rhs, List<Expression> expressions)
        {
            if (lhs.Indexes == rhs.Indexes)
            {
                return 0;
            }

            var l = lhs.Result;
            var r = rhs.Result;

            int result;
            string? exposition;

            if (r > 0 && l % r == 0)
            {
                result = l / r;
                exposition = $"({lhs.Exposition})/({rhs.Exposition})";
                expressions.Add(new Expression(result, exposition, new[] { ilhs, irhs }));
                return 1;
            }
            
            if (l > 0 && r % l == 0)
            {
                result = r / l;
                exposition = $"({rhs.Exposition})/({lhs.Exposition})";
                expressions.Add(new Expression(result, exposition, new[] { ilhs, irhs }));
                return 1;
            }

            return 0;
        }
    }
}
