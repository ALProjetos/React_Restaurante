using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiRestaurante.Models
{
    public class DishModelRequest
    {
        [Required( ErrorMessage = "É necessário ter ao menos uma refeição" ), MinLength( 1 )]
        public List<int> Dishes { get; set; }
    }
}
