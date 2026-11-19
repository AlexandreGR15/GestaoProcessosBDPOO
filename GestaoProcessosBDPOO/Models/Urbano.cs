using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Models
{
    internal class Urbano
    {
        public int FK_imovelID { get; set; }
        public string Setor { get; set; } = string.Empty;
        public string Quadra { get; set; } = string.Empty;
        public string Lote { get; set; } = string.Empty;
        public decimal? AreaMtQuadrado { get; set; }
    }
}
