namespace Library.Common
{
    public static class ValidationConstants
    {
        public static class Book
        {
            public const int TitleMinLength = 10;
            public const int TitleMaxLength = 50;

            public const int AuthorMinLength = 5;
            public const int AuthorMaxLength = 50;

            public const int DescriptionMinLength = 5;
            public const int DescriptionMaxLength = 5000;

            public const int ImageUrlMinLength = 5;

            public const int CategoryIdMinValue = 1; 
            public const int CategoryIdMaxValue = int.MaxValue;

            public const string RatingMinValue = "0.00";
            public const string RatingMaxValue = "10.00";
        }

        public static class Category
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 50; 
        }
    }
}
