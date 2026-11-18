using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Models
{
    internal class Cliente
    {
        public int ClienteID { get; set; }
        public string? Telefone { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateOnly DataCadastro { get; set; }
    }
}
