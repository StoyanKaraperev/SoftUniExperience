﻿namespace Boardgames.DataProcessor.ExportDto;

using System.Xml.Serialization;

[XmlType("Creator")]
public class ExportCreatorDto
{
    [XmlElement("CreatorName")]
    public string CreatorName { get; set; } = null!;

    [XmlAttribute("BoardgamesCount")]
    public int BoardgameCount { get; set; }

    [XmlArray("Boardgames")]
    public ExportBoardgameDto[] Boardgames { get; set; } = null!;
}
