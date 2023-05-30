namespace Trucks.Utilities;

using System.Text;
using System.Xml.Serialization;

public class XmlHelper
{
    public T Deserialize<T> (string inputXml, string rootName)
    {
        // Create XmlRootAtribute 
        XmlRootAttribute xmlRoot = new XmlRootAttribute(rootName);

        // XmlSerializer = XmlSerializer and XmlDeserializer
        XmlSerializer xmlSerializer =
            new XmlSerializer(typeof(T), xmlRoot);

        // Convert string to Stream!!! 
        using StringReader stringReader = new StringReader(inputXml);

        T supplierDto =
            (T)xmlSerializer.Deserialize(stringReader);

        return supplierDto; 
    }

    // May be not use
    public IEnumerable<T> DeserializeCollection<T>(string inputXml, string rootName)
    {
        // Create XmlRootAtribute 
        XmlRootAttribute xmlRoot = new XmlRootAttribute(rootName);

        // XmlSerializer = XmlSerializer and XmlDeserializer
        XmlSerializer xmlSerializer =
            new XmlSerializer(typeof(T[]), xmlRoot);

        // Convert string to Stream!!! 
        using StringReader stringReader = new StringReader(inputXml);

        T[] supplierDto =
            (T[])xmlSerializer.Deserialize(stringReader);

        return supplierDto;
    }

    // Serialize<ExportDto>(ExportDto, rootName)
    public string Serialize<T>(T obj, string rootName)
    {
        StringBuilder sb = new StringBuilder();

        XmlRootAttribute xmlRoot =
            new XmlRootAttribute(rootName);
        XmlSerializer xmlSerializer =
            new XmlSerializer(typeof(T), xmlRoot);

        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
        namespaces.Add(string.Empty, string.Empty);

        using StringWriter writer = new StringWriter(sb);
        xmlSerializer.Serialize(writer, obj, namespaces);

        return sb.ToString().TrimEnd();
    }
}
