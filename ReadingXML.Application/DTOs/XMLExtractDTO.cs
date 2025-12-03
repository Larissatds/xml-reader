using ReadingXML.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ReadingXML.Application.DTOs
{
    [XmlRoot("NotaFiscal")]
    public class XMLExtractDTO
    {
        public decimal IdXMLExtract { get; set; }

        [Required]
        [XmlElement("Numero")]
        [StringLength(20, ErrorMessage = "O Número da Nota deve ter no máximo 20 caracteres")]
        public string NumeroNota { get; set; }

        [XmlElement("Prestador")]
        public PrestadorDto Prestador { get; set; }

        [XmlElement("Tomador")]
        public TomadorDto Tomador { get; set; }

        [Required]
        [XmlElement("DataEmissao")]
        public DateTime DataEmissao { get; set; }

        [XmlElement("Servico")]
        public ServicoDto Servico { get; set; }
    }

    public class PrestadorDto
    {
        [Required]
        [XmlElement("CNPJ")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ do Prestador deve ter 14 dígitos")]
        public string CNPJ { get; set; }
    }

    public class TomadorDto
    {
        [Required]
        [XmlElement("CNPJ")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ do Tomador deve ter 14 dígitos")]
        public string CNPJ { get; set; }
    }

    public class ServicoDto
    {
        [Required]
        [XmlElement("Descricao")]
        [StringLength(int.MaxValue)]
        public string Descricao { get; set; }

        [Required]
        [XmlElement("Valor")]
        public decimal ValorTotal { get; set; }
    }

    public class XMLExtractRequest : PaginacaoRequest
    {
        public string NumeroNota { get; set; }
    }
}
