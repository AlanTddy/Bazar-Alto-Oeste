using System;

namespace BazarDoAltoOeste
{
    public class Produto
    {
        public int Id { get ; private set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int IdRelacao { get; set; }

        public Produto(string nome, string descricao, decimal preco, int idrelacao)
        {
            Id = GerarId();
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            IdRelacao = idrelacao;
        }

        private int GerarId(){
            return new Random().Next(0001, 9999);
        }
    }
}
