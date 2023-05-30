namespace Boardgames.DataProcessor.ImportDto;

using Boardgames.Common;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;


[XmlType("Boardgame")]
public class ImportBoardgame
{
    [XmlElement("Name")]
    [Required]
    [MinLength(ValidationConstants.BoardgameNameMinValidation)]
    [MaxLength(ValidationConstants.BoardgameNameMaxValidation)]
    public string Name { get; set; } = null!;

    [XmlElement("Rating")]
    [Required]
    [Range(ValidationConstants.BoardgameRatingMinRange, ValidationConstants.BoardgameRatingMaxRange)]
    public double Rating { get; set; }

    [XmlElement("YearPublished")]
    [Required]
    [Range(ValidationConstants.BoardgameYearPublishedMinRange, ValidationConstants.BoardgameYearPublishedMaxRange)]
    public int YearPublished { get; set; }

    [XmlElement("CategoryType")]
    [Required]
    [Range(ValidationConstants.BoardgameCategoryTypeMinRange,
        ValidationConstants.BoardgameCategoryTypeMaxRange)]
    public int CategoryType { get; set; }

    [XmlElement("Mechanics")]
    [Required]
    public string Mechanics { get; set; } = null!;
}
