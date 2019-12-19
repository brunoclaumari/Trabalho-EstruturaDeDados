using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRAB_N2_ESTR_BRUNO_PAULO
{
    class ComparaCategorias : IComparer<Categorias>
    {
        /// <summary>
        /// COMPARA CATEGORIA PELO CODIGO
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Categorias x, Categorias y)
        {
            return x.Codigo.CompareTo(y.Codigo);
        }
        /// <summary>
        /// COMPARA CATEGORIA PELA DESCRICAO
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare2(Categorias x, Categorias y)
        {
            return x.Descricao.CompareTo(y.Descricao);
        }
    }
}
