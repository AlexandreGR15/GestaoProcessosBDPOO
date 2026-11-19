using GestaoProcessosBDPOO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Interfaces
{
    internal interface IPessoaJuridicaDao:Idao<PessoaJuridica>
    {
        PessoaJuridica BuscarID(int FK_clienteID);
    }
}
