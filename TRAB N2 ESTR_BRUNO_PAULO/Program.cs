using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace TRAB_N2_ESTR_BRUNO_PAULO
{
    class Program
    {
        #region//Metodos para instanciar objetos
        static Categorias InstanciaCategoria(string linhaRefer)
        {
            Categorias categ = new Categorias()
            {
                Codigo = Convert.ToUInt16(linhaRefer.Split('|')[0]),
                Descricao = linhaRefer.Split('|')[1]
            };
            return categ;
        }
        static Clientes InstanciaClientes(string linhaRef)
        {
            Clientes client = new Clientes()
            {
                Cpf = (linhaRef.Split('|')[0]),
                Nome = (linhaRef.Split('|')[1].Trim())
            };
            return client;
        }
        static Produtos InstanciaProdutos(string linha)
        {
            string data = linha.Split('|')[4];
            data = (data.Substring(6, 2) + "/" +
                    data.Substring(4, 2) + "/" +
                    data.Substring(0, 4));

            Produtos prod = new Produtos()
            {
                Codigo = Convert.ToUInt16(linha.Split('|')[0]),
                Preco = Convert.ToDouble(linha.Split('|')[1]),
                Descricao = linha.Split('|')[2],
                Categoria = Convert.ToUInt16(linha.Split('|')[3]),
                DataCadastro = Convert.ToDateTime(data)
            };
            return prod;
        }
        static Vendas InstanciaVenda(string linhaRefer)
        {
            Vendas venda = new Vendas();
            //int pos = (linhaRefer.IndexOf('|'));

            venda.CodigoVenda = Convert.ToUInt32(linhaRefer.Substring(0, (linhaRefer.IndexOf('|'))));
            linhaRefer = linhaRefer.Remove(0, linhaRefer.Substring(0, (linhaRefer.IndexOf('|')) + 1).Length);

            venda.Cliente = linhaRefer.Substring(0, (linhaRefer.IndexOf('|')));
            linhaRefer = linhaRefer.Remove(0, linhaRefer.Substring(0, (linhaRefer.IndexOf('|')) + 1).Length);

            venda.Produto = Convert.ToUInt16(linhaRefer.Substring(0, (linhaRefer.IndexOf('|'))));
            linhaRefer = linhaRefer.Remove(0, linhaRefer.Substring(0, (linhaRefer.IndexOf('|')) + 1).Length);
            //linhaRefer.Substring(6, 2) - Substring Excluida!!! colocada como 1
            venda.DataVenda = Convert.ToDateTime(("01" + "/" +
                    linhaRefer.Substring(4, 2) + "/" +
                    linhaRefer.Substring(0, 4)));
            //ValorTotalVendaPorCliente(dicioProdutos, dicioQuantVendaPorCliente, venda);

            return venda;
        }
        #endregion

        #region //Metodo que testa se nome de cliente repete
        private static void TestaNomeClienteRepetido(ref Dictionary<string, string> dicNomeCli, ref HashSet<string> listRep, string nome)
        {
            bool repetido = false;
            if (dicNomeCli.ContainsKey(nome))
            {
                repetido = true;
            }
            else
                dicNomeCli[nome] = nome;
            if (repetido && !listRep.Contains(nome))
            {
                listRep.Add(nome);
            }
        }
        #endregion

        #region//Metodo que testa se produto é valido
        private static bool EhProdutoValido(Dictionary<UInt16, Categorias> TestaCateg,
            UInt16 categParaTeste)
        {
            bool valido = false;
            if (TestaCateg.ContainsKey(categParaTeste))
                valido = true;
            return valido;
        }
        #endregion


        static void Main(string[] args)
        {

            #region//Texto de Estrutura dos arquivos
            /*           

            •	Categorias.txt
             codigo|descricao

            •	Produtos.txt
            codigo | preco | descricao | categoria | datacadastro no formato yyyyMMddHHmmss

            •	Clientes.txt
            cpf | nome
            •	Vendas.txt
            Codigo da venda | cliente | produto  | dataVenda no formato ("yyyyMMddHHmmss")
             */
            #endregion

            DateTime inicio = DateTime.Now;
            string comeca = inicio.ToString("HH:mm:ss");
            string termina = "";
            //Dicionários para guardar valores principais

            Dictionary<UInt16, Categorias> dicCateg = new Dictionary<UInt16, Categorias>();
            Dictionary<string, Clientes> dicClientes = new Dictionary<string, Clientes>();
            Dictionary<string, string> dicNomeCliente = new Dictionary<string, string>();
            Dictionary<UInt16, Produtos> dicProdutos = new Dictionary<UInt16, Produtos>();
            //Hashset, Listas e Dicionarios auxiliares para opcodes
            HashSet<string> hashClientNomeRepet = new HashSet<string>();
            List<Clientes> listaTotalCLientes = new List<Clientes>();
            List<Produtos> listaTotalProdutos = new List<Produtos>();
            Dictionary<uint, Vendas> dicVendasNaoReptCod = new Dictionary<uint, Vendas>();
            Dictionary<uint, Vendas> dicVendasNaoReptProduto = new Dictionary<uint, Vendas>();
            Dictionary<DateTime, double> dicVendasPorData = new Dictionary<DateTime, double>();
            Dictionary<string, double> dicQuantVendaPorCliente = new Dictionary<string, double>();
            Dictionary<uint, double> dicMaiorValorDeVenda = new Dictionary<uint, double>();
            //Variáveis
            UInt16 produtoRepetido = 0;
            UInt16 clienteForaDaVenda = 0;
            UInt16 produtoForaDaVenda = 0;
            UInt16 categoriaNaoVendida = 0;

            ComparaCliente comparaClient = new ComparaCliente();
            ComparaProdutos comparaProduto = new ComparaProdutos();
            ComparaEmVenda comparaEmVenda;
            #region//Le o arquivo categorias
            using (StreamReader leitor = new StreamReader("categorias.txt"))
            {
                Categorias ct;
                string linha;
                while ((linha = leitor.ReadLine()) != null)
                {
                    ct = InstanciaCategoria(linha);
                    if (dicCateg.ContainsKey(ct.Codigo))
                        continue;
                    else
                        dicCateg[ct.Codigo] = ct;
                }
            }
            #endregion

            #region//Le o arquivo Clientes
            using (StreamReader leitor = new StreamReader("clientes.txt"))
            {
                string linha;
                int existe = 0;

                Clientes client;
                while ((linha = leitor.ReadLine()) != null)
                {
                    client = InstanciaClientes(linha);

                    if (dicClientes.ContainsKey(client.Cpf))
                        existe++;
                    else
                    {
                        dicClientes[client.Cpf] = client;
                        listaTotalCLientes.Add(client);
                        TestaNomeClienteRepetido(ref dicNomeCliente, ref hashClientNomeRepet, client.Nome.ToString());
                    }
                }
            }
            #endregion

            //ordena lista pelo CPF
            listaTotalCLientes.Sort(comparaClient.Compare);

            #region//Le o arquivo produtos
            using (StreamReader leitor = new StreamReader("produtos.txt"))
            {
                string linha;
                /*
                 •	Produtos.txt
                codigo | preco | descricao | categoria | datacadastro no formato yyyyMMddHHmmss
                 */
                Produtos product;

                while ((linha = leitor.ReadLine()) != null)
                {
                    product = InstanciaProdutos(linha);
                    if (EhProdutoValido(dicCateg, product.Categoria))
                    {
                        if (!dicProdutos.ContainsKey(product.Codigo))
                        {
                            dicProdutos[product.Codigo] = product;
                            listaTotalProdutos.Add(product);
                        }
                        else
                        {
                            produtoRepetido++;
                        }
                    }
                }
            }
            #endregion

            //Ordena lista pelo Codigo do produto.
            listaTotalProdutos.Sort(comparaProduto.Compare);

            #region//Le o arquivo Vendas
            using (StreamReader leitor = new StreamReader("vendas.txt"))
            {
                string linha;
                /*
                 •	Vendas.txt
                  Codigo da venda | cliente | produto  | dataVenda no formato ("yyyyMMddHHmmss")
                 */

                Vendas Vendendo;

                while ((linha = leitor.ReadLine()) != null)
                {

                    if (dicClientes.ContainsKey(linha.Split('|')[1]) &&
                        dicProdutos.ContainsKey(Convert.ToUInt16(linha.Split('|')[2])))
                    {
                        Vendendo = InstanciaVenda(linha);
                        //Salva as vendas em um dicionário usando código como chave
                        if (!dicVendasNaoReptCod.ContainsKey(Vendendo.CodigoVenda))
                        {
                            Vendendo.ValorDaVenda = dicProdutos[Vendendo.Produto].Preco;
                            dicVendasNaoReptCod[Vendendo.CodigoVenda] = Vendendo;
                        }
                        else
                        {
                            dicVendasNaoReptCod[Vendendo.CodigoVenda].ValorDaVenda += dicProdutos[Vendendo.Produto].Preco;
                        }
                        //Adiciona no Dicionário para guardar vendas sem repetir o produto
                        if (!dicVendasNaoReptProduto.ContainsKey(Vendendo.Produto))
                            dicVendasNaoReptProduto[Vendendo.Produto] = Vendendo;

                        //Adiciona a quantidade de venda por cliente
                        if (dicQuantVendaPorCliente.ContainsKey(Vendendo.Cliente))
                            dicQuantVendaPorCliente[Vendendo.Cliente] += dicProdutos[Vendendo.Produto].Preco;
                        else
                            dicQuantVendaPorCliente.Add(Vendendo.Cliente, dicProdutos[Vendendo.Produto].Preco);

                        //Testa o produto. Se já estiver no dicionário ele incrementa 1 na quantidade
                        if (dicProdutos.ContainsKey(Vendendo.Produto))
                        {
                            dicProdutos[Vendendo.Produto].Quantidade++;
                        }

                        //Guarda no dicionário de venda por data
                        if (dicVendasPorData.ContainsKey(Vendendo.DataVenda))
                        {
                            dicVendasPorData[Vendendo.DataVenda] += dicProdutos[Vendendo.Produto].Preco;
                        }
                        else
                            dicVendasPorData.Add(Vendendo.DataVenda, dicProdutos[Vendendo.Produto].Preco);

                        if (!dicCateg.ContainsKey(dicProdutos[Vendendo.Produto].Categoria))
                            categoriaNaoVendida++;

                        if (dicMaiorValorDeVenda.ContainsKey(Vendendo.CodigoVenda))
                        {
                            dicMaiorValorDeVenda[Vendendo.CodigoVenda] += dicProdutos[Vendendo.Produto].Preco;
                        }
                        else
                            dicMaiorValorDeVenda[Vendendo.CodigoVenda] = dicProdutos[Vendendo.Produto].Preco;
                    }
                    else
                    {
                        continue;
                    }
                    if (!dicClientes.ContainsKey(linha.Split('|')[1]))
                        clienteForaDaVenda++;
                    if (!dicProdutos.ContainsKey(Convert.ToUInt16(linha.Split('|')[2])))
                        produtoForaDaVenda++;
                }
            }
            #endregion

            GC.Collect();

            //Procura pela categoria a qual produto pertence no diocionario de produtos
            //e soma o valor monetário total no dicionario de categorias
            foreach (Produtos prod in dicProdutos.Values)
            {
                dicCateg[prod.Categoria].TotalVendaPorCategoria += prod.Total();
            }
            //Comparador para vendas
            comparaEmVenda = new ComparaEmVenda();

            //Comparador para KeyValuesPair
            ComparaKeyValuePair comparaKeyValue = new ComparaKeyValuePair();

            //Usa o dicionario de venda por data para criar lista de venda por data
            List<KeyValuePair<DateTime, double>> listaVendaPData = dicVendasPorData.ToList();
            //Usa dicionario de venda por cliente para criar lista
            List<KeyValuePair<string, double>> ListaKeyVendasPorCliente = dicQuantVendaPorCliente.ToList();

            //Comparador com vários tipos de dados que foi preciso para ordenar
            ComparadorGenerico comparaGenerico = new ComparadorGenerico();

            #region//Escreve o arquivo resultado
            using (StreamWriter stream = new StreamWriter("resultado.txt"))
            {
                stream.WriteLine(comeca);
                stream.WriteLine("A - " + dicCateg.Count);
                stream.WriteLine("B - " + dicProdutos.Count);
                stream.WriteLine("C - " + dicClientes.Count);
                stream.WriteLine("D - " + dicVendasNaoReptCod.Count);
                stream.WriteLine("E - " + dicVendasNaoReptProduto.Count);
                stream.WriteLine("F - " + hashClientNomeRepet.Count);

                //Escreve Opcode G
                foreach (Clientes clie in listaTotalCLientes)
                {
                    stream.WriteLine("G - " + clie.ToString() + "|" +
                    dicQuantVendaPorCliente[clie.Cpf].ToString());
                }

                //Escreve Opcode H
                listaTotalProdutos.Sort(comparaProduto.Compare2);
                foreach (Produtos prod in listaTotalProdutos)
                {
                    stream.WriteLine("H - " + prod.ToString());
                }

                //Escreve Opcode I
                foreach (Categorias cat in dicCateg.Values)
                {
                    stream.WriteLine("I - " + cat.ToString());
                }

                //Escreve Opcode J

                //Ordena pelo mes e ano que está na chave do key/value        
                listaVendaPData.Sort(comparaGenerico.Compare6);
                foreach (KeyValuePair<DateTime, double> VendaPData in listaVendaPData)
                {
                    string data = (VendaPData.Key).ToShortDateString().Remove(0, 3);
                    stream.WriteLine("J - " + data + "|" + VendaPData.Value.ToString("c2"));
                }

                //Escreve Opcode K
                ListaKeyVendasPorCliente.Sort(comparaGenerico.Compare2);
                int pos = ListaKeyVendasPorCliente.Count - 1;
                double ClienteMaisGastao = ListaKeyVendasPorCliente[pos].Value;
                stream.WriteLine("K - " + dicClientes[ListaKeyVendasPorCliente[pos].Key].Nome +
                    "|" + ListaKeyVendasPorCliente[pos].Value.ToString("c2"));

                for (int k = pos - 1; k > 0; k--)
                {
                    if (ListaKeyVendasPorCliente[k].Value == ClienteMaisGastao)
                        stream.WriteLine("K - " + dicClientes[ListaKeyVendasPorCliente[k].Key].Nome +
                    "|" + ListaKeyVendasPorCliente[k].Value.ToString("c2"));
                    else
                        break;
                }

                //Escreve Opcode L
                listaTotalProdutos.Sort(comparaProduto.Compare3);
                int n = listaTotalProdutos.Count - 1;
                UInt16 quantDeProdMaisVendido = listaTotalProdutos[n].Quantidade;
                stream.WriteLine("L - " + listaTotalProdutos[n].MaiorQuant());
                for (int k = n - 1; k > 0; k--)
                {
                    if (listaTotalProdutos[k].Quantidade == quantDeProdMaisVendido)
                        stream.WriteLine("L - " + listaTotalProdutos[k].MaiorQuant());
                    else
                        break;
                }

                //Escreve Opcode M
                listaVendaPData.Sort(comparaKeyValue.Compare2);
                int m = listaVendaPData.Count - 1;
                double maiorVendaPMes = listaVendaPData[m].Value;

                stream.WriteLine("M - " + (listaVendaPData[m].Key).ToShortDateString().Remove(0, 3) +
                    "|" + listaVendaPData[m].Value.ToString("c2"));

                for (int k = m - 1; k > 0; k--)
                {
                    if (listaVendaPData[k].Value == maiorVendaPMes)
                        stream.WriteLine("M - " + (listaVendaPData[k].Key).ToShortDateString().Remove(0, 3) +
                    "|" + listaVendaPData[k].Value.ToString("c2"));
                    else
                        break;
                }

                //Escreve Opcode N

                List<KeyValuePair<uint, double>> ListaKeyVendasPorCod = dicMaiorValorDeVenda.ToList();
                ListaKeyVendasPorCod.Sort(comparaGenerico.Compare7);

                int testeN = ListaKeyVendasPorCod.Count - 1;
                double maiorVenda = ListaKeyVendasPorCod[testeN].Value;

                stream.WriteLine("N - " + ListaKeyVendasPorCod[testeN].Key + "|" +
                    dicVendasNaoReptCod[ListaKeyVendasPorCod[testeN].Key].Cliente +
                                        "|" + ListaKeyVendasPorCod[testeN].Value.ToString("c2"));

                for (int k = testeN - 1; k > 0; k--)
                {
                    if (ListaKeyVendasPorCod[k].Value == maiorVenda)
                        stream.WriteLine("N - " + dicVendasNaoReptCod[ListaKeyVendasPorCod[k].Key].Cliente + "|" +
                                         ListaKeyVendasPorCod[k].Key + "|"
                     + maiorVenda.ToString("c2"));
                    else
                        break;
                }

                stream.WriteLine("O - " + produtoForaDaVenda);//Escreve Opcode O
                stream.WriteLine("P - " + clienteForaDaVenda);//Escreve Opcode P
                stream.WriteLine("Q - " + categoriaNaoVendida);//Escreve Opcode Q

                DateTime fim = DateTime.Now;
                termina = fim.ToString("HH:mm:ss");
                stream.WriteLine(termina);
                stream.WriteLine((DateTime.Now.Subtract(inicio)).TotalSeconds);
            }
            #endregion

            TimeSpan final = DateTime.Now.Subtract(inicio);
            Console.WriteLine(final.TotalSeconds);

            Console.WriteLine("Quant vendas codigo distinto: " + dicVendasNaoReptCod.Count);
            Console.WriteLine("Quant produtos validos: " + dicProdutos.Count);
            Console.WriteLine("Quant clientes repetidos: " + hashClientNomeRepet.Count);
            Console.ReadLine();
        }
    }
}


