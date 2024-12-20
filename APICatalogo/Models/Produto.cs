﻿using APICatalogo.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models
{
    [Table("Produtos")]
    public class Produto : IValidatableObject
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O Nome é *OBRIGATÓRIOS*")]
        [StringLength(80)]
        [PrimeiraLetraMaiscula]
        public string Nome { get; set; }

        [Required]
        [StringLength(512)]
        public string Descricao { get; set; }

        [Required]
        [Range(0.01, 1000000000, ErrorMessage = "O preço do produto deve estar entre {1} e {2}")]
        public decimal Preco { get; set; }

        [Required]
        [StringLength (312)]
        public string ImagemUrl { get; set; }

        
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }

      
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; } //Indicar que um produto está relacionado com categoria

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.Nome))
            {
                var primeiraLetra = this.Nome[0].ToString();
                if (primeiraLetra != primeiraLetra.ToUpper())
                {
                    yield return new
                        ValidationResult("A primeira Letra do Produto deve ser Maiúscula", new[] { nameof(this.Nome) });
                }
            }
        }

        public Produto(string nome, string descricao, decimal preco, string imagemUrl, float estoque, int categoriaId)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            ImagemUrl = imagemUrl;
            Estoque = estoque;
            CategoriaId = categoriaId;
        }
    }
}
