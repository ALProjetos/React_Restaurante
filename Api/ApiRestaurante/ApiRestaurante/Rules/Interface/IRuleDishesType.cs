using ApiRestaurante.EnumType;

namespace ApiRestaurante.Rules.Interface
{
    public interface IRuleDishesType
    {
        bool IsValidRule( EnumTimeDay p_TimeDay, int p_Dish );
    }
}
