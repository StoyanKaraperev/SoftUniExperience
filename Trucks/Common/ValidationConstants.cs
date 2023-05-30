namespace Trucks.Common;

public class ValidationConstants
{
    // Truck validation 
    public const int TruckRegistrationNumberMaxLength = 8;
    public const int TruckVinNumberMaxLength = 17;
    public const int TruckRegistrationNumberMinLength = 8;
    public const string TruckRegistrationNumberRegex =
        @"([A-Z]){2}\d{4}[A-Z]{2}";
    public const int TruckVinNumberMinLength = 17;
    public const int TruckTankCapacityMaxLength = 1420;
    public const int TruckTankCapacityMinLength = 950;
    public const int TruckCargoCapacityMaxLength = 29000;
    public const int TruckCargoCapacityMinLength = 5000;
    public const int TruckCategoryTypeMinLength = 0;
    public const int TruckCategoryTypeMaxLength = 3;
    public const int TruckMakeTypeMinLength = 0;
    public const int TruckMakeTypeMaxLength = 4;

    // Client validatio 
    public const int ClientNameMaxLength = 40;
    public const int ClientNameMinLength = 3;
    public const int ClientNationalityMaxLength = 40;
    public const int ClientNationalityMinLength = 2;

    // Despatcher validation 
    public const int DespatcherNameMaxLenght = 40; 
    public const int DespatcherNameMinLenght = 2;
}
