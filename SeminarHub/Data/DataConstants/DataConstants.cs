namespace SeminarHub.Data.DataConstants
{
    public static class DataConstants
    {
        public const int SeminarTopicMinimumLength = 3;
        public const int SeminarTopicMaximumLength = 100;

        public const int SeminarLecturerMinimumLength = 5;
        public const int SeminarLecturerMaximumLength = 60;

        public const int SeminarDetailsMinimumLength = 10;
        public const int SeminarDetailsMaximumLength = 500;

        public const string SeminarDateAndTimeFormat = "dd/MM/yyyy HH:mm";
        public const string SeminarDateAndTimeRegex = @"\d{2}\/\d{2}\/\d{4} \d{2}:\d{2}";
        public const string SeminarDateAndTimeErrorMsg = "Field Date and Time must match the dd/MM/yyyy HH:mm pattern";
        public const string SeminarDurationErrorMsg = "Duration is required";

        public const int SeminarDurationMin = 30;
        public const int SeminarDurationMax = 180;

        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 50;
    }
}
