namespace Boardgames.DataProcessor;

using Boardgames.Data;
using Boardgames.Data.Models;
using Boardgames.Data.Models.Enums;
using Boardgames.DataProcessor.ImportDto;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Xml.Serialization;

public class Deserializer
{
    private const string ErrorMessage = "Invalid data!";

    private const string SuccessfullyImportedCreator
        = "Successfully imported creator – {0} {1} with {2} boardgames.";

    private const string SuccessfullyImportedSeller
        = "Successfully imported seller - {0} with {1} boardgames.";

    public static string ImportCreators(BoardgamesContext context, string xmlString)
    {
        StringBuilder sb  = new StringBuilder();
        XmlRootAttribute xmlRoot = new XmlRootAttribute("Creators");
        XmlSerializer xmlSerializer =
            new XmlSerializer(typeof(ImportCreatorDto[]), xmlRoot);
        StringReader stringReader = new StringReader(xmlString);
        ImportCreatorDto[] creatorDtos =
            (ImportCreatorDto[])xmlSerializer.Deserialize(stringReader); 

        ICollection<Creator> validCreator = new HashSet<Creator>();

        foreach (ImportCreatorDto creatorDto in creatorDtos)
        {
            if(!IsValid(creatorDto))
            {
                sb
                    .AppendLine(ErrorMessage);
                continue;
            }

            Creator creator = new Creator()
            {
                FirstName = creatorDto.FirstName,
                LastName = creatorDto.LastName
            };

            foreach (ImportBoardgame boardGameDto in creatorDto.ImportsBoardgames)
            {
                if(!IsValid(boardGameDto))
                {
                    sb 
                        .AppendLine(ErrorMessage);
                    continue;
                }

                Boardgame boardGame = new Boardgame()
                {
                    Name = boardGameDto.Name,
                    Rating = boardGameDto.Rating,
                    YearPublished = boardGameDto.YearPublished,
                    CategoryType = (CategoryType)boardGameDto.CategoryType,
                    Mechanics = boardGameDto.Mechanics
                };

                creator.Boardgames.Add(boardGame);
            }

            validCreator.Add(creator);
            sb
                .AppendLine(String.Format(SuccessfullyImportedCreator, creator.FirstName, creator.LastName, creator.Boardgames.Count));
        }

        context.Creators.AddRange(validCreator);
        context.SaveChanges();

        return sb.ToString().TrimEnd();
    }

    public static string ImportSellers(BoardgamesContext context, string jsonString)
    {
        StringBuilder sb = new StringBuilder();

        ImportSellerDto[] sellerDtos =
            JsonConvert.DeserializeObject<ImportSellerDto[]>(jsonString); 

        ICollection<Seller> validSeller = new HashSet<Seller>();  
        ICollection<int> validBoardgame = context.Boardgames
            .Select(b => b.Id)
            .ToArray();

        foreach (ImportSellerDto sellerDto in sellerDtos)
        {
            if(!IsValid(sellerDto))
            {
                sb
                    .AppendLine(ErrorMessage);
                continue;
            }

            Seller newseler = new Seller()
            {
                Name = sellerDto.Name,
                Address = sellerDto.Address,
                Country = sellerDto.Country,
                Website = sellerDto.Website
            };

            foreach (int boardgameid in sellerDto.BoardgamesIds.Distinct())
            {
                if(!validBoardgame.Contains(boardgameid))
                {
                    sb
                    .AppendLine(ErrorMessage);
                    continue;
                }

                BoardgameSeller boardgameSeller = new BoardgameSeller()
                {
                    Seller = newseler,
                    BoardgameId = boardgameid,
                };

                newseler.BoardgamesSellers.Add(boardgameSeller);
            }

            validSeller.Add(newseler);
            sb
                .AppendLine(String.Format(SuccessfullyImportedSeller, newseler.Name, newseler.BoardgamesSellers.Count));
        }

        context.Sellers.AddRange(validSeller);
        context.SaveChanges();

        return sb.ToString().TrimEnd();
    }

    private static bool IsValid(object dto)
    {
        var validationContext = new ValidationContext(dto);
        var validationResult = new List<ValidationResult>();

        return Validator.TryValidateObject(dto, validationContext, validationResult, true);
    }
}
