using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Models
{
    internal class PessoaJuridica
    {
        public int FK_clienteID { get; set; }
        public string Cnpj { get; set; } = string.Empty;
        public string RazaoSocial { get; set; } = string.Empty;
    }
}
