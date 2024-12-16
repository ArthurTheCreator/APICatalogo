using APICatalogo.Arguments.Categorias;
using APICatalogo.Arguments.Produtos;
using APICatalogo.Context;
using APICatalogo.DTO.DTOMapping;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.Interfaces;
using APICatalogo.Repositories.UnitOfWork;
using APICatalogo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWorkrepository;
    private readonly ILogger<CategoriasController> _logger;

    public CategoriasController(IUnitOfWork unitOfWorkRepository, ILogger<CategoriasController> logger)
    {
        _unitOfWorkrepository = unitOfWorkRepository;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<OutputCategoria>>> Get()
    {
        var categorias = await _unitOfWorkrepository.CategoriasRepository.GetAll();
        return Ok(categorias.ToOutputCategoriaList());
    }

    // Retornanndo com Id
    [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")] //Resttrições -> maior que zero
    public async Task<ActionResult<OutputCategoria>> Get(int id)
    {
        var categoria = await _unitOfWorkrepository.CategoriasRepository.Get(c => c.CategoriaId == id);
        if (categoria is null)
        {
            _logger.LogWarning($"Categoria com id: {id} não encontrada...");
            return NotFound($" >>> Categorias Não encotradas <<<");
        }
        return Ok(categoria.ToOuputCategoria());
    }


    // Mostrar tudo
    [HttpGet("CategoriaESeusProdutos")]
    public async Task<ActionResult<List<OutputCategoria>>> GetCategoriasProdutos()
    {
        var categoriasprod = await _unitOfWorkrepository.CategoriasRepository.GetCategoriaEProdutos();
        if (categoriasprod is null)
        {
            _logger.LogWarning($"Categorias e produtos não encontrados...");
            return NotFound($" >>> Categorias Não encotradas <<<");

        }
        return Ok(categoriasprod.ToOutputCategoriaList());
    }


    // Mostrar tudo por id
    [HttpGet("Produtos{id:int:min(1)}")]
    public async Task<ActionResult<List<OutputCategoria>>> GetCategoriasProdutos(int id)
    {
        var catEProdutos = await _unitOfWorkrepository.CategoriasRepository.GetCategoriasProdutos(id);
        if (catEProdutos is null)
        {
            _logger.LogWarning($"Categorias e produtos não encontrados...");
            return NotFound($" >>> Categorias Não encotradas <<<");
        }
        return Ok(catEProdutos.ToOutputCategoriaList());
    }


    // criando
    [HttpPost]
    public ActionResult<OutputCategoria> Post(InputCreateCategoria categoria)
    {
        var categoriaCriada = _unitOfWorkrepository.CategoriasRepository.Create(categoria.ToCategoria());
        _unitOfWorkrepository.Commit();
        if (categoria is null)
        {
            return BadRequest("Categoria vazia");
        }

        _unitOfWorkrepository.CategoriasRepository.Create(categoriaCriada);

        return new CreatedAtRouteResult("ObterCategoria",
            categoriaCriada.ToOuputCategoria());
    }

    [HttpPut("{id:int}")]
    public ActionResult<OutputCategoria> Put(int id, InputUpdateCategoria categoria)
    {
        if (id != categoria.CategoriaID)
        {
            return BadRequest(" >> Categoria Não Encontrada <<");
        }
        var categoriaUpdate = categoria.ToCategoria();
        _unitOfWorkrepository.CategoriasRepository.Update(categoriaUpdate);
        _unitOfWorkrepository.Commit();
        return Ok(categoriaUpdate.ToOuputCategoria());
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    { 
        var catDel = _unitOfWorkrepository.CategoriasRepository.Delete(id);
        if (catDel)
        {
            _unitOfWorkrepository.Commit();
            return Ok(" >>> Categoria Deletada Com SUCESSO <<<");
        }
        return NotFound(" >>> Categoria NÃO encontrada ou ja deletada <<<");
    }
}

//[HttpGet("UsandoFromServices/{nome}")]
//public ActionResult<string> GetSaudacoesFromServices([FromServices] IMeuServico meuServico, string nome)
//{
//    return meuServico.Saudacao(nome);
//}

//[HttpGet("{nome}")]
//public ActionResult<string> GetSaudacoesSemFromServices( IMeuServico meuServico, string nome)
//{
//    return meuServico.Saudacao(nome);
//}

//[HttpGet]
//[ServiceFilter(typeof(ApiLoggingFilter))]
//public async Task<ActionResult<IEnumerable<Categoria>>> Get()
//{
//    var categoria = _unitOfWorkrepository.CategoriasRepository.Categorias.AsNoTracking().ToListAsync();
//    if (categoria is null)
//    {
//        _logger.LogWarning($"Categoria não encontrada...");
//        return NotFound($" >>> Categorias Não encotradas <<<");
//    }
//    return await categoria;
//}


//Retornando Todas as categorias
//[HttpGet]
//public ActionResult<IEnumerable<Categoria>> Get()
//{
//    var categorias = _unitOfWorkrepository.CategoriasRepository.Categorias.ToList();
//    if (categorias is null)
//    {
//        return NotFound(" >> Categorias Não Encontradas <<");
//    }

//    return Ok(categorias);
//}