using Microsoft.EntityFrameworkCore;
using ReadingXML.Data.Context;
using ReadingXML.Domain.Entities;
using ReadingXML.Domain.Interfaces;

namespace ReadingXML.Data.Repositories
{
    public class XMLExtractRepository : IXMLExtractRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<XMLExtract> _dbSet;

        public XMLExtractRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<XMLExtract>();
        }

        public async Task AddAsync(XMLExtract entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginacaoResult<XMLExtract>> GetAllPagedAsync(PaginacaoRequest request, string? numNota)
        {
            var items = await _dbSet
                            .Where(x => string.IsNullOrEmpty(numNota) || x.NumeroNota.Contains(numNota))
                            .OrderBy(x => x.IdXMLExtract)
                            .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                            .Take(request.TamanhoPagina)
                            .ToListAsync();

            return new PaginacaoResult<XMLExtract>
            {
                Items = items,
                TotalRegistros = items.Count(),
                NumeroPagina = request.NumeroPagina,
                TamanhoPagina = request.TamanhoPagina
            };
        }
    }
}
