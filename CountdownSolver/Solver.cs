
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

            // Evaluate initial expressions (i.e. single-value expressions)
            //while (true)
            {
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

//                break;
            }



            /*
            var start = count;

            var count = 0;
            var counter = 0;
            count = counter;


            for (var iteration = 0; iteration < iterations; iteration++)
            {
                Console.WriteLine($">> {iteration}");

                for (var i = 0; i < inputs.Count; i++)
                {
                    var input = inputs[i];

                    // Calculate

                    for (var c = start; c < count; c++)
                    {
                        var calculation = expressions[c];

                        if (calculation.Indexes.Contains(i)) continue;

                        // Add

                        var result = calculation.Result + input;
                        var key = calculation.Indexes.Count > 1 ? $"({calculation.Key}) + [{i}]" : $"{calculation.Key} + [{i}]";
                        var calc = calculation.Indexes.Count > 1 ? $"({calculation.Result}) + [{input}]" : $"{calculation.Result} + {input}";

                        Expression.AddToList(expressions, key, $"{calc}", calculation.Indexes, i, result);
                        counter++;

                        // Subtract

                        result = calculation.Result - input;

                        if (result < 0)
                        {
                            key = calculation.Indexes.Count > 1 ? $"[{i}] - ({calculation.Key})" : $"[{i}] - {calculation.Key}";
                            calc = calculation.Indexes.Count > 1 ? $"{input} - ({calculation.Result}) " : $"{input} - {calculation.Result}";
                        }
                        else
                        {
                            key = calculation.Indexes.Count > 1 ? $"({calculation.Key}) - [{i}]" : $"{calculation.Key} - [{i}]";
                            calc = calculation.Indexes.Count > 1 ? $"({calculation.Result}) - {input}" : $"{calculation.Result} - {input}";
                        }


                        Expression.AddToList(expressions, key, $"{calc}", calculation.Indexes, i, Math.Abs(result));
                        counter++;

                        // Multiply

                        result = calculation.Result * input;
                        key = calculation.Indexes.Count > 1 ? $"({calculation.Key}) * [{i}]" : $"{calculation.Key} * [{i}]";
                        calc = calculation.Indexes.Count > 1 ? $"({calculation.Result}) * {input}" : $"{calculation.Result} * {input}";

                        Expression.AddToList(expressions, key, $"{calc}", calculation.Indexes, i, result);
                        counter++;

                        // Divide

                        var x = calculation.Result;

                        if (input > 0 && x % input == 0)
                        {
                            result = x / input;
                            key = calculation.Indexes.Count > 1 ? $"({calculation.Key}) / [{i}]" : $"{calculation.Key} / [{i}]";
                            calc = calculation.Indexes.Count > 1 ? $"({calculation.Result}) / {input}" : $"{calculation.Result} / {input}";

                            Expression.AddToList(expressions, key, $"{calc}", calculation.Indexes, i, result);
                            counter++;
                        }
                        else if (x > 0 && input % x == 0)
                        {
                            result = input / x;

                            key = calculation.Indexes.Count > 1 ? $"[{i}] / ({calculation.Key})" : $"[{i}] / {calculation.Key}";
                            calc = calculation.Indexes.Count > 1 ? $"{input} / ({calculation.Result}) " : $"{input} / {calculation.Result}";

                            Expression.AddToList(expressions, key, $"{calc}", calculation.Indexes, i, result);
                            counter++;
                        }
                    }
                }

                start = count;
                count += counter;
            }

            */

            foreach (var expression in expressions)
            {
                Console.WriteLine(expression);
            }

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
