using ApiRestaurante.EnumType;
using ApiRestaurante.Rules.Interface;
using System.Collections.Generic;
using System.Linq;

namespace ApiRestaurante.Rules
{
    public class RuleDishesType: IRuleDishesType
    {
        private Dictionary<EnumTimeDay, List<int>> Rules { get; set; }

        public RuleDishesType( )
        {
            Rules = new Dictionary<EnumTimeDay, List<int>>( );
        }

        public bool IsValidRule( EnumTimeDay p_TimeDay, int p_Dish )
        {
            bool isValid = true;

            if ( Rules.ContainsKey( p_TimeDay ) )
            {
                // No período da manhã, com exceção do café, os outros pratos podem ter apenas 1 pedido
                if ( p_TimeDay == EnumTimeDay.Morning )
                {
                    if( Rules[p_TimeDay].Count( c => ( EnumDishesMorning )c != EnumDishesMorning.Coffe && c == p_Dish ) == 1 )
                    {
                        isValid = false;
                    }
                }
                // No perído da noite, com exceção da batata, os outros pratos podem ter apenas 1 pedido
                else if ( p_TimeDay == EnumTimeDay.Night )
                {
                    if( Rules[p_TimeDay].Count( c => ( EnumDishesNigth )c != EnumDishesNigth.Potato && c == p_Dish ) == 1 )
                    {
                        isValid = false;
                    }
                }

                if ( isValid )
                {
                    Rules[ p_TimeDay ].Add( p_Dish );
                }
            }
            else
            {
                Rules.Add( p_TimeDay, new List<int>( ) { p_Dish } );
            }

            return isValid;
        }
    }
}
