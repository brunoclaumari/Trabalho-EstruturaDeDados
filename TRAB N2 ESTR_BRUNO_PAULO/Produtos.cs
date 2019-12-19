using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace TRAB_N2_ESTR_BRUNO_PAULO
{
    public class Produtos
    {
        /*
        //•Produtos.txt
        //codigo | preco | descricao | categoria | datacadastro no formato yyyyMMddHHmmss
        */
        public UInt16 Codigo { get; set; }
        public double Preco { get; set; }
        public string Descricao { get; set; }
        public UInt16 Categoria { get; set; }
        public DateTime DataCadastro { get; set; }
        public UInt16 Quantidade { get; set; }

        public double Total()
        {
            return Preco * Quantidade;
        }
        public override string ToString()
        {
            return Descricao + "|" + Codigo + "|" + Quantidade + "|" + Total();
        }

        public string MaiorQuant()
        {
            return Descricao + "|" + Total();
        }
    }
}
