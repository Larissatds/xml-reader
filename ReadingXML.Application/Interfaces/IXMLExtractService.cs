using ReadingXML.Application.DTOs;
using ReadingXML.Domain.Entities;

namespace ReadingXML.Application.Interfaces
{
    public interface IXMLExtractService
    {
        Task<PaginacaoResult<XMLExtractDTO>> GetAllPagedAsync(PaginacaoRequest request, string? numNota);
        Task AddAsync();
    }
}
