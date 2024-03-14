using System.Diagnostics;

namespace CountdownSolver
{
    internal class Program
    {
        private static readonly string _line = new('_', 80);

        private static void Main()
        {
            // https://en.wikipedia.org/wiki/Countdown_(game_show)#Numbers_Round
            
            Test(new List<int> { 75, 50, 2, 3, 8, 7 }, 812, false);
            Test(new List<int> { 75, 50, 2, 3, 8, 7 }, 812, false);

            Test(new List<int> { 75, 50, 2, 3, 8, 7 }, -1, false);
            Test(new List<int> { 75, 50, 2, 3, 8, 7 }, -1, false);

            Test(new List<int> { 75, 50, 2, 3, 8, 7 }, int.MaxValue, false);
            Test(new List<int> { 75, 50, 2, 3, 8, 7 }, int.MaxValue, false);
        }

        private static void Test(IList<int> inputs, int target, bool show)
        {
            var stopwatch = Stopwatch.StartNew();
            var answer = Solver.Solve(inputs, target, out var expressions);

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.WriteLine(_line);
            Console.WriteLine(answer);
            Console.WriteLine(_line);

            if (!show) return;

            foreach (var expression in expressions)
            {
                Console.WriteLine(expression);
            }
        }
    }
}