using Microsoft.AspNetCore.Http;

namespace Produtos.Application.Produtos.Commands
{
    public class CreateProdutoCommand
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public ProdutoImagem Imagem { get; set; }

        public CreateProdutoCommand(string nome, decimal valor, ProdutoImagem imagem)
        {
            Nome = nome;
            Valor = valor;
            Imagem = imagem;
        }
    }

    public class ProdutoImagem
    {
        public IFormFile Conteudo { get; set; }
        public ProdutoImagem(IFormFile formFile)
        {
            Conteudo = formFile;
        }
    }
}