using System;
using System.Collections.Generic;
using System.Text;
using GestaoProcessosBDPOO.Models;

namespace GestaoProcessosBDPOO.Interfaces
{
    internal interface IProcessoDao:Idao<Processo> //Interface específica da Classe Processo
    {
        Processo BuscarID(int processoID);
    }
}
