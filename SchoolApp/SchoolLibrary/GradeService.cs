namespace SchoolLibrary;

/// <summary>
/// Contains grade rules independently from WinForms. This separation keeps the
/// calculation easy to read, test and reuse.
/// </summary>
public static class GradeService
{
    public static StudentEvaluation Evaluate(
        string studentName,
        string subject,
        IEnumerable<decimal> scores)
    {
        if (string.IsNullOrWhiteSpace(studentName))
            throw new ArgumentException("Enter the student's name.", nameof(studentName));
        if (string.IsNullOrWhiteSpace(subject))
            throw new ArgumentException("Choose a subject.", nameof(subject));

        var scoreList = scores.ToArray();
        if (scoreList.Length == 0)
            throw new ArgumentException("Enter at least one score.", nameof(scores));
        if (scoreList.Any(score => score is < 0 or > 100))
            throw new ArgumentOutOfRangeException(nameof(scores), "Scores must be between 0 and 100.");

        var average = Math.Round(scoreList.Average(), 1, MidpointRounding.AwayFromZero);
        var (grade, comment) = average switch
        {
            >= 90 => ("A", "Excellent work"),
            >= 80 => ("B", "Very good progress"),
            >= 70 => ("C", "Solid result"),
            >= 60 => ("D", "More practice needed"),
            _ => ("F", "Support recommended")
        };

        return new StudentEvaluation(studentName.Trim(), subject.Trim(), average, grade, comment);
    }
}
