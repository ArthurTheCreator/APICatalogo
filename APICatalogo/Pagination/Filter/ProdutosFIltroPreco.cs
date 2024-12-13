namespace APICatalogo.Pagination.Filter;

public class ProdutosFIltroPreco : ProdutosParameters
{
    public decimal? Preco { get; set; }
    public string? PrecoCriterio { get; set; } // Maior, menor ou igaul
}
