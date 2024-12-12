using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Arguments.Produtos
{
    public class InputUpdateProduto(int id, string nome, string descricao, decimal preco, string imagemUrl, float estoque, int categoriaId)
    {
        public int ProdutoId { get; private set; } = id;
        public string Nome { get; private set; } = nome;
        public string Descricao { get; private set; } = descricao;
        public decimal Preco { get; private set; } = preco;
        public string ImagemUrl { get; private set; } = imagemUrl;
        public float Estoque { get; private set; } = estoque;
        public int CategoriaId { get; private set; } = categoriaId;
    }
}