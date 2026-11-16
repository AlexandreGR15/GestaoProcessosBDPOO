using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Models
{
    internal class Processo
    {
        public int ProcessoId { get; set; }
        public string NumProtocolo { get; set; } = string.Empty; //conferir string.Empty
        public DateOnly DataAbertura { get; set; }
        public DateOnly? DataPrazo { get; set; }
        public DateOnly? DataConclusao { get; set; } //conferir ?
        public string Status { get; set; } = string.Empty;
        public int FK_clienteID { get; set; }
        public int FK_servicoID { get; set; }
        public int FK_usuarioID { get; set; }
        public int FK_imovelID { get; set; }
    }
}
