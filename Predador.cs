using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Modelo_Baseado_em_Agente
{
    class Predador : Agente
    {

        //Construtor da classe Predador onde 'r' e o raio de percepcao do agente e o 'v' o intervalo de tempo em milisegundos na qual a 
        //thread vai esperar.
        public Predador(int r,int v)
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
                posAgente = PosOutroAgente(typeof(Presa));
                switch (myEstado)
                {
                    case Estado.andando:
                        AndarAleatorio();
                        break;
                    case Estado.outro:
                        Perseguir(posAgente);
                        break;
                    default:
                        break;
                }
            }
        }
    

        //Metoto para a predador perseguir a presa onde se passa como parametro a posicao da presa no mundo
        //para que o agente possa decidir para onde deverar ir (no caso ir para cima da presa)
        protected void Perseguir(int[] presa)
        {
            int[] pos = new int[2];             //Pos na qual o Predador ira ir para tentar pegar a Presa 
                                                // (1 vai movimentar o Predaor para direita e -1 para a esquerda) eixo x
                                                // (1 vai movimentar o Predaor para baixo e -1 para a cima) eixo y
            int[] dist = new int[2];            //Distancia entre o Predador e a presa na qual o [0] e em x e o [1] em y
            dist[0] = x - presa[0];             
            dist[1] = y - presa[1];

            
            if (dist[0] != 0)                   //Se a distancia em x for diferente de 0 a posicao na qual o Predador vai ir sera 1
            {                                   // mas se a distancia for menor que 0 sera -1 
                pos[0] = 1;                    
                if (dist[0] > 0)
                {
                    pos[0] = -pos[0];
                }
            }      
            if(dist[0] == 0)                    //Se a Distancia em x for igual a 0 ele n precisa movimentar neste eixo
            {                                   
                pos[0] = 0;
            }

            if (dist[1] != 0)                   //Se a distancia em y for diferente de 0 a posicao na qual o Predador vai ir sera 1
            {                                   // mas se a distancia for menor que 0 sera -1
                pos[1] = dist[1] / dist[1];     
                if(dist[1] > 0)
                {
                    pos[1] = -pos[1];
                }
            }   
            if(dist[1] == 0)                    //Se a Distancia em y for igual a 0 n precisa movimentar neste eixo
            {
                pos[1] = 0;
            }

            if (Mundo.ChecarDentroBorda(this, pos))
            {
                Agente posMundo = Mundo.MoverAgente(this, x + pos[0], y + pos[1]);
                if (posMundo == null)           //Se nao tiver nenhum Agente na posicao adicionar este agente la
                {
                    x += pos[0];
                    y += pos[1];
                    return;
                }
            }

        }
    }
}
