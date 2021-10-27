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
    }
}