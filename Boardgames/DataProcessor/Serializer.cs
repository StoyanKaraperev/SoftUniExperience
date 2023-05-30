namespace Boardgames.DataProcessor;

using Boardgames.Data;
using Boardgames.DataProcessor.ExportDto;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Serialization;

public class Serializer
{
    public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
    {
        StringBuilder sb = new StringBuilder();

        XmlSerializer xmlSerializer =
            new XmlSerializer(typeof(ExportCreatorDto[]), new XmlRootAttribute("Creators"));

        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
        namespaces.Add(string.Empty, String.Empty);

        using (StringWriter stringWriter = new StringWriter(sb))
        {
            ExportCreatorDto[] creators = context.Creators
                .Where(c => c.Boardgames.Any())
                .Select(c => new ExportCreatorDto()
                {
                    CreatorName = c.FirstName + " " + c.LastName,
                    BoardgameCount = c.Boardgames.Count,
                    Boardgames = c.Boardgames
                     .Select(b => new ExportBoardgameDto()
                     {
                         BoardgameName = b.Name,
                         BoardgameYearPublished = b.YearPublished,
                     })
                     .OrderBy(b => b.BoardgameName)
                     .ToArray()
                })
                .OrderByDescending(c => c.BoardgameCount)
                .ThenBy(c => c.CreatorName)
                .ToArray();

            xmlSerializer.Serialize(stringWriter, creators, namespaces);
        }

        return sb.ToString().TrimEnd();
    }

    public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
    {
        var sellers = context.Sellers
            .Where(s => s.BoardgamesSellers.Any(bs => bs.Boardgame.YearPublished >= year && bs.Boardgame.Rating <= rating))
            .Select(s => new
            {
                s.Name,
                s.Website,
                Boardgames = s.BoardgamesSellers
                    .Where(bs => bs.Boardgame.YearPublished >= year && bs.Boardgame.Rating <= rating)
                    .Select(bs => new
                    {
                        bs.Boardgame.Name,
                        bs.Boardgame.Rating,
                        bs.Boardgame.Mechanics,
                        Category = bs.Boardgame.CategoryType.ToString()
                    })
                    .OrderByDescending(bs => bs.Rating)
                    .ThenBy(bs => bs.Name)
                    .ToArray()
            })
            .OrderByDescending(seller => seller.Boardgames.Length)
            .ThenBy (s => s.Name)
            .Take(5)
            .ToArray();

        return JsonConvert.SerializeObject(sellers, Formatting.Indented);
    }
}