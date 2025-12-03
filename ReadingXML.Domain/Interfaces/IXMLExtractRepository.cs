using ReadingXML.Domain.Entities;

namespace ReadingXML.Domain.Interfaces
{
    public interface IXMLExtractRepository
    {
        Task<PaginacaoResult<XMLExtract>> GetAllPagedAsync(PaginacaoRequest request, string? numNota);
        Task AddAsync(XMLExtract entity);
    }
}
