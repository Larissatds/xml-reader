using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingXML.Domain.Entities
{
    [Table("TB_XML_EXTRACT")]
    public class XMLExtract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_XML_EXTRACT")]
        public decimal IdXMLExtract { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "O Número da Nota deve ter no máximo 20 caracteres")]
        [Column("NUM_NOTA")]
        public string NumeroNota { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ do Prestador deve ter 14 dígitos")]
        [Column("CNPJ_PRESTADOR")]
        public string CNPJPrestador { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ do Tomador deve ter 14 dígitos")]
        [Column("CNPJ_TOMADOR")]
        public string CNPJTomador { get; set; }

        [Required]
        [Column("DATA_EMISSAO", TypeName = "date")]
        public DateTime DataEmissao { get; set; }

        [Required]
        [StringLength(int.MaxValue)]
        [Column("DESCRICAO_SERVICO")]
        public string DescricaoServico { get; set; }

        [Required]
        [Column("VALOR_TOTAL")]
        public decimal ValorTotal { get; set; }
    }
}