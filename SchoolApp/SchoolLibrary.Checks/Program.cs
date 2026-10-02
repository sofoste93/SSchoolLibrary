using SchoolLibrary;

var checks = new (string Name, Action Run)[]
{
    ("average and A grade", () =>
    {
        var result = GradeService.Evaluate("Ada", "Science", new decimal[] { 88, 94, 91 });
        Equal(91m, result.Average);
        Equal("A", result.Grade);
    }),
    ("rounding away from zero", () =>
    {
        var result = GradeService.Evaluate("Ada", "Arts", new decimal[] { 80, 81, 80.5m });
        Equal(80.5m, result.Average);
        Equal("B", result.Grade);
    }),
    ("empty student rejected", () => Throws<ArgumentException>(() =>
        GradeService.Evaluate(" ", "History", new decimal[] { 75 }))),
    ("out-of-range score rejected", () => Throws<ArgumentOutOfRangeException>(() =>
        GradeService.Evaluate("Ada", "History", new decimal[] { 101 }))),
    ("school social handle validated", () =>
    {
        var school = new School { Name = "Northstar", City = "Berlin", SocialHandle = "northstar" };
        Equal(1, school.Validate().Count);
    })
};

foreach (var check in checks)
{
    check.Run();
    Console.WriteLine($"PASS  {check.Name}");
}

Console.WriteLine($"\n{checks.Length} checks passed.");

static void Equal<T>(T expected, T actual)
{
    if (!EqualityComparer<T>.Default.Equals(expected, actual))
        throw new InvalidOperationException($"Expected {expected}, received {actual}.");
}

static void Throws<TException>(Action action) where TException : Exception
{
    try
    {
        action();
    }
    catch (TException)
    {
        return;
    }

    throw new InvalidOperationException($"Expected {typeof(TException).Name}.");
}
