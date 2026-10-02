namespace SchoolLibrary;

/// <summary>
/// Holds the identity data displayed in the school register. Validation lives
/// in the model so another interface can reuse the same rules later.
/// </summary>
public sealed class School
{
    public string Name { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public string Region { get; init; } = string.Empty;
    public string PostalCode { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string SocialHandle { get; init; } = string.Empty;

    public IReadOnlyList<string> Validate()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(Name))
            errors.Add("Enter the school name.");
        if (string.IsNullOrWhiteSpace(City))
            errors.Add("Enter the city.");
        if (!string.IsNullOrWhiteSpace(SocialHandle) && !SocialHandle.StartsWith('@'))
            errors.Add("The social handle must begin with @.");

        return errors;
    }

    public string FormattedAddress => string.Join(", ", new[] { Address, PostalCode, City, Region }
        .Where(part => !string.IsNullOrWhiteSpace(part)));
}
