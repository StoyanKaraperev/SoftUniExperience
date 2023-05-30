﻿namespace Boardgames.Common;

public class ValidationConstants
{
    // Boardgame  
    public const int BoardgameNameMinValidation = 10; 
    public const int BoardgameNameMaxValidation = 20;

    public const double BoardgameRatingMinRange = 1.00;
    public const double BoardgameRatingMaxRange = 10.00;

    public const int BoardgameYearPublishedMinRange = 2018;
    public const int BoardgameYearPublishedMaxRange = 2023;

    public const int BoardgameCategoryTypeMinRange = 0;
    public const int BoardgameCategoryTypeMaxRange = 4;

    // Seller 
    public const int SellerNameMinLength = 5;
    public const int SellerNameMaxLength = 20;

    public const int SellerAddressMinLength = 2; 
    public const int SellerAddressMaxLength = 30;

    public const string SellerWebsiteRegex = @"^www.[A-Za-z\d\-]*.com";

    // Creator 
    public const int CreatorFirstNameMinLength = 2;
    public const int CreatorFirstNameMaxLength = 7;

    public const int CreatorLastNameMinLength = 2;
    public const int CreatorLastNameMaxLength = 7;
}
