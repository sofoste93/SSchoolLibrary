namespace SchoolLibrary;

/// <summary>A calculated result ready to display or export.</summary>
public sealed record StudentEvaluation(
    string StudentName,
    string Subject,
    decimal Average,
    string Grade,
    string Comment);
