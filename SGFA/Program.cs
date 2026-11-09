
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SGFA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Lista de veículos
            List<Veiculo> veiculos = new List<Veiculo>(100)
            {
                new Veiculo("ABC-1234", 50)
            };

            // Variáveis usadas
            string opcao_carro, opcao_frota, mat;
            int deposito = 0, escolha_veiculo = 0;

            veiculos[0].ReservaAtingida += AlertaReserva;

            do
            {

                // Menu da frota
                Tabela_Frota();

                // Solicitar a opção do usuário
                Console.Write("Escolha uma opção: ");
                opcao_frota = Console.ReadLine();

                // Executar a ação escolhida
                switch (opcao_frota)
                {
                    //selecionar veículo
                    case "1":
                        {
                            

                            if (veiculos.Count == 0)
                            {
                                Console.WriteLine("ERRO: Não existem veículos disponíveis.");
                                Console.ReadKey();
                                break;
                            }

                            Console.WriteLine("\n-----Veículos-----");

                            // Listar veículos disponíveis
                            for (int i = 0; i < veiculos.Count; i++)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine($"{i + 1} - Veículo");
                                Console.ResetColor();
                                Console.WriteLine($"Matrícula - {veiculos[i].Matricula}\nDepósito - {veiculos[i].CapacidadeDeposito}\n\n");
                            }

                            // Escolher veículo
                            do
                            {
                                Console.Write("\nEscolha o numero do veiculo: ");

                                // Validar a escolha do veículo
                                if (!int.TryParse(Console.ReadLine(), out escolha_veiculo) || escolha_veiculo < 1 || escolha_veiculo > veiculos.Count)
                                {
                                    Console.WriteLine("ERRO: Escolha um veículo válido.");
                                    // Continuar o loop para solicitar uma escolha válida
                                    continue;
                                }
                                // Ajustar o índice para corresponder à lista (0-based)
                                escolha_veiculo--;
                                // Sair do loop se a escolha for válida
                                break;
                                

                            } while (true);

                            // Menu do veículo
                            do
                            {
                                // Menu do veículo 
                                Tabela_Carro();

                                Console.Write("Escolha uma opção: ");
                                opcao_carro = Console.ReadLine();

                                // Executar a ação escolhida
                                switch (opcao_carro)
                                {
                                    //consultar veículo
                                    case "1":
                                        // Exibir informações do veículo selecionado
                                        veiculos[escolha_veiculo].ExibirInformacoes();
                                        break;

                                    //abastecer veículo
                                    case "2":
                                        Console.Write("\nQuantos litros pretende abastecer? ");

                                        if (double.TryParse(Console.ReadLine(), out double litros)) 
                                        {
                                            veiculos[escolha_veiculo].Abastecer(litros);
                                        }
                                        else { 
                                        Console.WriteLine("ERRO: Introduza um valor numérico válido.");
                                        }

                                        break;

                                    //realizar viagem
                                    case "3":
                                        Console.Write("\nQuantos quilómetros pretende percorrer? ");

                                        if (int.TryParse(Console.ReadLine(), out int kmDistancia))
                                        {
                                            veiculos[escolha_veiculo].RealizarViagem(kmDistancia);
                                        }
                                        else
                                        {
                                            Console.WriteLine("ERRO: Introduza um número inteiro válido.");
                                        }

                                        break;

                                    //sair do menu do veículo
                                    case "4":
                                        Console.WriteLine("\nA sair do menu do veículo...");
                                        break;

                                    //opção inválida
                                    default:
                                        Console.WriteLine("\nOpção inválida.");
                                        break;
                                }

                                if (opcao_carro != "4")
                                {
                                    Console.WriteLine("\nPrima uma tecla para continuar...");
                                    Console.ReadKey();
                                }

                            } while (opcao_carro != "4");

                            break;
                        }
                    //adicionar veículo
                    case "2":
                            bool valido = false;

                            
                            if (veiculos.Count >= 100)
                            {
                                Console.WriteLine("ERRO: A frota atingiu a capacidade máxima de 100 veículos.");
                                break; // Sair do case sem adicionar um novo veículo
                            }

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n---Adicionar veículo à frota---");
                            Console.ResetColor();

                            do
                            {
                                Console.Write("Matrícula: ");
                                mat = Console.ReadLine();

                                if (mat == null || mat == "")
                                {
                                    Console.WriteLine("ERRO: Matrícula não pode ser vazia.\n");
                                    continue;
                                }

                                if (veiculos.Any(v => v.Matricula == mat))
                                {
                                    Console.WriteLine("ERRO: Matrícula já existente.\n");
                                    continue;
                                }

                                Console.Write("Depósito (litros): ");
                                

                            if (int.TryParse(Console.ReadLine(), out deposito) && deposito > 0)
                            {
                                valido = true;
                            }
                            else
                            {
                                Console.WriteLine("ERRO: Introduza um número inteiro superior a 0.\n");
                                continue;
                            }
                                
                            } while (valido != true);
                            
                            veiculos.Add(new Veiculo(mat, deposito));
                            veiculos[veiculos.Count - 1].ReservaAtingida += AlertaReserva;
                            Console.WriteLine("Veículo adicionado com sucesso!");
                            break;

                    //remover veículo
                    case "3":
                        if (veiculos.Count == 0)
                        {
                            Console.WriteLine("ERRO: Não existem veículos para remover.");
                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine("\n-----Veículos-----");

                        for (int i = 0; i < veiculos.Count; i++)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"{i + 1} - Veículo");
                            Console.ResetColor();
                            Console.WriteLine($"Matrícula - {veiculos[i].Matricula}\nDepósito - {veiculos[i].CapacidadeDeposito}\n\n");
                        }

                        do
                        {
                            Console.Write("\nEscolha o numero do veiculo: ");

                            if (!int.TryParse(Console.ReadLine(), out int indice) || indice < 1 || indice > veiculos.Count)
                            {
                                Console.WriteLine("ERRO: Escolha um veículo válido.");
                                continue;
                            }

                            veiculos.RemoveAt(indice - 1);

                            Console.WriteLine("Veículo removido com sucesso!");
                            break;

                        } while (true);

                        break;

                    //quantos veículos
                    case "4":
                        Console.WriteLine($"\nA quantidade atual de veículos é: {veiculos.Count}/100");
                        break;

                    //sair
                    case "5":
                        Console.WriteLine("\nA sair do programa...");
                        break;

                    //opção inválida
                    default:
                        Console.WriteLine("\nOpção inválida.");
                        break;
                }

                if (opcao_frota != "5" && opcao_frota != "1")
                {
                    Console.WriteLine("\nPrima uma tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcao_frota != "5");
        }




        // Tabelas de opções  daqui para baixo


        // Tabela da frota
        static void Tabela_Frota()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("======================================");
            Console.WriteLine("       SISTEMA DE GESTÃO DE FROTA");
            Console.WriteLine("======================================");
            Console.ResetColor();

            Console.WriteLine("1 - Selecionar veículo");
            Console.WriteLine("2 - Adicionar veículo");
            Console.WriteLine("3 - Remover veículo");
            Console.WriteLine("4 - Quantos veiculos");
            Console.WriteLine("5 - Sair");
            Console.WriteLine("======================================");
        }

        // Tabela do carro
        static void Tabela_Carro()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("======================================");
            Console.WriteLine("       SISTEMA DE GESTÃO DO VEÍCULO");
            Console.WriteLine("======================================");
            Console.ResetColor();

            Console.WriteLine("1 - Consultar veículo");
            Console.WriteLine("2 - Abastecer veículo");
            Console.WriteLine("3 - Realizar viagem");
            Console.WriteLine("4 - Sair");

            Console.WriteLine("======================================");
        }

        // Método para receber o evento
        static void AlertaReserva(object sender, EventArgs e)
        {
            // Cast do sender para Veiculo
            Veiculo veiculo = (Veiculo)sender;

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("\n======================================");
            Console.WriteLine("          ALERTA DE RESERVA!!!");
            Console.WriteLine("======================================");
            Console.WriteLine("Matricula: " + veiculo.Matricula);
            Console.WriteLine("Combustível abaixo de 10%!");
            Console.WriteLine("É necessário abastecer!");

            Console.ResetColor();
        }
    }
}