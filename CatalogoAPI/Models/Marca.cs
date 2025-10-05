﻿using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.CatalogoAPI.Models
{
    [Table("marcas")]
    public class Marca
    {
        public long Handle { get; set; }
        public string? Nome { get; set; }
        
        public ICollection<Produto>? Produtos { get; set; }
    }
}
