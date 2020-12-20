using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Produtos.Api.Produtos.Commands
{
    public class ProdutoCreateCommand
    {
        [Required(ErrorMessage = "Nome do produto obrigatório")]
        public string Nome { get; set; }
        [Range(1, 9999999.99, ErrorMessage = "Valor do produto deve ser maior que 0")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "Imagem do produto obrigatória")]
        public IFormFile Imagem { get; set; }
    }
}