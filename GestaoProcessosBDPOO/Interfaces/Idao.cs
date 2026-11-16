using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoProcessosBDPOO.Interfaces
{
    internal interface Idao<T> //Interface genérica
    {
        void Insert(T t);
        void Update(T t);
        void Delete(int id);
        List<T> GetAll();
    }
}
