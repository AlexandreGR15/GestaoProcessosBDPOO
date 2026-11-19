using GestaoProcessosBDPOO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Interfaces
{
    internal interface IRuralDao:Idao<Rural>
    {
        Rural BuscarID(int FK_imovelID);
    }
}
