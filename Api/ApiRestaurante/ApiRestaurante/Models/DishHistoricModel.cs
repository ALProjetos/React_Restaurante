using System;
using System.Collections.Generic;

namespace ApiRestaurante.Models
{
    public class DishHistoricModel
    {
        public DateTime Date { get; set; }
        public List<DishModel> Dishes { get; set; }
    }
}
