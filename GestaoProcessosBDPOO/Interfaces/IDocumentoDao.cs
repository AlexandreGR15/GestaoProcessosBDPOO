using GestaoProcessosBDPOO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Interfaces
{
    internal interface IDocumentoDao:Idao<Documento>
    {
        Documento BuscarID(int documentoID);
    }
}
