using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace APICatalogo.Arguments.Categorias;

[method: JsonConstructor]
public class InputUpdateCategoria(string? nome, string imagemUrl)
{
    public string? Nome { get; private set; } = nome;

    public string ImagemUrl { get; private set; } = imagemUrl;
}