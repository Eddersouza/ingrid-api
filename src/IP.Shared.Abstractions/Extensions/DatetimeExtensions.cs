namespace IP.Shared.Abstractions.Extensions;

public static class DatetimeExtensions
{
    public static DateTime FirstMomentOfMonth(this DateTime? date, DateTime defaultValue)
    {
        DateTime dateToChange = date ?? defaultValue;

        return new DateTime(dateToChange.Year, dateToChange.Month, 1);
    }

    public static DateTime LastMomentOfMonth(this DateTime? date, DateTime defaultValue)
    {
        DateTime dateToChange = date ?? defaultValue;

        int year = dateToChange.Year;
        int month = dateToChange.Month;
        int days = DateTime.DaysInMonth(year, month); // Detecta se é ano bissexto

        return new DateTime(year, month, days, 23, 59, 59);
    }
}
