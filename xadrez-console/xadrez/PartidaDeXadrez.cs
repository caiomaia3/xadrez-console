using System;
using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            xeque = false;

            colocarPecas();
        }

        public Peca executaMovimento(Posicao pOrigem, Posicao pDestino)
        {
            Peca p = tab.retirarPeca(pOrigem);
            p.incrementarQtdMovimentos();
            Peca pcCapturada = tab.retirarPeca(pDestino);
            tab.colocarPeca(p, pDestino);
            if(pcCapturada != null)
            {
                capturadas.Add(pcCapturada);
            }
            return pcCapturada;
        }

        public void realizaJogada(Posicao pOrigem, Posicao pDestino)
        {
            Peca pcCapturada = executaMovimento(pOrigem,pDestino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(pOrigem,pDestino,pcCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }
            if (estaEmXeque(corAdversaria(jogadorAtual)))
            {
               xeque = true; 
            }
            else
            {
                xeque = false;
            }

            if(estaEmXequeMate(corAdversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                mudarJogador();
            }

        }   

        public void desfazMovimento(Posicao pOrigem, Posicao pDestino, Peca pcCapturada)
        {
            Peca p = tab.retirarPeca(pDestino);
            if(pcCapturada != null)
            {
                tab.colocarPeca(pcCapturada,pDestino);
                capturadas.Remove(pcCapturada);
            }
            tab.colocarPeca(p,pOrigem);
            p.decrementarQtdMovimentos();
        }

        public void validarPosicaoOrigem(Posicao p)
        {
            if (tab.peca(p) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (tab.peca(p).cor != jogadorAtual)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(p).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimento possíveis para a peça escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao pOrigem, Posicao pDestino)
        {
            if (!tab.peca(pOrigem).podeMoverPara(pDestino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        public void mudarJogador()
        {
            if(jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> pecaNaCor = new HashSet<Peca>();
            foreach(Peca p in capturadas)
            {
                if(p.cor == cor) pecaNaCor.Add(p);
            }
            return pecaNaCor;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca p in pecas)
            {   
                if(p.cor == cor) aux.Add(p);
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }


        private Cor corAdversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca pegaRei(Cor cor)
        {
            foreach (Peca p in pecasEmJogo(cor))
            {
                if (p is Rei)
                {
                    return p;
                }
            }
            return null;
        }
        public bool estaEmXeque(Cor cor){
            Peca rei = pegaRei(cor);
            if(rei == null)
            {
                throw new TabuleiroException("Não tem rei na " + cor + " no tabuleiro!");
            }

            foreach(Peca p in pecasEmJogo(corAdversaria(cor)))
            {
                if(p.movimentosPossiveis()[rei.posicao.linha, rei.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool estaEmXequeMate(Cor cor)
        {
            if(estaEmXeque(cor))
            {
                if(!podeCancelarXeque(cor))
                {
                    return true;
                }
            }
           return false;
        }

        public bool podeCancelarXeque(Cor cor){
            foreach (Peca p in pecasEmJogo(cor))
            {
                bool [,] eMovimentoPossivel = p.movimentosPossiveis();
                for (int i = 0; i < tab.nLinhas; i++)
                {
                    for (int j = 0; j < tab.nColunas; j++)
                    {
                        if(eMovimentoPossivel[i,j])
                        {
                            Posicao pOrigem = p.posicao;
                            Posicao pDestino = new Posicao(i,j);
                            Peca pcCapturada = executaMovimento(pOrigem,pDestino);
                            bool xequeCancelado = !estaEmXeque(cor);
                            desfazMovimento(pOrigem,pDestino,pcCapturada);
                            if (xequeCancelado)
                            {
                                return true;
                            }
                        }
                    }
                    
                }   
            }
            return false;
        }


        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna,linha).toPosicao());
            pecas.Add(peca);
        }
        public void colocarPecas()
        {

            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));
            colocarNovaPeca('h', 7, new Torre(tab, Cor.Branca));

            colocarNovaPeca('a', 8, new Rei(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Torre(tab, Cor.Preta));

            // colocarNovaPeca('c',1,new Torre(tab,Cor.Branca));
            // colocarNovaPeca('c',2,new Torre(tab,Cor.Branca));
            // colocarNovaPeca('d',2,new Torre(tab,Cor.Branca));
            // colocarNovaPeca('e',1,new Torre(tab,Cor.Branca));
            // colocarNovaPeca('e',2,new Torre(tab,Cor.Branca));
            // colocarNovaPeca('d',1,new Rei(tab,Cor.Branca));

            // colocarNovaPeca('c',8,new Torre(tab,Cor.Preta));
            // colocarNovaPeca('c',7,new Torre(tab,Cor.Preta));
            // colocarNovaPeca('d',7,new Torre(tab,Cor.Preta));
            // colocarNovaPeca('e',8,new Torre(tab,Cor.Preta));
            // colocarNovaPeca('e',7,new Torre(tab,Cor.Preta));
            // colocarNovaPeca('d',8,new Rei(tab,Cor.Preta));


        }
    }
}
