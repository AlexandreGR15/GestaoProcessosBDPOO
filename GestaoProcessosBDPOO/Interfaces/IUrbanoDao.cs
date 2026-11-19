using GestaoProcessosBDPOO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Interfaces
{
    internal interface IUrbanoDao:Idao<Urbano>
    {
        Urbano BuscarID(int FK_imovelID);
    }
}
