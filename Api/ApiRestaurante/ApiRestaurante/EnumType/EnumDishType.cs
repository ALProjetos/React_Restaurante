using System.ComponentModel;

namespace ApiRestaurante.EnumType
{
    public enum EnumDishType
    {
        [Description( "Entrada" )]
        Entree = 1,
        [Description( "Side" )]
        Side = 2,
        [Description( "Bebida" )]
        Drink = 3,
        [Description( "Sobremesa" )]
        Dessert = 4,
    }
}
