using APICatalogo.Arguments.Produtos;
using APICatalogo.Context;
using APICatalogo.DTO.DTOMapping;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.Interfaces;
using APICatalogo.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        public ActionResult<List<OutputProduto>> Get() //Criando a lista de produtos para serem retornados
        {
            var produtos = _unitOfWorkRepository.ProdutosRepository.GetAll();
            if (produtos is null)
            {
                return NotFound(">> Produtos não encontrados <<");
            }
            return produtos.ToOutputProdutoList();
        }

        [HttpGet("Pagination")]
        public ActionResult<List<OutputProduto>> Get([FromQuery] ProdutosParameters produtosParameters)
        {
            var produtos = _unitOfWorkRepository.ProdutosRepository.GetProdutos(produtosParameters);
            return Ok(produtos.ToOutputProdutoList());
        }

        //Retornando produto por Id
        [HttpGet("{id:int:min(1)}", Name="ObterProduto")]
        public ActionResult<OutputProduto> Get(int id)
        {
            var produto = _unitOfWorkRepository.ProdutosRepository.Get(c => c.CategoriaId == id);

            if (produto == null)
            {
                return NotFound(">> Produto Inexistente ou não Encontrado <<");
            }
            return Ok(produto.ToOuputProduto());

        }


        //Post
        [HttpPost]
        public ActionResult Post(InputCreateProduto produto)
        {
            if (produto == null)
            {
                return BadRequest();
            }
            var ProdutoCriado = _unitOfWorkRepository.ProdutosRepository.Create(produto.ToProduto());
            _unitOfWorkRepository.Commit();
            return new CreatedAtRouteResult("ObterProduto",
                ProdutoCriado.ToOuputProduto());
        }

        [HttpPut("{id:int:min(1)}")] //Altera
        public ActionResult<OutputProduto> Put(int id, InputUpdateProduto produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest(">> Produto não encontrado <<");
            }
            var update = produto.ToProduto();
            var produtoUpdate = _unitOfWorkRepository.ProdutosRepository.Update(update);
            _unitOfWorkRepository.Commit();
            return Ok(produtoUpdate.ToOuputProduto());
        }

        [HttpPatch("{id}/UpdatePartial")]
        public ActionResult<OutputProduto>  Patch(int id,
            JsonPatchDocument<InputUpdateProduto> patch)
        {
            if (patch is null || id <= 0) return BadRequest();
            var produto = _unitOfWorkRepository.ProdutosRepository.Get(c => c.ProdutoId == id);
            if (produto is null) return NotFound();

            _unitOfWorkRepository.ProdutosRepository.Update(produto);
            _unitOfWorkRepository.Commit();
            return produto.ToOuputProduto();
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
