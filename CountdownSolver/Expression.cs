namespace CountdownSolver
{
    public class Expression
    {
        public Expression(int result, string? calculation, IEnumerable<int> indexes)
        {
            Result = result;
            Calculation = calculation;
            Indexes = new HashSet<int>(indexes);
        }

        public Expression(int result, string? calculation, int index)
        {
            Result = result;
            Calculation = calculation;
            Indexes = new HashSet<int> { index };
        }

        public int Result { get; init; }
        public string? Calculation { get; init; }
        public HashSet<int> Indexes { get; init; }

        public override string ToString()
        {
            return $"{Result} = {Calculation} [{string.Join(',', Indexes)}]";
        }
    }
}
