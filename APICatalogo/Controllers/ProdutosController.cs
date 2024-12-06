using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var categorias = _context.Categorias.ToList();
            if (produtos is null)
            {
                return NotFound(">> Produtos não encontrados <<");
            }
            return produtos;
        }


        //Retornando produto por Id
        [HttpGet("{id:int}", Name="ObterProduto")]
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
        //[HttpGet("{Id_Categoria:int}")]
        //public ActionResult<IEnumerable<Produto>> Id(int Id_Categoria)
        //{
        //    var produtocat = _context.Produtos.Where(p => p.CategoriaId == Id_Categoria).ToList();

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
            if (produto == null)
            {
                return BadRequest();
            }
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto",
                new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")] //Altera
        public ActionResult Put(int id, Produto produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest(">> Produto não encontrado <<");
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if(produto is null)
            {
                return BadRequest(">> Produto não encontrado - Ou já excluido <<");
            }
            _context.Remove(produto).State = EntityState.Deleted;
            _context.SaveChanges();
            return Ok(produto);
        }
    }
}
