using PulseTemple.Domain.Common.Validation;
using PulseTemple.Domain.Exceptions.Custom;
using System.Text.RegularExpressions;

namespace Domain.Models.ValueObjects;

public partial record Email
{
    public string Value { get; init; }
    private Email() => Value = null!;
    private Email(string value) => Value = value;

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();

    public static Email Create(string value)
    {
        Guard.AgainstInvalidStr(value, 350, "Email");

        var trimmedValue = value.Trim().ToLower();

        if (!EmailRegex().IsMatch(trimmedValue))
            throw new ValidationDomainException("Invalid email format.");

        return new Email(trimmedValue);
    }

    public static implicit operator string(Email email) => email.Value;

    public static implicit operator Email(string value) => Create(value);
}
