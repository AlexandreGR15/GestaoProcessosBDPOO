using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Models
{
    internal class Documento
    {
        public int DocumentoID { get; set; }
        public string NomeArquivo { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public DateOnly DataEmissao { get; set; }
        public DateOnly? DataValidade { get; set; }
        public DateOnly? DataRecebimento { get; set; }
        public SituacaoDocumento Situacao { get; set; } = SituacaoDocumento.PENDENTE; 
        public int FK_processoID { get; set; }
    }
}
