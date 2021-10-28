using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao p = new Posicao(3, 4);
            Tabuleiro tab = new Tabuleiro(8, 8);

            tab.ColocarPeca(new Torre(tab,Cor.Preta), new Posicao(0, 0));
            tab.ColocarPeca(new Torre(tab,Cor.Preta), new Posicao(1, 3)); 
            tab.ColocarPeca(new Rei(tab,Cor.Preta), new Posicao(2, 4));


            Tela.imprimirTabuleiro(tab);
            Console.WriteLine("Posição: " + p);
            Console.ReadLine();
        }
    }
}
