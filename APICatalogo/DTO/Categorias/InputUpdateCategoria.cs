using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace APICatalogo.Arguments.Categorias;

[method: JsonConstructor]
public class InputUpdateCategoria(int id, string? nome, string imagemUrl)
{
    public int CategoriaID { get; private set; } = id;
    public string? Nome { get; private set; } = nome;
    public string ImagemUrl { get; private set; } = imagemUrl;
}