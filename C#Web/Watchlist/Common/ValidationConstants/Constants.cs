namespace Watchlist.Common.ValidationConstants
{
    public class Constants
    {
        public const int UserUserNameMinLength = 5; 
        public const int UserUserNameMaxLength = 20;

        public const int UserEmailMinLength = 10;
        public const int UserEmailMaxLength = 60; 

        public const int UserPasswordMinLength = 5;
        public const int UserPasswordlMaxLength = 20;

        public const int MovieTitleMinLength = 10; 
        public const int MovieTitleMaxLength = 50;

        public const int MovieDirectorMinLength = 5;
        public const int MovieDirectorMaxLength = 50;

        public const decimal MovieRatingMinRange = 0.00m;
        public const decimal MovieRatingMaxRange = 10.00m;

        public const int GenreNameMinLength = 10; 
        public const int GenreNameMaxLength = 50;
    }
}
