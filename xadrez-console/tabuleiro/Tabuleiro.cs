using tabuleiro;
namespace tabuleiro
{
    class Tabuleiro
    {
        public int nLinhas { get; set; }
        public int nColunas { get; set; }
        private Peca[,] pecas;

        public Tabuleiro(int nLinhas, int nColunas)
        {
            this.nLinhas = nLinhas;
            this.nColunas = nColunas;
            this.pecas = new Peca[nLinhas, nColunas];
        }

        public Peca peca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }
        public Peca peca(Posicao pos)
        {
            return pecas[pos.linha, pos.coluna];
        }

        public void colocarPeca(Peca p, Posicao pos)
        {
            if (existePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nesta posição.");
            }
            p.posicao = pos;
            pecas[pos.linha, pos.coluna] = p;
        }

        public bool existePeca(Posicao pos)
        {
            validarPosicao(pos);
            return this.pecas[pos.linha,pos.coluna] != null;
        }

        public bool posicaoValida(Posicao pos)
        {
            if (pos.linha>=0 && pos.linha<this.nLinhas && pos.coluna>=0 && pos.coluna<this.nColunas )
            {
                return true;
            }
            return false;
        }

        public void validarPosicao(Posicao pos)
        {
            if (!posicaoValida(pos))
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }
    }
}