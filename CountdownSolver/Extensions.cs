namespace CountdownSolver
{
    internal static class Extensions
    {
        public static Expression Add(this IList<Expression> expressions, int index, int result)
        {
            var expression = new Expression(result, $"{result}", index);
            expressions.Add(expression);
            return expression;
        }

        public static Expression Evaluate(this Expression result, Expression? answer, int target)
        {
            if (answer == null) return result;

            if (result.Result == target)
            {
                return result;
            }

            return Math.Abs(result.Result - target) < Math.Abs(answer.Result - target)
                ? result
                : answer;
        }

        public static bool TryAdd(this IList<Expression> expressions, int lhsIndex, int rhsIndex, out Expression? expression)
        {
            var lhs = expressions[lhsIndex];
            var rhs = expressions[rhsIndex];

            // Can only use an index once
            if (rhs.Indexes.Any(lhs.Indexes.Contains))
            {
                expression = null;
                return false;
            }

            var result = lhs.Result + rhs.Result;
            var calculation = $"({lhs.Calculation})+({rhs.Calculation})";
            var indexes = lhs.Indexes.Concat(rhs.Indexes);

            expression = new Expression(result, calculation, indexes);

            expressions.Add(expression);

            return true;
        }

        public static bool TrySubtract(this IList<Expression> expressions, int lhsIndex, int rhsIndex, out Expression? expression)
        {
            var lhs = expressions[lhsIndex];
            var rhs = expressions[rhsIndex];

            // Can only use an index once
            if (rhs.Indexes.Any(lhs.Indexes.Contains))
            {
                expression = null;
                return false;
            }

            var result = Math.Abs(lhs.Result - rhs.Result);
            var calculation = lhs.Result > rhs.Result
                ? $"({lhs.Calculation})-({rhs.Calculation})"
                : $"({rhs.Calculation})-({lhs.Calculation})";

            var indexes = lhs.Indexes.Concat(rhs.Indexes);

            expression = new Expression(result, calculation, indexes);

            expressions.Add(expression);

            return true;
        }

        public static bool TryMultiply(this IList<Expression> expressions, int lhsIndex, int rhsIndex, out Expression? expression)
        {
            var lhs = expressions[lhsIndex];
            var rhs = expressions[rhsIndex];

            // Can only use an index once
            if (rhs.Indexes.Any(lhs.Indexes.Contains))
            {
                expression = null;
                return false;
            }

            var result = lhs.Result * rhs.Result;
            var calculation = $"({lhs.Calculation})*({rhs.Calculation})";
            var indexes = lhs.Indexes.Concat(rhs.Indexes);

            expression = new Expression(result, calculation, indexes);

            expressions.Add(expression);

            return true;
        }


        public static bool TryDivide(this IList<Expression> expressions, int lhsIndex, int rhsIndex, out Expression? expression)
        {
            var lhs = expressions[lhsIndex];
            var rhs = expressions[rhsIndex];

            // Can only use an index once
            if (rhs.Indexes.Any(lhs.Indexes.Contains))
            {
                expression = null;
                return false;
            }

            var l = lhs.Result;
            var r = rhs.Result;

            int result;
            string? calculation;

            if (r > 0 && l % r == 0)
            {
                result = l / r;
                calculation = $"({lhs.Calculation})/({rhs.Calculation})";
            }
            else if (l > 0 && r % l == 0)
            {
                result = r / l;
                calculation = $"({rhs.Calculation})/({lhs.Calculation})";
            }
            else
            {
                expression = null;
                return false;
            }

            var indexes = lhs.Indexes.Concat(rhs.Indexes);

            expression = new Expression(result, calculation, indexes);

            expressions.Add(expression);

            return true;
        }
    }
}
