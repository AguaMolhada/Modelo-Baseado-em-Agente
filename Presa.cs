using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Modelo_Baseado_em_Agente
{
    class Presa : Agente
    {
        //Construtor da classe Presa onde 'r' e o raio de percepcao do agente e o 'v' o intervalo de tempo em milisegundos na qual a 
        //thread vai esperar.
        public Presa(int r,int v)
        {
            RaioPercepcao = r;
            velocidade = v;
            myEstado = Estado.andando;
            x = Mundo.random.Next(0, Mundo.mundo.GetLength(0));
            y = Mundo.random.Next(0, Mundo.mundo.GetLength(1));

        }

        public override void Decidir()
        {
            while (true)
            {
                Thread.Sleep(velocidade);
                posAgente = PosOutroAgente(typeof(Predador));
                switch (myEstado)
                {
                    case Estado.andando:
                        AndarAleatorio();
                        break;
                    case Estado.outro:
                        Fugir(posAgente);
                        break;
                    default:
                        break;
                }

            }
        }

        protected void Fugir(int[] pred)
        {
            int[] pos = new int[2];             //Pos na qual a Presa ira ir para tentar pegar a Predador 
                                                // (1 vai movimentar o Predaor para direita e -1 para a esquerda) eixo x
                                                // (1 vai movimentar o Predaor para baixo e -1 para a cima) eixo y
            int[] dist = new int[2];            //Distancia entre a Presa e o predador na qual o [0] e em x e o [1] em y
            dist[0] = x - pred[0];

            if (dist[0] != 0)                   //Se a distancia em x for diferente de 0 a posicao na qual o Predador vai ir sera 1
            {                                   // mas se a distancia for menor que 0 sera -1 
                pos[0] = 1;
                if (dist[0] < 0)
                {
                    pos[0] = -pos[0];
                }
            }                                   //Se a distancia em x for 0 escolhe um valor aleatorio entre -1 e 1
            if (dist[0] == 0)
            {
                pos[0] = new Random().Next(-1, 2);
            }

            if (dist[1] != 0)                   //Se a distancia em y for diferente de 0 a posicao na qual o Predador vai ir sera 1
            {                                   // mas se a distancia for menor que 0 sera -1
                pos[1] = 1;
                if (dist[1] < 0)
                {
                    pos[1] = -pos[1];
                }
            }                                   //Se a distancia em y for 0 escolhe um valor aleatorio entre -1 e 1
            if (dist[1] == 0)
            {
                pos[1] = new Random().Next(-1, 2);
            }


            if (Mundo.ChecarDentroBorda(this, pos))
            {
                //Checa se podera movimentar para a nova posiçao
                if (Mundo.MoverAgente(this, x + pos[0], y + pos[1]) == null)
                {
                    x += pos[0];
                    y += pos[1];
                    return;
                }
                //se nao for possivel tente posicao atual em x e a nova posicao em y
                else if(Mundo.MoverAgente(this, x, y + pos[1]) == null)
                {
                    y += pos[1];
                    return;
                }
                //se nao for possivel tente nova posicao em x e posicao atual em y
                else if(Mundo.MoverAgente(this,x+pos[0], y) == null)
                {
                    x += pos[0];
                    return;
                }
            }
            //se nenhuma das opcoes funcionar ande aleatoriamente lugar
            else
            {
               AndarAleatorio();
            }
        }
    }
}