using APICatalogo.Arguments.Categorias;
using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories.Interfaces;
using APICatalogo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IRepository<Categoria> _repository;
    private readonly ICategoriasRepository _repositoryCategoria;
    private readonly ILogger<CategoriasController> _logger;

    public CategoriasController(IRepository<Categoria> repository, ILogger<CategoriasController> logger, ICategoriasRepository repositoryCategoria)
    {
        _repository = repository;
        _repositoryCategoria = repositoryCategoria;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<List<Categoria>> Get()
    {
        var categorias = _repository.GetAll();
        return Ok(categorias);
    }

    // Retornanndo com Id
    [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")] //Resttrições -> maior que zero
    public ActionResult<Categoria> Get(int id)
    {
        var categoria = _repository.Get(c => c.CategoriaId == id);
        if (categoria is null)
        {
            _logger.LogWarning($"Categoria com id: {id} não encontrada...");
            return NotFound($" >>> Categorias Não encotradas <<<");
        }
        return Ok(categoria);
    }


    // Mostrar tudo
    [HttpGet("Categorias")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        var categoriasprod = _repositoryCategoria.GetCategoriaEProdutos();
        if (categoriasprod is null)
        {
            _logger.LogWarning($"Categorias e produtos não encontrados...");
            return NotFound($" >>> Categorias Não encotradas <<<");

        }
        return categoriasprod;
    }


    // Mostrar tudo por id
    [HttpGet("produtos{id:int:min(1)}")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos(int id)
    {
        var catEProdutos = _repositoryCategoria.GetCategoriasProdutos(id);
        if (catEProdutos is null)
        {
            _logger.LogWarning($"Categorias e produtos não encontrados...");
            return NotFound($" >>> Categorias Não encotradas <<<");
        }
        return catEProdutos;
    }


    // criando
    [HttpPost]
    public ActionResult Post(InputCreateCategoria categoria)
    {
        var categoriaCriada = _repository.Create(new Categoria(categoria.Nome, categoria.ImagemUrl, null));
        if (categoria is null)
        {
            return BadRequest("Categoria vazia");
        }

        _repository.Create(categoriaCriada);

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoriaCriada.CategoriaId }, new OutputCategoria(categoriaCriada.CategoriaId, categoriaCriada.ImagemUrl, categoriaCriada.Nome));
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest(" >> Categoria Não Encontrada <<");
        }
        _repository.Update(categoria);
        return Ok(categoria);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    { 
        var catDel = _repository.Delete(id);
        if (catDel)
        {
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
//    var categoria = _repository.Categorias.AsNoTracking().ToListAsync();
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
//    var categorias = _repository.Categorias.ToList();
//    if (categorias is null)
//    {
//        return NotFound(" >> Categorias Não Encontradas <<");
//    }

//    return Ok(categorias);
//}