using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.nLinhas; i++)
            {
                Console.Write($"{8-i} ");
                for (int j = 0; j < tab.nColunas; j++)
                {
                        imprimirPeca(tab.peca(i, j));
                }
                Console.WriteLine("");
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void imprimirTabuleiro(Tabuleiro tab, bool [,] posicoesPossiveis)
        {
            ConsoleColor fundoInicial = Console.BackgroundColor;
            ConsoleColor fundoModificado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.nLinhas; i++)
            {
                Console.Write($"{8-i} ");
                for (int j = 0; j < tab.nColunas; j++)
                {
                    if(posicoesPossiveis[i,j])
                    {
                        Console.BackgroundColor = fundoModificado;
                    }
                    imprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoInicial;
                }
                Console.WriteLine("");
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void imprimirPeca(Peca p)
        {
            if (p == null)
                {
                    Console.Write("- ");
                }
            else
            {
                if(p.cor == Cor.Branca)
                {
                    Console.Write(p);
                }
                else
                {
                    ConsoleColor previousColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(p);
                    Console.ForegroundColor = previousColor;
                }
                Console.Write(" ");
            }
       }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
    }
}
