﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.API.Dtos
{
    public class ErrorDto
    {
        public ErrorDto()
        {
            Errors = new List<string>();//Error Dto çağrıldığında bir nesne örneği alıyoruz yoksa hata alır
        }

        public List<String> Errors { get; set; }
        public int Status { get; set; }
    }
}
