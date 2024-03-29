﻿using NLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Core.Services
{
    public interface ICategoryService:IGenericService<Category>
    {
        Task<Category> GetWithProductsByIdAsync(int categoryId);
    }
}
