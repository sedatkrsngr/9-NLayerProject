﻿using NLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetWithCategoryByIdAsync(int productId);
    }
}
