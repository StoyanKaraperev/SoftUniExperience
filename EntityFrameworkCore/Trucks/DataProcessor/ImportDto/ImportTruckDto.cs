namespace Trucks.DataProcessor.ImportDto;

using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Trucks.Common;

[XmlType("Truck")]
public class ImportTruckDto
{
    [XmlElement("RegistrationNumber")]
    [Required]
    [MinLength(ValidationConstants.TruckRegistrationNumberMinLength)]
    [MaxLength(ValidationConstants.TruckRegistrationNumberMaxLength)]
    [RegularExpression(ValidationConstants.TruckRegistrationNumberRegex)]
    public string RegistrationNumber { get; set; } = null!;

    [XmlElement("VinNumber")]
    [Required]
    [MinLength(ValidationConstants.TruckVinNumberMinLength)]
    [MaxLength(ValidationConstants.TruckVinNumberMaxLength)]
    public string VinNumber { get; set; } = null!;

    [XmlElement("TankCapacity")]
    [Range(ValidationConstants.TruckTankCapacityMinLength, ValidationConstants.TruckTankCapacityMaxLength)]
    public int TankCapacity { get; set; }

    [XmlElement("CargoCapacity")]
    [Range(ValidationConstants.TruckCargoCapacityMinLength, ValidationConstants.TruckCargoCapacityMaxLength)]
    public int CargoCapacity { get; set; }

    [XmlElement("CategoryType")]
    [Required]
    [Range(ValidationConstants.TruckCategoryTypeMinLength, ValidationConstants.TruckCategoryTypeMaxLength)]
    public int CategoryType { get; set; }

    [XmlElement("MakeType")]
    [Required]
    [Range(ValidationConstants.TruckMakeTypeMinLength, ValidationConstants.TruckMakeTypeMaxLength)]
    public int MakeType { get; set; }
}
