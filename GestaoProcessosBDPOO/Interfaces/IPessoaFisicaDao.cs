using GestaoProcessosBDPOO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Interfaces
{
    internal interface IPessoaFisicaDao:Idao<PessoaFisica>
    {
        PessoaFisica BuscarID(int FK_clienteID);
    }
}
