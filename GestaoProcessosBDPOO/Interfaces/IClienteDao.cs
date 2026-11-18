using System;
using System.Collections.Generic;
using System.Text;
using GestaoProcessosBDPOO.Models;

namespace GestaoProcessosBDPOO.Interfaces
{
    internal interface IClienteDao:Idao<Cliente>
    {
        Cliente BuscarID(int clienteID);
    }
}
