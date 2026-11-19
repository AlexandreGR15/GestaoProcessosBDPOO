using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Models
{
    internal class PessoaFisica
    {
        public int FK_clienteID { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public DateOnly? DataNasc { get; set; }
    }
}
