using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Models
{
    internal class Processo
    {
        public int ProcessoID { get; set; }
        public string NumProtocolo { get; set; } = string.Empty; //serve para as propriedades n ficarem nulas, alternativa mais simples
        public DateOnly DataAbertura { get; set; }
        public DateOnly? DataPrazo { get; set; }
        public DateOnly? DataConclusao { get; set; } //? = serve para permitir que aceite valores nulos
        public StatusProcesso Status { get; set; } = StatusProcesso.ABERTO;
        public int FK_clienteID { get; set; }
        public int FK_servicoID { get; set; }
        public int FK_usuarioID { get; set; }
        public int FK_imovelID { get; set; }
    }
}
