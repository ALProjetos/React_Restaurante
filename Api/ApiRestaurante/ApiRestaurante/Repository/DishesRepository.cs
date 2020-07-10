using ApiRestaurante.EnumType;
using ApiRestaurante.Models;
using ApiRestaurante.Repository.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiRestaurante.Repository
{
    public class DishesRepository: IDishesRepository
    {
        private static readonly List<DishHistoricEntity> m_DishHistoric = new List<DishHistoricEntity>( );
        private readonly ILogger<DishesRepository> m_Logger;

        public DishesRepository( ILogger<DishesRepository> p_Logger )
        {
            m_Logger = p_Logger;
        }

        public List<DishModel> GetByTimeDay( EnumTimeDay p_TimeDayId )
        {
            List<DishModel> allDishesByTimeDay = new List<DishModel>( );

            allDishesByTimeDay.Add( new DishModel { TimeDayId = p_TimeDayId, DishTypeId = EnumDishType.Entree } );
            allDishesByTimeDay.Add( new DishModel { TimeDayId = p_TimeDayId, DishTypeId = EnumDishType.Side } );
            allDishesByTimeDay.Add( new DishModel { TimeDayId = p_TimeDayId, DishTypeId = EnumDishType.Drink } );
            allDishesByTimeDay.Add( new DishModel { TimeDayId = p_TimeDayId, DishTypeId = EnumDishType.Dessert } );

            return allDishesByTimeDay;
        }

        public DishModel GetByDishTypeId( EnumTimeDay p_TimeDayId, EnumDishType p_DishTypeId )
        {
            try
            {
                return GetByTimeDay( p_TimeDayId )?.FirstOrDefault( f => f.DishTypeId == p_DishTypeId );
            }
            catch ( Exception ex )
            {
                m_Logger.LogError( ex, $"Error to get by dishTypeId [{( int )p_DishTypeId}] and timeDayId [{( int )p_TimeDayId}]" );
                throw new Exception( $"Error to get by dishTypeId [{( int )p_DishTypeId}] and timeDayId [{( int )p_TimeDayId}]", ex );
            }
        }

        public bool ExistsTimeDay( EnumTimeDay p_TimeDayId )
        {
            try
            {
                return Enum.GetValues( typeof( EnumTimeDay ) ).Cast<EnumTimeDay>( ).ToList( ).Contains( p_TimeDayId );
            }
            catch ( Exception ex )
            {
                m_Logger.LogError( ex, $"Error to verify exists by timeDayId [{( int )p_TimeDayId}]" );
                throw new Exception( $"Error to verify exists by timeDayId [{( int )p_TimeDayId}]", ex );
            }
        }

        public string NewOrder( EnumTimeDay p_TimeDay, List<DishModel> p_Model )
        {
            try
            {
                string result = "";

                if ( null != p_Model || p_Model.Count > 0 )
                {
                    result = string.Join( ", ",
                        p_Model.GroupBy( g => g.DishTypeId ).Select( s =>
                        {
                            string description = "Inválido";
                            DishModel model = s.First( );

                            if ( null != model )
                            {
                                int count = p_Model.Count( c => c.DishTypeId == s.Key );

                                description = $"{model.Dish}{( count > 1 ? $"(X{count})" : "" )}";
                            }

                            return description;
                        } )
                    );

                    m_DishHistoric.Add(
                        new DishHistoricEntity
                        {
                            Date = DateTime.UtcNow,
                            TimeDay = p_TimeDay,
                            Dishes = p_Model.Select( s => s.DishTypeId ).ToList( )
                        }
                    );
                }
                else
                {
                    result = "Não existem pedidos para criar";
                }

                return result;
            }
            catch ( Exception ex )
            {
                m_Logger.LogError( ex, $"Error to create new order {string.Join( ",", p_Model )}" );
                throw new Exception( $"Error to create new order {string.Join( ",", p_Model )}", ex );
            }
        }

        public List<DishHistoricEntity> History( EnumTimeDay p_TimeDay )
        {
            try
            {
                return m_DishHistoric.Where( w => w.TimeDay == p_TimeDay ).ToList( );
            }
            catch ( Exception ex )
            {
                m_Logger.LogError( ex, $"Error to find history by timeDayId [{p_TimeDay}]" );
                throw new Exception( $"Error to find history by timeDayId [{p_TimeDay}]", ex );
            }
        }
    }
}
