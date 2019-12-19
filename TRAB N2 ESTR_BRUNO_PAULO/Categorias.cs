using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace TRAB_N2_ESTR_BRUNO_PAULO
{
    public class Categorias
    {
        // •	Categorias.txt
        //codigo|descricao

        public UInt16 Codigo { get; set; }
        public string Descricao { get; set; }       

        public double TotalVendaPorCategoria { get; set; }
        

        public override string ToString()
        {
            return Descricao + "|" + Codigo + "|" + TotalVendaPorCategoria.ToString("c2");
        }
        



    }
}
