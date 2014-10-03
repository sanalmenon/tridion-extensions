namespace System
{
    public static class DateTimeExtensions
    {
        public static string ToRFC822Date(this Nullable<DateTime> inputDate)
        {
            string formattedDate = inputDate.Value.ToString();
            const string RFC822DateFormat = "yyyyMMddHHmmss";
            var dt = new DateTime();
            if (DateTime.TryParse(formattedDate, out dt))
            {
                formattedDate = dt.ToString(RFC822DateFormat);
            }
            return formattedDate;
        }
    }
}
