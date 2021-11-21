using tabuleiro;

namespace xadrez
{
    class Dama : Peca
    {
        public Dama(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro,cor)
        {
        }

        public override string ToString()
        {
            ushort codePoint = 0x2655;
            return $"{(char)codePoint}";
        }
        
        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool [,] mat = new bool[tab.nLinhas,tab.nColunas];

            Posicao pos = new Posicao(0,0);
            //NO
            pos.definirValores(posicao.linha-1,posicao.coluna-1);
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                   break; 
                } 
                pos.linha--;
                pos.coluna--;
            }
            //NE
            pos.definirValores(posicao.linha-1,posicao.coluna+1);
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                   break; 
                } 
                pos.linha--;
                pos.coluna++;
            }
            //SO
            pos.definirValores(posicao.linha+1,posicao.coluna-1);
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                   break; 
                } 
                pos.linha++;
                pos.coluna--;
            }
            //SE
            pos.definirValores(posicao.linha+1,posicao.coluna+1);
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                   break; 
                } 
                pos.linha++;
                pos.coluna++;
            }
            //acima
            pos.definirValores(posicao.linha-1,posicao.coluna);
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                   break; 
                } 
                pos.linha = pos.linha - 1;
            }

            //abaixo
            pos.definirValores(posicao.linha+1,posicao.coluna);
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                   break; 
                }
                pos.linha++;
            }

            //direita 
            pos.definirValores(posicao.linha,posicao.coluna+1);
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                   break; 
                }
                pos.coluna++;
            }

            //esquerda 
            pos.definirValores(posicao.linha,posicao.coluna-1);
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                   break; 
                }
                pos.coluna--;
            }



        return mat;
        }
    }
}