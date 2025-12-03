using ReadingXML.Application.Interfaces;
using System.Xml.Serialization;

namespace ReadingXML.Application.Services
{
    public class DeserializeXMLService : IDeserializeXMLService
    {
        public T DeserializeXml<T>(string xmlPath) where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (var reader = new StreamReader(xmlPath))
            {
                return (T)serializer.Deserialize(reader) as T
                    ?? throw new InvalidOperationException($"Erro ao deserializar o arquivo XML para o tipo {typeof(T).Name}");
            }
        }
    }
}
