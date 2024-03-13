using System.Diagnostics;
using CountdownSolver;

var stopwatch = Stopwatch.StartNew();
var answer = Solver.Solve(new List<int> { 2, 5, 8, 16, 3 }, int.MaxValue, out var expressions);

Console.WriteLine(stopwatch.ElapsedMilliseconds);
Console.WriteLine("---------------------------------");
Console.WriteLine(answer);
Console.WriteLine("---------------------------------");

foreach (var expression in expressions)
{
    Console.WriteLine(expression);
}
