namespace Boardgames.DataProcessor.ImportDto;

using Boardgames.Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

[XmlType("Creator")]
public class ImportCreatorDto
{
    [XmlElement("FirstName")]
    [Required]
    [MinLength(ValidationConstants.CreatorFirstNameMinLength)]
    [MaxLength(ValidationConstants.CreatorFirstNameMaxLength)]
    public string FirstName { get; set; } = null!;

    [XmlElement("LastName")]
    [Required]
    [MinLength(ValidationConstants.CreatorLastNameMinLength)]
    [MaxLength(ValidationConstants.CreatorLastNameMaxLength)]
    public string LastName { get; set; } = null!;

    [XmlArray("Boardgames")] 
    public ImportBoardgame[] ImportsBoardgames { get; set; } = null!; 

}
