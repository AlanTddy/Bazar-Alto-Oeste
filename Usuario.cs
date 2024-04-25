using System;

namespace BazarDoAltoOeste
{
    public class Usuario
    {
        public int Id { get ; private set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Senha { get; set; }
        public Endereco Endereco { get; set; }
        public Contato Contato { get; set; }

        public Usuario(string cpf, string nome, int idade, string senha, Endereco endereco, Contato contato)
        {
            Id = GerarId();
            Cpf = cpf;
            Nome = nome;
            Idade = idade;
            Senha = senha;
            Endereco = endereco;
            Contato = contato;
        }
        
        private int GerarId(){
            return new Random().Next(0001, 9999);
        }
    }
}
