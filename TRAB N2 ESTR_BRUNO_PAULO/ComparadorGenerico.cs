using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRAB_N2_ESTR_BRUNO_PAULO
{
    class ComparadorGenerico : IComparer
    {
        public ComparadorGenerico()
        {
        }
        /// <summary>
        /// COMPARA VALORES DOUBLE
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            double a = Convert.ToDouble(x);
            double b = Convert.ToDouble(y);
            return a.CompareTo(b);
        }
        /// <summary>
        /// COMPARA VALOR DOUBLE DE UM KEYVALUE DE CHAVE STRING,
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare2(KeyValuePair<string, double> x, KeyValuePair<string, double> y)
        {
            return x.Value.CompareTo(y.Value);
        }
        /// <summary>
        /// COMPARA OS VALORES STRINGS DO KEYVALUE
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare3(KeyValuePair<string, string> x, KeyValuePair<string, string> y)
        {
            return x.Value.CompareTo(y.Value);
        }
        /// <summary>
        /// COMPARA POR CODIGO STRING
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare4(string x, string y)
        {            
            return x.CompareTo(y);
        }
        /// <summary>
        /// COMPARA DATA TIPO DATETIME
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare5(DateTime x, DateTime y)
        {
            return x.CompareTo(y);
        }
        /// <summary>
        /// COMPARA A CHAVE DATA TIPO DATETIME DE KEY VALUE
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare6(KeyValuePair<DateTime, double> x, KeyValuePair<DateTime, double> y)
        {
            return x.Key.CompareTo(y.Key);
        }
        /// <summary>
        /// COMPARA VALOR DOUBLE DE KEY VALUE
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare7(KeyValuePair<uint, double> x, KeyValuePair<uint, double> y)
        {
            return x.Value.CompareTo(y.Value);
        }
    }
}
