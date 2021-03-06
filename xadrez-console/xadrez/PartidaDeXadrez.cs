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
        public Peca vulneravelEnPassant { get; private set;}

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            xeque = false;
            vulneravelEnPassant = null;

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

            // #Jogadaespecial roque pequeno
            if(p is Rei && pDestino.coluna == (pOrigem.coluna+2))
            {
                Posicao torreOrigem = new Posicao(pOrigem.linha,pOrigem.coluna+3);
                Posicao torreDestino = new Posicao(pOrigem.linha,pOrigem.coluna+1);
                Peca t = tab.retirarPeca(torreOrigem);
                t.incrementarQtdMovimentos();
                tab.colocarPeca(t,torreDestino);
            }
            // #jogadaespecial roque grande
            if(p is Rei && pDestino.coluna == (pOrigem.coluna-2))
            {
                Posicao torreOrigem = new Posicao(pOrigem.linha,pOrigem.coluna-4);
                Posicao torreDestino = new Posicao(pOrigem.linha,pOrigem.coluna-1);
                Peca t = tab.retirarPeca(torreOrigem);
                t.incrementarQtdMovimentos();
                tab.colocarPeca(t,torreDestino);
            }

            // #jogada especial en passant
            if (p is Peao && pDestino.coluna != pOrigem.coluna && pcCapturada == null)
            {
                Posicao peaoPosicao;
                if (p.cor == Cor.Branca)
                {
                    peaoPosicao = new Posicao(pDestino.linha +1, pDestino.coluna);
                }
                else
                {
                    peaoPosicao = new Posicao(pDestino.linha -1, pDestino.coluna);
                }
                pcCapturada = tab.retirarPeca(peaoPosicao); 
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

            Peca p = tab.peca(pDestino);
            // #jogadaespecial promoção
            if(p is Peao)
            {
                bool chegouCasaPromocao = (p.cor == Cor.Branca && pDestino.linha ==0) || (p.cor == Cor.Preta && pDestino.linha == 7);
                p = tab.retirarPeca(pDestino);
                pecas.Remove(p);
                Peca dama = new Dama(tab,p.cor);
                tab.colocarPeca(dama,pDestino);
                pecas.Add(dama);
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

            // #jogadaespecial en passant
            bool andouDuasCasas = Math.Abs(pDestino.linha - pOrigem.linha) == 2; 

            if(p is Peao && andouDuasCasas)
            {
               vulneravelEnPassant = p; 
            }
            else
            {
                vulneravelEnPassant = null;
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

            // #jogadaespecial roque pequeno
            if(p is Rei && pDestino.coluna == (pOrigem.coluna+2))
            {
                Posicao torreOrigem = new Posicao(pOrigem.linha,pOrigem.coluna+3);
                Posicao torreDestino = new Posicao(pOrigem.linha,pOrigem.coluna+1);
                Peca t = tab.retirarPeca(torreDestino);
                t.decrementarQtdMovimentos();
                tab.colocarPeca(t,torreOrigem);
            }
            // #jogadaespecial roque grande 
            if(p is Rei && pDestino.coluna == (pOrigem.coluna-2))
            {
                Posicao torreOrigem = new Posicao(pOrigem.linha,pOrigem.coluna-4);
                Posicao torreDestino = new Posicao(pOrigem.linha,pOrigem.coluna-1);
                Peca t = tab.retirarPeca(torreDestino);
                t.decrementarQtdMovimentos();
                tab.colocarPeca(t,torreOrigem);
            }
            // #jogada especial en passant
            if(p is Peao)
            {
                if(pOrigem.coluna != pDestino.coluna && pcCapturada == vulneravelEnPassant)
                {
                    Peca peao = tab.retirarPeca(pDestino);
                    Posicao peaoPosicao;
                    if (p.cor == Cor.Branca)
                    {
                        peaoPosicao = new Posicao(3,pDestino.coluna);
                    }
                    else
                    {
                        peaoPosicao = new Posicao(4,pDestino.coluna);
                    }
                    tab.colocarPeca(peao,peaoPosicao);
                }
            }

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
            if (!tab.peca(pOrigem).movimentoPossivel(pDestino))
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
            // https://pt.wikipedia.org/wiki/S%C3%ADmbolos_de_xadrez_em_Unicodebool 
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca,this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca,this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca,this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca,this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca,this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca,this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca,this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca,this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca,this));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta,this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta,this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta,this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta,this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta,this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta,this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta,this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta,this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta,this));
        }
    }
}
