using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace TRAB_N2_ESTR_BRUNO_PAULO
{
    class ComparaCliente : IComparer<Clientes>
    {/// <summary>
    /// Compara Cliente pelo CPF
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
        public int Compare(Clientes x, Clientes y)
        {
            return x.Cpf.CompareTo(y.Cpf);
        }

        /// <summary>
        /// Compara Cliente pelo Nome
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare2(Clientes x, Clientes y)
        {
            return x.Nome.CompareTo(y.Nome); ;
        }
    }
}
