﻿using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public interface IProdutoRepository
    {
        Task Add(Produto produto);
        Task<IEnumerable<Produto>> GetMany();
        Task<Produto> GetById(int id);
        Task<Produto> GetBySKU(string SKU);
        Task Update(Produto produto);
        Task<bool> ExistById(int id);
    }
}
