using System.Diagnostics.CodeAnalysis;

namespace TaxCRM.Utils.Guards;

public static class Guard
{
    public static void ArgumentPassCondition(bool condition, string error)
    {
        if (!condition)
            throw new ArgumentException(error);
    }

    public static void ArgumentIsNotNull([NotNull]object? arg, string error)
    {
        if (arg is null)
            throw new ArgumentException(error);
    }
}
