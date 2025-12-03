using ReadingXML.Application.DTOs;
using ReadingXML.Application.Interfaces;
using ReadingXML.Domain.Entities;
using ReadingXML.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace ReadingXML.Application.Services
{
    public class XMLExtractService : IXMLExtractService
    {
        private readonly IXMLExtractRepository _repository;
        private readonly IDeserializeXMLService _deserializeXMLService;

        public XMLExtractService(IXMLExtractRepository repository, IDeserializeXMLService deserializeXMLService)
        {
            _repository = repository;
            _deserializeXMLService = deserializeXMLService;
        }

        public async Task AddAsync()
        {
            //Extrair XML
            var dtos = new List<XMLExtractDTO>();
            dtos.Add(_deserializeXMLService.DeserializeXml<XMLExtractDTO>("../ReadingXML.API/Files/nota_fiscal_01.xml"));
            dtos.Add(_deserializeXMLService.DeserializeXml<XMLExtractDTO>("../ReadingXML.API/Files/nota_fiscal_02.xml"));
            dtos.Add(_deserializeXMLService.DeserializeXml<XMLExtractDTO>("../ReadingXML.API/Files/nota_fiscal_teste.xml"));

            foreach (var dto in dtos)
            {
                dto.Prestador.CNPJ = Regex.Replace(dto.Prestador.CNPJ, @"\D", "");
                dto.Tomador.CNPJ = Regex.Replace(dto.Tomador.CNPJ, @"\D", "");

                // Validações
                await this.Validations(dto);

                // Preenche o objeto e registra na base de dados
                var xml = new XMLExtract
                {
                    IdXMLExtract = dto.IdXMLExtract,
                    NumeroNota = dto.NumeroNota,
                    CNPJPrestador = dto.Prestador.CNPJ,
                    CNPJTomador = dto.Tomador.CNPJ,
                    DataEmissao = dto.DataEmissao,
                    DescricaoServico = dto.Servico.Descricao,
                    ValorTotal = dto.Servico.ValorTotal
                };

                await _repository.AddAsync(xml);

                dto.IdXMLExtract = xml.IdXMLExtract;
            }
        }

        public async Task<PaginacaoResult<XMLExtractDTO>> GetAllPagedAsync(PaginacaoRequest request, string? numNota)
        {
            var result = await _repository.GetAllPagedAsync(request, numNota);

            return new PaginacaoResult<XMLExtractDTO>
            {
                Items = result.Items.Select(x => new XMLExtractDTO
                {
                    IdXMLExtract = x.IdXMLExtract,
                    NumeroNota = x.NumeroNota,
                    Prestador = new PrestadorDto() { CNPJ = x.CNPJPrestador },
                    Tomador = new TomadorDto() { CNPJ = x.CNPJTomador },
                    DataEmissao = x.DataEmissao,
                    Servico = new ServicoDto() { 
                        Descricao = x.DescricaoServico,
                        ValorTotal = x.ValorTotal
                    }
                }),
                TotalRegistros = result.TotalRegistros,
                NumeroPagina = result.NumeroPagina,
                TamanhoPagina = result.TamanhoPagina
            };
        }

        private async Task<(bool valido, XMLExtract xml)> Validations(XMLExtractDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("A entidade XMLExtract é inválida.");

            if(string.IsNullOrEmpty(dto.NumeroNota))
                throw new ArgumentException("O número da nota é obrigatório.");

            if (string.IsNullOrEmpty(dto.Prestador.CNPJ))
                throw new ArgumentException("O CNPJ do Prestador é obrigatório.");
            if (dto.Prestador.CNPJ.Length != 14)
                throw new ArgumentException("O CNPJ deve ter no máximo 14 dígitos.");

            if (string.IsNullOrEmpty(dto.Tomador.CNPJ))
                throw new ArgumentException("O CNPJ do Tomador é obrigatório.");
            if (dto.Tomador.CNPJ.Length != 14)
                throw new ArgumentException("O CNPJ deve ter no máximo 14 dígitos.");

            if(dto.DataEmissao == DateTime.MinValue)
                throw new ArgumentException("A data de emissão é obrigatória.");

            if(dto.Servico.ValorTotal <= 0)
                throw new ArgumentException("O valor total do serviço deve ser maior que zero.");

            return (true, new XMLExtract());
        }
    }
}
