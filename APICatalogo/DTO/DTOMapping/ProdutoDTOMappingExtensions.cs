using APICatalogo.Arguments.Produtos;
using APICatalogo.Models;

namespace APICatalogo.DTO.DTOMapping;

public static class ProdutoDTOMappingExtension
{
    public static OutputProduto? ToOuputProduto(this Produto produto)
    {
        if (produto is null)
            return null;
        return new OutputProduto(produto.ProdutoId, produto.Nome, produto.Descricao, produto.Preco, produto.ImagemUrl, produto.Estoque, produto.CategoriaId);
    }
    public static Produto? ToProduto(this InputCreateProduto produto)
    {
        if (produto is null) return null;
        return new Produto(produto.Nome,produto.Descricao, produto.Preco, produto.ImagemUrl,produto.Estoque, produto.CategoriaId);
    }
    public static Produto? ToProduto(this InputUpdateProduto produto)
    {
        if (produto is null) return null;
        return new Produto(produto.Nome, produto.Descricao, produto.Preco, produto.ImagemUrl, produto.Estoque, produto.CategoriaId);
    }
    public static List<OutputProduto> ToOutputProdutoList(this List<Produto> produto)
    {
        if (produto is null) return null;
        return produto.Select(produto => new OutputProduto(produto.ProdutoId, produto.Nome, produto.Descricao, produto.Preco, produto.ImagemUrl, produto.Estoque, produto.CategoriaId)).ToList();
    }
    public static InputUpdateProduto? ToUpdateProduto(this Produto produto)
    {
        if (produto is null) return null;
        return new InputUpdateProduto(null, produto.Nome, produto.Descricao, produto.Preco, produto.ImagemUrl, produto.Estoque, produto.CategoriaId);
    }
}