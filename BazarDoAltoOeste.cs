using System;
using System.Collections.Generic;

namespace BazarDoAltoOeste
{
    public class BazarDoAltoOeste
    {
        private static List<Produto> produtos = new List<Produto>();
        private static List<Usuario> usuarios = new List<Usuario>();
        private static Usuario usuarioLogado = null;

        public static void Main(string[] args)
        {
            MenuPrincipal();
        }

        static void MenuPrincipal()
        {
            while (true)
            {
                Console.WriteLine("=== Bem-vindo ao Bazar do Alto Oeste ===");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Cadastro");
                Console.WriteLine("3. Sair");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        Cadastro();
                        break;
                    case "3":
                        Console.WriteLine("Saindo do sistema...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        static void Login()
        {
            Contract.Requires(!string.IsNullOrEmpty(cpf), "CPF não pode ser nulo ou vazio.");
            Contract.Requires(!string.IsNullOrEmpty(senha), "Senha não pode ser nula ou vazia.");

            Console.WriteLine("=== Login ===");
            Console.Write("CPF: ");
            string cpf = Console.ReadLine();
            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            foreach (var usuario in usuarios)
            {
                if (cpf == usuario.Cpf && senha == usuario.Senha){
                    MenuUsuario(usuario);
                }
            }
            Console.WriteLine("CPF ou senha inválidos\n");
        }

        static void Cadastro()
        {
            Contract.Requires(usuario != null, "Usuário não pode ser nulo.");
            Contract.Requires(!string.IsNullOrEmpty(usuario.CPF), "CPF do usuário não pode ser nulo ou vazio.");
            Contract.Requires(!UsuariosContemCPF(usuario.CPF), "Usuário com o mesmo CPF já cadastrado.");

            Console.WriteLine("=== Cadastro ===");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Idade: ");
            int idade = int.Parse(Console.ReadLine());
            if (idade < 18){
                Console.WriteLine("Não é permitido acesso a pessoas menores de 18 anos\n");
                return;
            }
            Console.Write("CPF: ");
            string cpf = Console.ReadLine();
            Console.Write("Senha: ");
            string senha = Console.ReadLine();
            Console.Write("Cidade: ");
            string cidade = Console.ReadLine();
            Console.Write("CEP: ");
            string cep = Console.ReadLine();
            Console.Write("Rua: ");
            string rua = Console.ReadLine();
            Console.Write("Número: ");
            int numero = int.Parse(Console.ReadLine());
            Console.Write("Telefone: ");
            string telefone = Console.ReadLine();
            Console.Write("E-mail: ");
            string email = Console.ReadLine();

            Endereco endereco = new Endereco
            {
                Cidade = cidade,
                CEP = cep,
                Rua = rua,
                Numero = numero
            };

            Contato contato = new Contato
            {
                Telefone = telefone,
                Email = email
            };

            Usuario usuario = new Usuario(cpf, nome, idade, senha, endereco, contato);
            usuarios.Add(usuario);

            Console.WriteLine("Cadastro realizado com sucesso!");

            Contract.Ensures(usuarios.Contains(usuario), "O usuário cadastrado deve estar presente na lista de usuários após o cadastro.");

            Login();
        }

        static void MenuUsuario(Usuario usuario)
        {
            Contract.Requires(usuarioLogado != null, "É necessário estar logado para acessar o menu inicial.");

            usuarioLogado = usuario;
            while (true)
            {
                Console.WriteLine("=== Menu do Usuário ===");
                Console.WriteLine("1. Feed Geral");
                Console.WriteLine("2. Adicionar Produto");
                Console.WriteLine("3. Editar Produto");
                Console.WriteLine("4. Remover Produto");
                Console.WriteLine("5. Visualizar Seus Produtos");
                Console.WriteLine("6. Alterar Dados da Conta");
                Console.WriteLine("7. Logout");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        FeedGeral();
                        break;
                    case "2":
                        AdicionarProduto();
                        break;
                    case "3":
                        EditarProduto();
                        break;
                    case "4":
                        RemoverProduto();
                        break;
                    case "5":
                        VisualizarProdutos();
                        break;
                    case "6":
                        AlterarDadosConta();
                        break;
                    case "7":
                        Contract.Requires(usuarioLogado != null, "É necessário estar logado para deslogar do sistema.");

                        usuarioLogado = null;
                        Console.WriteLine("Logout realizado com sucesso.");

                        Contract.Ensures(usuarioLogado == null, "Após o logout, o usuário logado deve ser nulo.");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
            Contract.Ensures(usuarioLogado != null, "Após acessar o menu inicial, o usuário logado deve continuar logado.");
        }

        static void FeedGeral()
        {
            Contract.Requires(usuarioLogado != null, "É necessário estar logado para acessar o feed geral.");

            Console.WriteLine("=== Feed Geral ===");
            foreach (var produto in produtos)
            {
                Console.WriteLine($"Id: {produto.Id} - Nome: {produto.Nome} - Descrição: {produto.Descricao} - Preço: R${produto.Preco}\n");
            }
        }

        static void AdicionarProduto()
        {
            Contract.Requires(produto != null, "Produto não pode ser nulo.");
            Contract.Requires(produto.Id > 0, "ID do produto deve ser maior que zero.");
            Contract.Requires(!string.IsNullOrEmpty(produto.Descricao), "Descrição do produto não pode ser nula ou vazia.");
            Contract.Requires(produto.Preco > 0, "Preço do produto deve ser maior que zero.");

            Console.WriteLine("=== Adicionar Produto ===");
            Console.Write("Nome do produto: ");
            string nome = Console.ReadLine();
            Console.Write("Descrição: ");
            string descricao = Console.ReadLine();
            Console.Write("Preço: ");
            decimal preco = decimal.Parse(Console.ReadLine());
            int idRel = usuarioLogado.Id;

            Produto produto = new Produto(nome, descricao, preco, idRel);
            produtos.Add(produto);

            Console.WriteLine("Produto adicionado com sucesso!\n");

            Contract.Ensures(produtos.Contains(produto), "O produto adicionado deve estar presente na lista de produtos após a adição.");
        }

        static void RemoverProduto()
        {
            Contract.Requires(id > 0, "ID do produto deve ser maior que zero.");
            Contract.Requires(ProdutosContemId(id), "Produto com o ID especificado não encontrado.");

            Console.WriteLine("=== Remover Produto===");
            Console.WriteLine("Digite o Id do produto a remover");
            int IdRmv = int.Parse(Console.ReadLine());
            int idRel = usuarioLogado.Id;

            foreach(var produto in produtos)
            {
                if (IdRmv == produto.Id && produto.IdRelacao == idRel){
                    produtos.Remove(produto);
                    Console.WriteLine("Produto removido com sucesso\n");
                    return;
                }
            }
            Console.WriteLine("Produto nao econtrado\n");

            Contract.Ensures(!ProdutosContemId(id), "O produto removido não deve estar mais presente na lista de produtos após a remoção.");
        }

        static void EditarProduto()
        {
            Contract.Requires(id > 0, "ID do produto deve ser maior que zero.");
            Contract.Requires(!string.IsNullOrEmpty(novaDescricao), "Nova descrição do produto não pode ser nula ou vazia.");
            Contract.Requires(novoPreco > 0, "Novo preço do produto deve ser maior que zero.");
            Contract.Requires(ProdutosContemId(id), "Produto com o ID especificado não encontrado.");

            Console.WriteLine("=== Editar Produto===");
            Console.WriteLine("Digite o Id do produto a ser editado");
            int IdEdt = int.Parse(Console.ReadLine());
            int idRel = usuarioLogado.Id;

            foreach(var produto in produtos)
            {
                if (IdEdt == produto.Id && idRel == produto.IdRelacao){
                    Console.WriteLine("Novo nome para produto ");
                    string novoNome = Console.ReadLine();
                    Console.WriteLine("Nova descrição para produto ");
                    string novaDescricao = Console.ReadLine();
                    Console.WriteLine("Novo preço para produto ");
                    decimal novoPreco = decimal.Parse(Console.ReadLine());

                    produto.Nome = novoNome;
                    produto.Descricao = novaDescricao;
                    produto.Preco = novoPreco;
                    return;
                }
            }
            Console.WriteLine("Produto nao econtrado\n");

            Contract.Ensures(Contract.OldValue(produtos.Find(p => p.Id == id).Descricao) != novaDescricao || 
            Contract.OldValue(produtos.Find(p => p.Id == id).Preco) != novoPreco, "O produto editado deve ter uma nova descrição ou um novo preço após a edição.");
        }

        static void VisualizarProdutos()
        {
            Console.WriteLine("=== Seus Produtos ===");
            int IdRef = usuarioLogado.Id;
            foreach (var produto in produtos)
            {
                if(produto.IdRelacao == IdRef){
                    Console.WriteLine($"Id: {produto.Id} - Nome: {produto.Nome} - Descrição: {produto.Descricao} - Preço: R${produto.Preco}");
                }
            }
        }

        static void AlterarDadosConta()
        {
            Contract.Requires(!string.IsNullOrEmpty(novoNome), "Novo nome completo não pode ser nulo ou vazio.");
            Contract.Requires(novaIdade > 0, "Nova idade do usuário deve ser maior que zero.");
            Contract.Requires(novoEndereco != null, "Novo endereço não pode ser nulo.");
            Contract.Requires(novoContato != null, "Novo contato não pode ser nulo.");

            Console.WriteLine("=== Alterar Dados da Conta ===");
            Console.Write("Novo Nome: ");
            string novoNome = Console.ReadLine();

            Console.Write("Nova Idade: ");
            int novaIdade = int.Parse(Console.ReadLine());

            Console.Write("Novo CPF: ");
            string novoCpf = Console.ReadLine();

            Console.Write("Nova Senha: ");
            string novaSenha = Console.ReadLine();

            Console.Write("Nova Cidade: ");
            string novaCidade = Console.ReadLine();

            Console.Write("Novo CEP: ");
            string novoCEP = Console.ReadLine();

            Console.Write("Nova Rua: ");
            string novaRua = Console.ReadLine();

            Console.Write("Nova Número: ");
            int novoNumero = int.Parse(Console.ReadLine());

            Console.Write("Novo Telefone: ");
            string novoTelefone = Console.ReadLine();

            Console.Write("Nova E-mail: ");
            string novoEmail = Console.ReadLine();

            usuarioLogado.Nome = novoNome;
            usuarioLogado.Idade = novaIdade;
            usuarioLogado.Cpf = novoCpf;
            usuarioLogado.Senha = novaSenha;
            usuarioLogado.Endereco.Cidade = novaCidade;
            usuarioLogado.Endereco.CEP = novoCEP;
            usuarioLogado.Endereco.Rua = novaRua;
            usuarioLogado.Endereco.Numero = novoNumero;
            usuarioLogado.Contato.Telefone = novoTelefone;
            usuarioLogado.Contato.Email = novoEmail;

            Console.WriteLine("Dados da conta alterados com sucesso!");

            Contract.Ensures(Nome == novoNomeCompleto || Idade == novaIdade || Endereco == novoEndereco || Contato == novoContato, "Pelo menos um dos dados da conta do usuário deve ser alterado após a alteração.");
        }
    }
}
