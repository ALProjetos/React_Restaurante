using System.ComponentModel;

namespace ApiRestaurante.EnumType
{
    public enum EnumDishesMorning
    {
        [Description( "Ovos" )]
        Eggs = 1,
        [Description( "Torrada" )]
        Toast = 2,
        [Description( "Café" )]
        Coffe = 3,
    }

    public enum EnumDishesNigth
    {
        [Description( "Bife" )]
        Steak = 1,
        [Description( "Batata" )]
        Potato = 2,
        [Description( "Vinho" )]
        Wine = 3,
        [Description( "Bolo" )]
        Cake = 4
    }
}
