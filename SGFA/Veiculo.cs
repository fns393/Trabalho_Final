using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFA
{
    internal class Veiculo
    {
        //Atributos
        private int _quilometragem = 0;
        private double _nivelCombustivel;
        public string Matricula { get; set; }
        public int CapacidadeDeposito { get; set; }

        //Evento para notificar quando o nível de combustível atingir a reserva
        public event EventHandler ReservaAtingida;


        //Construtor
        public Veiculo(string matricula, int capacidadeDeposito)
        {
            Matricula = matricula;
            CapacidadeDeposito = capacidadeDeposito;
            _nivelCombustivel = CapacidadeDeposito;
        }

        //Validação de dados
        public int Quilometragem 
        { 
            get { return _quilometragem; }

            // Validação para garantir que a quilometragem não diminui
            set
            {
                if (value < _quilometragem)
                {
                    Console.WriteLine("ERRO: A quilometragem não pode diminuir.");
                }
                else if (value < 0)
                {
                    Console.WriteLine("ERRO: A quilometragem não pode ser negativa.");
                }
                else
                {
                    _quilometragem = value;
                }
            }
        }

        public double NivelCombustivel
        {
            get { return _nivelCombustivel; }

            // Validação para garantir que o nível de combustível não é negativo e não excede a capacidade do depósito
            set
            {
                if (value < 0 || value > CapacidadeDeposito)
                {
                    Console.WriteLine("O nível de combustível não pode ser negativo ou exceder a capacidade do depósito.");
                }
                else
                {
                    _nivelCombustivel = value;

                    if (_nivelCombustivel < 0.1 * CapacidadeDeposito)
                    {
                        ReservaAtingida?.Invoke(this, EventArgs.Empty);
                    }
                }

                
            }
        }

        public void Abastecer(double litros)
        {
            // Validação para garantir que a quantidade de litros é positiva e não excede a capacidade do depósito
            if (litros <= 0)
            {
                Console.WriteLine("ERRO: A quantidade de litros tem de ser superior a zero.");
            }
            else if (NivelCombustivel + litros > CapacidadeDeposito)
            {
                Console.WriteLine("AVISO: A quantidade ultrapassa a capacidade do depósito.");

                NivelCombustivel = CapacidadeDeposito;

                Console.WriteLine("Depósito atestado até à capacidade máxima.");
            }
            else
            {
                NivelCombustivel += litros;

                Console.WriteLine("Abastecimento realizado com sucesso!");
            }
        }

        public bool RealizarViagem(int kmDistancia)
        {
            // Validação para garantir que a distância é positiva
            if (kmDistancia <= 0)
            {
                Console.WriteLine("ERRO: A distância tem de ser superior a zero.");
                return false;
            }

            double consumo = kmDistancia * 0.07;

            // Validação para garantir que há combustível suficiente para a viagem
            if (NivelCombustivel >= consumo)
            {
                Quilometragem += kmDistancia;

                Console.WriteLine("Viagem realizada com sucesso!");
                Console.WriteLine($"Distância percorrida: {kmDistancia} km");
                Console.WriteLine($"Combustível consumido: {consumo} litros");

                NivelCombustivel -= consumo;
                return true;
            }
            else
            {
                Console.WriteLine("ERRO: Combustível insuficiente para realizar a viagem.");

                return false;
            }
        }


        // Método para exibir informações do veículo
        public void ExibirInformacoes()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n--- INFORMAÇÕES DO VEÍCULO ---");
            Console.ResetColor();

            Console.WriteLine("Matrícula: " + Matricula);
            Console.WriteLine("Capacidade do depósito: " + CapacidadeDeposito + " litros");
            Console.WriteLine("Quilometragem: " + Quilometragem + " km");
            Console.WriteLine("Combustível atual: " + NivelCombustivel + " litros");
        }
    }
}