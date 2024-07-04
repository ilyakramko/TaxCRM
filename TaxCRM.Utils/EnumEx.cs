namespace TaxCRM.Utils;

public static class EnumEx
{
    public static bool TryValidate<T>(string value, out T result) where T : struct
    {
        result = default;

        if (!Enum.IsDefined(typeof(T), value))
            return false;

        if (!Enum.GetNames(typeof(T)).Any(x => x == value))
            return false;

        if (!Enum.TryParse<T>(value, false, out result))
            return false;

        return true;
    } 
}
