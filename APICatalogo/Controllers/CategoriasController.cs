﻿using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger _logger;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
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

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<IEnumerable<Categoria>>> Get()
    {
        var categoria = _context.Categorias.AsNoTracking().ToListAsync();
        if (categoria is null)
        {
            _logger.LogWarning($"Categoria não encontrada...");
            return NotFound($" >>> Categorias Não encotradas <<<");
        }
        return await categoria;
    }




    //Retornando Todas as categorias
    //[HttpGet]
    //public ActionResult<IEnumerable<Categoria>> Get()
    //{
    //    var categorias = _context.Categorias.ToList();
    //    if (categorias is null)
    //    {
    //        return NotFound(" >> Categorias Não Encontradas <<");
    //    }

    //    return Ok(categorias);
    //}

    // Retornanndo com Id
    [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")] //Resttrições -> maior que zero
    public ActionResult<Categoria> Get(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
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
        var categoriasprod = _context.Categorias.Include(p => p.Produtos).ToList();
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
        return _context.Categorias.Include(p => p.Produtos).Where(c => c.CategoriaId == id).AsNoTracking().ToList();
    }


    // criando
    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        if (categoria is null)
        {
            return BadRequest("Categoria vazia");
        }
        _context.Add(categoria);
        _context.SaveChanges();
        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest(" >> Categoria Não Encontrada <<");
        }
        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(categoria);
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
        var categoriaDeletada = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
        if (categoriaDeletada is null)
        {
            return NotFound(" >> Categoria Não Encontrada <<");
        }
        _context.Remove(categoriaDeletada);
        _context.SaveChanges();
        return Ok(categoriaDeletada);
    }
}
