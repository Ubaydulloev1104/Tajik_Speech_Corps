using System;

namespace TSR_Client.Services.ConverterService
{
    public interface IDateTimeConvertToStringService
    {
        string GetDisplayPostedDate(DateTime publishDate);
        (string DisplayDate, string Color) GetDeadlineOrEndDateDisplayDate(DateTime value);
    }
}
