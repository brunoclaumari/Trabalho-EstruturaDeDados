using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRAB_N2_ESTR_BRUNO_PAULO
{
    class ComparaKeyValuePair : IComparer<KeyValuePair<DateTime, double>>
    {
        /// <summary>
        /// COMPARA PELA CHAVE DATETIME DO KEYVALUE
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(KeyValuePair<DateTime, double> x, KeyValuePair<DateTime, double> y)
        {
            return (x.Key).CompareTo(y.Key);
        }
        /// <summary>
        /// COMPARA PELO VALOR DOUBLE DO KEYVALUE
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare2(KeyValuePair<DateTime, double> x, KeyValuePair<DateTime, double> y)
        {
            return x.Value.CompareTo(y.Value);
        }



    }
}
