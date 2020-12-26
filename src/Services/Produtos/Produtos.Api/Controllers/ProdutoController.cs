using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Produtos.Api.Produtos.Commands;
using Produtos.Application.Produtos.Commands;
using Produtos.Application.Produtos.Interfaces;
using System;
using System.Threading.Tasks;

namespace Produtos.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBaseApi
    {
        private readonly IProdutoAppService _produtoAppService;

        public ProdutoController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastroProduto([FromForm] ProdutoCreateCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ErrorMessage(ModelState));

            var result = await _produtoAppService.CadastrarProduto(new CreateProdutoCommand(
                command.Nome,
                command.Valor,
                new ProdutoImagem(command.Imagem)
                ));

            if (result == null) return BadRequest("Não foi possível cadastrar o produto.");

            return Created(string.Empty, result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PesquisarProduto(string nome)
        {
            var result = await _produtoAppService.PesquisarProdutos(nome);     

            if (result == null) return NotFound("Nenhum resultado encontrado.");
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarProduto([FromBody] UpdateProdutoCommand updateProdutoCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ErrorMessage(ModelState));

            var result = await _produtoAppService.AtualizarProduto(updateProdutoCommand);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("{id}")]
        public async Task<IActionResult> ExcluirProduto(Guid id)
        {
            if (id == null || id == Guid.Empty) return BadRequest("Id inválido");

            var result = await _produtoAppService.ExcluirProduto(id);

            if (!result) return BadRequest($"Não foi possível excluir o produto {id}");

            return Ok("Produto excluído com sucesso");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}/imagem/{idImagem}")]
        public async Task<IActionResult> ImagensProduto(Guid id, long idImagem)
        {
            if (id == null || id == Guid.Empty) return BadRequest("Id inválido");

            var file = await _produtoAppService.ImagemProduto(id, idImagem);
            if (file == null) return NotFound();

            return File(file, "image/jpeg");
        }
    }
}