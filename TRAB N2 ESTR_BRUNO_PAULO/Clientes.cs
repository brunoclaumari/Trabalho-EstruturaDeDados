using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRAB_N2_ESTR_BRUNO_PAULO
{
    class Clientes
    {
        /*•	Clientes.txt
            cpf | nome
            */
        public string Cpf { get; set; }
        public string Nome { get; set; }

        //public Clientes()
        //{

        //}
        public override string ToString()
        {
            return Cpf + "|" + Nome;
        }
    }
}
