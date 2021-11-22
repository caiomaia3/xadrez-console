using tabuleiro;
using System;

namespace xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez partida;
        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            ushort codePoint = 0x2654;
            return $"{(char)codePoint}";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;
        }

        private bool torrePodeRoque(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p is Torre && p.cor == cor && p.qtdMovimentos ==0;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.nLinhas,tab.nColunas];

            Posicao pos = new Posicao(0,0);

            //acima
            pos.definirValores(posicao.linha-1,posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            //ne
            pos.definirValores(posicao.linha-1,posicao.coluna+1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            // direita
            pos.definirValores(posicao.linha,posicao.coluna+1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            //se
            pos.definirValores(posicao.linha+1,posicao.coluna+1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            //sul
            pos.definirValores(posicao.linha+1,posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            } 
            //so
            pos.definirValores(posicao.linha+1,posicao.coluna-1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            //esquerda
            pos.definirValores(posicao.linha,posicao.coluna-1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            //no
            pos.definirValores(posicao.linha-1,posicao.coluna-1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }

            // #Jogada especial roque
            if(qtdMovimentos==0 && !partida.xeque)
            {
                // # Jogada especial roque pequeno
                Posicao posTorre1 = new Posicao(posicao.linha,posicao.coluna+3);
                if(torrePodeRoque(posTorre1))
                {
                    Posicao pos1 = new Posicao(posicao.linha,posicao.coluna+1);
                    Posicao pos2 = new Posicao(posicao.linha, posicao.coluna+2);
                    if(tab.peca(pos1) == null && tab.peca(pos2) == null)
                    {
                        mat[pos2.linha,pos2.coluna] = true;
                    }
                }
                // # Jogada especial roque pequeno
                Posicao posTorre2 = new Posicao(posicao.linha,posicao.coluna-4);
                if(torrePodeRoque(posTorre2))
                {
                    Posicao pos1 = new Posicao(posicao.linha,posicao.coluna-1);
                    Posicao pos2 = new Posicao(posicao.linha, posicao.coluna-2);
                    Posicao pos3 = new Posicao(posicao.linha, posicao.coluna-3);
                    if(tab.peca(pos1) == null && tab.peca(pos2) == null && tab.peca(pos3) == null)
                    {
                        mat[pos2.linha,pos2.coluna] = true;
                    }
                }
            }


            return mat;
        }
    }
}