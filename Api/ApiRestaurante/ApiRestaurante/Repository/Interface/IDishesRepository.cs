using ApiRestaurante.EnumType;
using ApiRestaurante.Models;
using System.Collections.Generic;

namespace ApiRestaurante.Repository.Interface
{
    public interface IDishesRepository
    {
        List<DishModel> GetByTimeDay( EnumTimeDay p_TimeDayId );
        DishModel GetByDishTypeId( EnumTimeDay p_TimeDayId, EnumDishType p_DishTypeId );
        bool ExistsTimeDay( EnumTimeDay p_TimeDayId );
        string NewOrder( EnumTimeDay p_TimeDay, List<DishModel> p_Model );
        List<DishHistoricEntity> History( EnumTimeDay p_TimeDay );
    }
}