namespace ReadingXML.Application.Interfaces
{
    public interface IDeserializeXMLService
    {
        T DeserializeXml<T>(string xmlPath) where T : class;
    }
}
