using GestaoProcessosBDPOO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Interfaces
{
    internal interface IImovelDao:Idao<Imovel>
    {
        Imovel BuscarID(int imovelID);
    }
}
