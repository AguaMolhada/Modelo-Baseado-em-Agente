using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_Baseado_em_Agente
{
    class Program
    {
        //Cria as instancia da presa e do predador passando (raio,intervalo do thread)
        public static Predador pred = new Predador(8, 600);
        public static Presa pres = new Presa(5, 300);

        static void Main(string[] args)
        {
            //Cria o Thread e passa o metodo Decidir para ficar rodando
            Thread predThread = new Thread(new ThreadStart(pred.Decidir));
            Thread presThread = new Thread(new ThreadStart(pres.Decidir));
            //Usa o Thread para imprimir o mundo (poderia ser feito de outra maneira so que assim ficou mais limpo o codigo
            Thread imprimirMundo = new Thread(new ThreadStart(Mundo.imprimirMundo));
            //Seta a o intervalo do Thread 
            Mundo.velocidade = 300;

            //Inicializa os Thread
            predThread.Start();
            presThread.Start();
            imprimirMundo.Start();

            Console.ReadKey();
        }

        // Usado para verificar se o value esta entre o min e o max e retorna Resultado;
        public static T Clamp<T>(T value, T min, T max) where T : System.IComparable<T>
        {
            T result = value;
            if (value.CompareTo(max) > 0)
                result = max;
            if (value.CompareTo(min) < 0)
                result = min;
            return result;
        }
    }

}
