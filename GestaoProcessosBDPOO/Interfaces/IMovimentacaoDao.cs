using GestaoProcessosBDPOO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Interfaces
{
    internal interface IMovimentacaoDao:Idao<Movimentacao>
    {
        Movimentacao BuscarID(int movimentacaoID);
    }
}
