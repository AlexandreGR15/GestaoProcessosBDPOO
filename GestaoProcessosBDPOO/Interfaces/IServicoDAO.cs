using GestaoProcessosBDPOO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Interfaces
{
    internal interface IServicoDAO:Idao<Servico>
    {
        Servico BuscarID(int servicoID);
    }
}
