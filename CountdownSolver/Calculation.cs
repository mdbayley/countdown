namespace CountdownSolver
{
    public class Calculation
    {
        public string Key { get; private init; }
        public string Calc { get; private init; }
        public HashSet<int> Indexes { get; } = new();
        public int Result { get; set; }

        private static Calculation Create(string key, string calc, IEnumerable<int> indexes, int index, int result)
        {
            var calculation = new Calculation
            {
                Key = key,
                Calc = calc,
                Result = result
            };

            foreach (var item in indexes)
            {
                calculation.Indexes.Add(item);
            }

            calculation.Indexes.Add(index);

            return calculation;
        }

        public static Calculation AddToList(IList<Calculation> list, string key, string calc, IEnumerable<int> indexes, int index, int result)
        {
            var calculation = Create(key, calc, indexes, index, result);
            list.Add(calculation);

            Console.WriteLine("---");
            Console.WriteLine(calculation);

            return calculation;
        }

        public override string ToString()
        {
            return $"{Key} : {Calc} : {string.Join(',', Indexes)} : {Result}";
        }
    }
}
