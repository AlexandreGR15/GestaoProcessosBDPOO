using GestaoProcessosBDPOO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Interfaces
{
    internal interface IClienteImovelDao:Idao<ClienteImovel>
    {
        ClienteImovel BuscarID(int vinculoID);
    }
}
