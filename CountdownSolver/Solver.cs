
namespace CountdownSolver
{
    public class Solver
    {
        private Solver()
        {

        }

        public static IList<Calculation> Solve(IList<int> inputs)
        {
            var calculations = new List<Calculation>();

            var iterations = inputs.Count * inputs.Count;

            var count = 0;
            var counter = 0;


            for (var i = 0; i < inputs.Count; i++)
            {
                var input = inputs[i];

                // Initialise

                Calculation.AddToList(calculations, $"[{i}]", $"{input}", Enumerable.Empty<int>(), i, input);
                counter++;
            }

            var start = count;
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
                        var calculation = calculations[c];

                        if (calculation.Indexes.Contains(i)) continue;

                        // Add

                        var result = calculation.Result + input;
                        var key = calculation.Indexes.Count > 1 ? $"({calculation.Key}) + [{i}]" : $"{calculation.Key} + [{i}]";
                        var calc = calculation.Indexes.Count > 1 ? $"({calculation.Result}) + [{input}]" : $"{calculation.Result} + {input}";

                        Calculation.AddToList(calculations, key, $"{calc}", calculation.Indexes, i, result);
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


                        Calculation.AddToList(calculations, key, $"{calc}", calculation.Indexes, i, Math.Abs(result));
                        counter++;

                        // Multiply

                        result = calculation.Result * input;
                        key = calculation.Indexes.Count > 1 ? $"({calculation.Key}) * [{i}]" : $"{calculation.Key} * [{i}]";
                        calc = calculation.Indexes.Count > 1 ? $"({calculation.Result}) * {input}" : $"{calculation.Result} * {input}";

                        Calculation.AddToList(calculations, key, $"{calc}", calculation.Indexes, i, result);
                        counter++;

                        // Divide

                        var x = calculation.Result;

                        if (input > 0 && x % input == 0)
                        {
                            result = x / input;
                            key = calculation.Indexes.Count > 1 ? $"({calculation.Key}) / [{i}]" : $"{calculation.Key} / [{i}]";
                            calc = calculation.Indexes.Count > 1 ? $"({calculation.Result}) / {input}" : $"{calculation.Result} / {input}";

                            Calculation.AddToList(calculations, key, $"{calc}", calculation.Indexes, i, result);
                            counter++;
                        }
                        else if (x > 0 && input % x == 0)
                        {
                            result = input / x;

                            key = calculation.Indexes.Count > 1 ? $"[{i}] / ({calculation.Key})" : $"[{i}] / {calculation.Key}";
                            calc = calculation.Indexes.Count > 1 ? $"{input} / ({calculation.Result}) " : $"{input} / {calculation.Result}";

                            Calculation.AddToList(calculations, key, $"{calc}", calculation.Indexes, i, result);
                            counter++;
                        }
                    }
                }

                start = count;
                count += counter;
            }

            // TO BE SORTED
            return calculations;
        }
    }
}
