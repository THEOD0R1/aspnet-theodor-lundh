using PulseTemple.Domain.Exceptions.Custom;

namespace PulseTemple.Domain.Common.Validation;

public static class Guard
{
    public static void AgainstEmptyGuid(Guid value, string message = "Id is required")
    {
        if (value == Guid.Empty)
            throw new ValidationDomainException(message);
    }
    public static void IsBeforeDate(DateTime startTime, DateTime endTime, string message)
    {
        if (endTime <= startTime)
            throw new ValidationDomainException(message);
    }
    public static void AgainstInvalidStr(string value, int maxLength, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValidationDomainException($"{fieldName} is required.");

        if (value.Length > maxLength)
            throw new ValidationDomainException($"{fieldName} cannot exceed {maxLength} characters.");
    }
    public static void MaxLengthValidation(string? value, int maxLength, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            return;    
        if (value.Length > maxLength)
            throw new ValidationDomainException($"{fieldName} cannot exceed {maxLength} characters.");
    }
    public static void TransferValidation(Guid newId, Guid oldId)
    {
        if (oldId == newId)
            throw new ValidationDomainException("Target cannot be the same as before.");

        if (newId == Guid.Empty)
            throw new ValidationDomainException("Target is required.");
    }
    public static void AgainstInsecurePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            throw new ValidationDomainException("Password must be at least 8 characters long.");
    }
}
