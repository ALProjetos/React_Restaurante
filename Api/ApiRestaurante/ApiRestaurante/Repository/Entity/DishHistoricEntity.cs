using ApiRestaurante.EnumType;
using System;
using System.Collections.Generic;

namespace ApiRestaurante.Models
{
    public class DishHistoricEntity
    {
        public DateTime Date { get; set; }
        public EnumTimeDay TimeDay { get; set; }
        public List<EnumDishType> Dishes { get; set; }
    }
}
