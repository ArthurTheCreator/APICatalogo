using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using APICatalogo.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWorkRepository; //Contexto com o banco de dados

        public ProdutosController(IUnitOfWork unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        

        //Retornando todos os produtos
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get() //Criando a lista de produtos para serem retornados
        {
            var produtos = _unitOfWorkRepository.ProdutosRepository.GetAll();
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
            var produto = _unitOfWorkRepository.ProdutosRepository.Get(c => c.CategoriaId == id);

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
        //    var produtocat = _unitOfWorkRepository.ProdutosRepository.Produtos.Where(p => p.CategoriaId == Id_Categoria).ToList();

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
            var ProdutoCriado = _unitOfWorkRepository.ProdutosRepository.Create(produto);
            _unitOfWorkRepository.Commit();
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

            var produtoUpdate = _unitOfWorkRepository.ProdutosRepository.Update(produto);
            _unitOfWorkRepository.Commit();
            return Ok(produtoUpdate);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var produtoDel = _unitOfWorkRepository.ProdutosRepository.Delete(id);
            _unitOfWorkRepository.Commit();
            if (produtoDel = false)
            {
                return BadRequest(">> Produto não encontrado - Ou já excluido <<");
            }
            return Ok(" >>> Produto Deletado Com SUCESSO <<<");
        }
    }
}