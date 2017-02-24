using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Modelo_Baseado_em_Agente
{
    static class Mundo
    {
        public static Random random = new Random();
        public static Agente[,] mundo = new Agente[15, 15];
        public static int velocidade;


        // Checa se a posicao do agente a + a pos esta dentro do mundo se for valida retorna true se nao false
        public static bool ChecarDentroBorda(Agente a,int[] pos)
        {
            if (a.x + pos[0] >= 0 && a.x + pos[0] < mundo.GetLength(0) && a.y + pos[1] >= 0 && a.y + pos[1] < mundo.GetLength(1))
            {
                return true;
            }
            return false;
        }

        // Adiciona e retorna o agente a na posicao x,y se ela estivar vazia se nao retorna quem esta na posicao
        public static Agente MoverAgente(Agente a,int x,int y)
        {
            if (mundo[x, y] == null)
            {
                mundo[x, y] = a;
                mundo[a.x, a.y] = null;
                return null;
            }
            else
            {
                return mundo[x,y];
            }
        }

        // Imprime o mundo no console se a posicao estiver vazia bota - se tiver a presa O e o predador #
        public static void imprimirMundo()
        {
            while (true)
            {
                Thread.Sleep(velocidade);
                Console.Clear();
                for (int i = 0; i < mundo.GetLength(0); i++)
                {
                    for (int j = 0; j < mundo.GetLength(1); j++)
                    {
                        if (mundo[i, j] == null)
                        {
                            Console.Write(" - ");
                        }
                        else if (mundo[i, j].GetType() == typeof(Presa))
                        {
                            Console.Write(" O ");
                        }
                        else if (mundo[i, j].GetType() == typeof(Predador))
                        {
                            Console.Write(" # ");
                        }
                    }
                    Console.Write("\n\r");
                }
            }
        }
    }
}
