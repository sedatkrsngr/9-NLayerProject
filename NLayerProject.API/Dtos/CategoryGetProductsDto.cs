﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.API.Dtos
{
    public class CategoryGetProductsDto
    {
        public ICollection<ProductDto> Products { get; set; }
    }
}
