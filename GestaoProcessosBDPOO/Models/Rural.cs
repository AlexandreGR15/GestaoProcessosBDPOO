using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Models
{
    internal class Rural
    {
        public int FK_imovelID { get; set; }
        public string CodigoIncra { get; set; } = string.Empty;
        public decimal? AreaHa { get; set; }
    }
}
