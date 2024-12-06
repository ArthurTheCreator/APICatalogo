﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required]
        [StringLength(80)]
        public string Nome { get; set; }

        [Required]
        [StringLength(512)]
        public string Descricao { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        [Required]
        [StringLength (312)]
        public string ImagemUrl { get; set; }

       
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }

      
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; } //Indicar que um produto está relacionado com categoria
    }
}
