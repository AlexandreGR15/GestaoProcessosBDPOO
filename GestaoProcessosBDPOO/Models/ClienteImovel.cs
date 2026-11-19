using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Models
{
    internal class ClienteImovel
    {
        public int VinculoID { get; set; }
        public decimal Percentual { get; set; }
        public TipoVinculo TipoVinculo { get; set; }
        public int FK_clienteID { get; set; }
        public int FK_imovelID { get; set; }
    }
}
