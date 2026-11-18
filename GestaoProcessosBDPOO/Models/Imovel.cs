using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Models
{
    internal class Imovel
    {
        public int ImovelID { get; set; }
        public string Denominacao { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public string? Endereco { get; set; }
        public string Matricula { get; set; } = string.Empty;
    }
}
