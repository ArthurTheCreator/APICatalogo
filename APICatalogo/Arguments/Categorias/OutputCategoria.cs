using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICatalogo.Arguments.Categorias;

[method: JsonConstructor]
public class OutputCategoria(int categoriaId, string? nome, string imagemUrl)
{
    public int CategoriaId { get; private set; } = categoriaId;

    public string? Nome { get; private set; } = nome;

    public string ImagemUrl { get; private set; } = imagemUrl;
}
