using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Models
{
    internal class Movimentacao
    {
        public int MovimentacaoID { get; set; }
        public DateTime DataHora { get; set; }
        public string? Descricao { get; set; }
        public int FK_processoID { get; set; }
        public int FK_usuarioID { get; set; }
    }
}
