using ApiRestaurante.Code;
using ApiRestaurante.EnumType;

namespace ApiRestaurante.Models
{
    public class DishModel
    {
        public EnumTimeDay TimeDayId { get; set; }

        public string TimeDay
        {
            get
            {
                return TimeDayId.GetEnumDescription( );
            }
        }

        public EnumDishType DishTypeId { get; set; }
        
        public string DishType
        {
            get
            {
                return DishTypeId.GetEnumDescription( );
            }
        }

        private string DishDescription = "";
        public string Dish
        {
            get
            {
                if ( TimeDayId == EnumTimeDay.Morning )
                {
                    switch ( DishTypeId )
                    {
                        case EnumDishType.Entree:
                            DishDescription = EnumDishesMorning.Eggs.GetEnumDescription( );
                            break;
                        case EnumDishType.Side:
                            DishDescription = EnumDishesMorning.Toast.GetEnumDescription( );
                            break;
                        case EnumDishType.Drink:
                            DishDescription = EnumDishesMorning.Coffe.GetEnumDescription( );
                            break;
                        case EnumDishType.Dessert:
                            DishDescription = "Não tem sobremesa no café da manhã";
                            break;
                    }
                }
                else if ( TimeDayId == EnumTimeDay.Night )
                {
                    switch ( DishTypeId )
                    {
                        case EnumDishType.Entree:
                            DishDescription = EnumDishesNigth.Steak.GetEnumDescription( );
                            break;
                        case EnumDishType.Side:
                            DishDescription = EnumDishesNigth.Potato.GetEnumDescription( );
                            break;
                        case EnumDishType.Drink:
                            DishDescription = EnumDishesNigth.Wine.GetEnumDescription( );
                            break;
                        case EnumDishType.Dessert:
                            DishDescription = EnumDishesNigth.Cake.GetEnumDescription( );
                            break;
                    }
                }

                return DishDescription;
            }
            set
            {
                DishDescription = value;
            }
        }
    }
}
