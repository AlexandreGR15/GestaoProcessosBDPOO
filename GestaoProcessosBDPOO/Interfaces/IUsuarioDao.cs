using GestaoProcessosBDPOO.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Interfaces
{
    internal interface IUsuarioDao:Idao<Usuario>
    {
        Usuario BuscarID(int usuarioID);
    }
}
