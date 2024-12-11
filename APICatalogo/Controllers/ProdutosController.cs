using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosRepository _repository; //Contexto com o banco de dados

        public ProdutosController(IProdutosRepository repository)
        {
            _repository = repository;
        }
        

        //Retornando todos os produtos
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get() //Criando a lista de produtos para serem retornados
        {
            var produtos = _repository.GetProdutos();
            if (produtos is null)
            {
                return NotFound(">> Produtos não encontrados <<");
            }
            return produtos;
        }


        //Retornando produto por Id
        [HttpGet("{id:int:min(1)}", Name="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _repository.GetProduto(id);

            if (produto == null)
            {
                return NotFound(">> Produto Inexistente ou não Encontrado <<");
            }
            return produto;

        }

        //Retornando produto por Id da categoria
        //[HttpGet("{Id_Categoria:int}")]
        //public ActionResult<IEnumerable<Produto>> Id(int Id_Categoria)
        //{
        //    var produtocat = _repository.Produtos.Where(p => p.CategoriaId == Id_Categoria).ToList();

        //    if (produtocat is null)
        //    {
        //        return NotFound(">> Produtos dessa categoria não foram encontrados <<");
        //    }
        //    return produtocat;
        //}


        //Post
        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            //if (produto == null)
            //{
            //    return BadRequest();
            //}
            var ProdutoCriado = _repository.Create(produto);
            return new CreatedAtRouteResult("ObterProduto",
                new { id = ProdutoCriado.ProdutoId }, ProdutoCriado);
        }

        [HttpPut("{id:int:min(1)}")] //Altera
        public ActionResult Put(int id, Produto produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest(">> Produto não encontrado <<");
            }

            var produtoUpdate = _repository.Update(produto);

            return Ok(produtoUpdate);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var produtoDel = _repository.Delete(id);
            if(produtoDel is null)
            {
                return BadRequest(">> Produto não encontrado - Ou já excluido <<");
            }
            return Ok(produtoDel);
        }
    }
}
