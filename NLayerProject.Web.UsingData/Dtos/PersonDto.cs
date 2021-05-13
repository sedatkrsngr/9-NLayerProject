using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.Web.UsingData.Dtos
{
    public class PersonDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir")]
        public string SurName { get; set; }
    }
}
