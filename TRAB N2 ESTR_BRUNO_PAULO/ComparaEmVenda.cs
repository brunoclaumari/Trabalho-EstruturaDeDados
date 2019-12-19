using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRAB_N2_ESTR_BRUNO_PAULO
{
    class ComparaEmVenda : IComparer<Vendas>
    {
        /// <summary>
        /// COMPARA CPF'S NA VENDA
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Vendas x, Vendas y)
        {
            return x.Cliente.CompareTo(y.Cliente);
        }
        /// <summary>
        /// COMPARA DATAS COMO STRING NA VENDA
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare2(Vendas x, Vendas y)
        {
            return x.DataVenda.ToString().CompareTo(y.DataVenda.ToString());
        }
        /// <summary>
        /// COMPARA DATAS NA VENDA
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare3(Vendas x, Vendas y)
        {
            return x.DataVenda.CompareTo(y.DataVenda);
        }
        /// <summary>
        /// COMPARA OS VALORES DOUBLE DA VENDA
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare4(Vendas x, Vendas y)
        {
            return x.ValorDaVenda.CompareTo(y.ValorDaVenda);
        }


    }
}
