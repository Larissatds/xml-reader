using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingXML.Application.DTOs;
using ReadingXML.Application.Interfaces;
using ReadingXML.Domain.Entities;

namespace ReadingXML.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class XMLExtractController : ControllerBase
    {
        private readonly IXMLExtractService _xmlExtractService;

        public XMLExtractController(IXMLExtractService xmlExtractService)
        {
            _xmlExtractService = xmlExtractService;
        }

        [HttpGet("Read")]
        public async Task<IActionResult> ReadXML()
        {
            await _xmlExtractService.AddAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaged([FromBody] XMLExtractRequest xml)
        {
            var result = await _xmlExtractService.GetAllPagedAsync(new PaginacaoRequest
            {
                NumeroPagina = xml.NumeroPagina,
                TamanhoPagina = xml.TamanhoPagina
            }, xml.NumeroNota);

            return Ok(result);
        }
    }
}
