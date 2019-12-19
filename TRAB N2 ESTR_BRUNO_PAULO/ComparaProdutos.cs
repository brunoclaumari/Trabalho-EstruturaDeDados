using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace TRAB_N2_ESTR_BRUNO_PAULO
{
    class ComparaProdutos : IComparer<Produtos>
    {
        /// <summary>
        /// COMPARA PRODUTOS PELO CODIGO
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Produtos x, Produtos y)
        {
            return x.Codigo.CompareTo(y.Codigo);
        }
        /// <summary>
        /// COMPARA PRODUTOS PELA DESCRIÇÃO
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare2(Produtos x, Produtos y)
        {
            return x.Descricao.CompareTo(y.Descricao);
        }
        /// <summary>
        /// COMPARA POR QUANTIDADE
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare3(Produtos x, Produtos y)
        {
            return x.Quantidade.CompareTo(y.Quantidade);
        }
    }
}
