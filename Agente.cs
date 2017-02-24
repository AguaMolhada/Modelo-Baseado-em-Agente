using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo_Baseado_em_Agente
{
    class Agente
    {
        public int x;                               //Pos X mundo
        public int y;                               //Pos y mundo
        protected int RaioPercepcao;                //Distancia da "visao' do agente
        public int velocidade;                      //Velocidade do Thread.Sleep() em milisegundos;
        public int[] posAgente = new int[2];        //Posicao do outro Agente;
        private int tentativas = 0;                 //Pro AndarAleatorio Nao entrar em loop infinito
        public enum Estado                          //Estado para decidir qual acao devera ser tomada
        {
            andando,
            outro,
        }
        public Estado myEstado;
                                                    //Metodo para decidir qual acao tomar. baseado nos estados
        public virtual void Decidir()
        {
            Console.WriteLine("Movendo");
        }

        protected void AndarAleatorio()             //Andar aleatoriamente pelo mapa. Gera um numero Random entre -1 e 1. 
        {                                           //Checa se a posicao e valida, se for valida Movimenta ele
            Random rnd = new Random();
            int[] escolha = new int[2];
            escolha[0] = rnd.Next(-1, 2);
            escolha[1] = rnd.Next(-1, 2);

            if (Mundo.ChecarDentroBorda(this, escolha)) 
            {
                if (Mundo.MoverAgente(this, x + escolha[0], y + escolha[1]) == null)
                {
                    x += escolha[0];
                    y += escolha[1];
                    return;
                }
            }
            else if(tentativas < 10)                //Se a posicao nao for valida tentar novamente outra nova posicao
            {                                       //10 no caso seria o numero maximo de tentativas
                tentativas++;                       //Isto foi feito para que o metodo nao entre num loop infinito
                AndarAleatorio();
            }
            else
            {                                       //Depois que o Agente movimentou o contador de tentativas zera
                tentativas = 0;
            }
        }

        //Metodo para achar a posicao do outro agente no mundo e mudar o Estado caso achei alguem
        protected int[] PosOutroAgente(Type outro)      
        {
            //para checar as posicoes no grid onde o Agente vai enxergar
            int[] xRaio = new int[2];
            xRaio[0] = x - RaioPercepcao;
            xRaio[1] = x + RaioPercepcao;
            xRaio[0] = Program.Clamp(xRaio[0], 0, Mundo.mundo.GetLength(0));
            xRaio[1] = Program.Clamp(xRaio[1], 0, Mundo.mundo.GetLength(0));

            int[] yRaio = new int[2];
            yRaio[0] = y - RaioPercepcao;
            yRaio[1] = y + RaioPercepcao;
            yRaio[0] = Program.Clamp(yRaio[0], 0, Mundo.mundo.GetLength(1));
            yRaio[1] = Program.Clamp(yRaio[1], 0, Mundo.mundo.GetLength(1));

            for (int i = 0; i < Mundo.mundo.GetLength(0); i++)
            {
                for (int j = 0; j < Mundo.mundo.GetLength(1); j++)
                {
                    if (Mundo.mundo[i,j] != null && Mundo.mundo[i, j].GetType() == outro)
                    {
                        if (i > xRaio[0] && i < xRaio[1] && j > yRaio[0] && j < yRaio[1])
                        {
                            myEstado = Estado.outro;            //Se achou um outro agente o estado muda para outro
                            return new int[] { i, j };          //e retorna a posicao na qual o agente foi visto
                        }
                    }
                }

            }

            myEstado = Estado.andando;                          //Se nao achou ninguem fica andando aleatoriamente
            return null;                                        //e retorna vazio
        }
    }
}
