using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRAB_N2_ESTR_BRUNO_PAULO
{
    class Vendas
    {
        /*
         Vendas.txt
            Codigo da venda | cliente | produto  | dataVenda no formato ("yyyyMMddHHmmss")
         */

        public uint CodigoVenda { get; set; }
        public string Cliente { get; set; }
        public UInt16 Produto { get; set; }
        public DateTime DataVenda { get; set; }
        //Produtos aux;
        //double valorInicio;
        public double ValorDaVenda { get; set; }        
          
              

        //public void ValorEnviado(Produtos prod)
        //{
        //    aux = prod;
        //}
        //public double TotalDasVendas()
        //{
        //    return valorInicio += aux.Preco;
        //}

        public override string ToString()
        {
            return CodigoVenda + "|" + Cliente;
        }
    }
}
