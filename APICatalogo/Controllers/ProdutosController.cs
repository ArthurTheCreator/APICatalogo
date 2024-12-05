using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context; //Contexto com o banco de dados

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }


        //Retornando todos os produtos
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get() //Criando a lista de produtos para serem retornados
        {
            var produtos = _context.Produtos.ToList();
            if (produtos is null)
            {
                return NotFound(">> Produtos não encontrados <<");
            }
            return produtos;
        }


        //Retornando produto por Id
        [HttpGet("{id:int}")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto == null)
            {
                return NotFound(">> Produto Inexistente ou não Encontrado <<");
            }
            return produto;

        }

        //Retornando produto por Id da categoria
        //[HttpGet("{idCat:int}")]
        //public ActionResult<IEnumerable<Produto>> Get(decimal idCat)
        //{
        //    var produtocat = _context.Produtos.Where(p => p.CategoriaId == idCat).ToList();

        //    if (produtocat is null)
        //    {
        //        return NotFound(">> Produtos dessa categoria não foram encontrados <<");
        //    }
        //    return produtocat;
        //}

    }
}
