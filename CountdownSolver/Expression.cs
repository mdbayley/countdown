namespace CountdownSolver
{
    public class Expression
    {
        public Expression(int result, string? exposition, IEnumerable<int> indexes)
        {
            Result = result;
            Exposition = exposition;

            foreach (var index in indexes)
            {
                Indexes |= (int)Math.Pow(2, index);
            }
        }

        public int Result { get; set; }
        public string? Exposition { get; set; }
        public int Indexes { get; set; } // Bitwise represenatation of the original input indexes used in the Exposition

        public override string ToString()
        {
            return $"{Result} = {Exposition} [{Convert.ToString(Indexes, toBase: 2).PadLeft(32, '0'), 32}]";
        }
    }
}
