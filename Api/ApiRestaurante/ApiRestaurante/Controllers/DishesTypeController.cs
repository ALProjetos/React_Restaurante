using ApiRestaurante.Code;
using ApiRestaurante.EnumType;
using ApiRestaurante.Models;
using ApiRestaurante.Repository.Interface;
using ApiRestaurante.Rules.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ApiRestaurante.Controllers
{
    [Route( "api/[controller]" )]
    public partial class DishesTypeController : ControllerBase
    {
        private readonly IDishesRepository m_DishesRepository;
        private readonly IRuleDishesType m_RuleDishesType;
        private readonly ILogger<DishesTypeController> m_Logger;

        public class DishesErrors
        {
            public static readonly string TimeDayIdNotFound = "Período não encontrado";
            public static readonly string ErrorToFindAllByTimeDayId = "Erro ao buscar o cardápio por período";
            public static readonly string ErrorFindDishModel = "Erro ao buscar o prato do cardápio por período e tipo de prato";
            public static readonly string ErrorCreateNewOrder = "Ocorreu um erro ao criar o pedido";
        }

        public DishesTypeController( IDishesRepository p_DishesRepository, IRuleDishesType p_RuleDishesType, ILogger<DishesTypeController> p_Logger )
        {
            m_DishesRepository = p_DishesRepository;
            m_RuleDishesType = p_RuleDishesType;
            m_Logger = p_Logger;
        }

        [HttpGet( "{timeDayId}" )]
        [ProducesResponseType( typeof( List<DishModel> ), ( int )HttpStatusCode.OK )]
        [ProducesResponseType( typeof( DishesErrors ), ( int )HttpStatusCode.BadRequest )]
        public IActionResult GetAllByTimeDayId( [FromRoute] int timeDayId )
        {
            IActionResult result;

            try
            {
                if ( m_DishesRepository.ExistsTimeDay( ( EnumTimeDay )timeDayId ) )
                {
                    result = Ok(
                        m_DishesRepository.GetByTimeDay( ( EnumTimeDay )timeDayId )
                    );
                }
                else
                {
                    result = BadRequest( DishesErrors.TimeDayIdNotFound );
                }
            }
            catch ( Exception ex )
            {
                string timeDay = ( ( EnumTimeDay )timeDayId ).GetEnumDescription( );

                m_Logger.LogError( ex, $"Error to get all by timeDay [{timeDay}]" );
                result = BadRequest( DishesErrors.ErrorToFindAllByTimeDayId );
            }

            return result;
        }

        [HttpGet( "{timeDayId}/{dishTypeId}" )]
        [ProducesResponseType( typeof( DishModel ), ( int )HttpStatusCode.OK )]
        [ProducesResponseType( typeof( DishesErrors ), ( int )HttpStatusCode.NoContent )]
        public IActionResult GetByDishType( [FromRoute] int timeDayId, [FromRoute] int dishTypeId )
        {
            IActionResult result = NoContent( );

            try
            {
                if ( m_DishesRepository.ExistsTimeDay( ( EnumTimeDay )timeDayId ) )
                {
                    DishModel dishModel = m_DishesRepository.GetByDishTypeId( ( EnumTimeDay )timeDayId, ( EnumDishType )dishTypeId );
                    if ( null != dishModel )
                    {
                        result = Ok( dishModel );
                    }
                }
                else
                {
                    result = BadRequest( DishesErrors.TimeDayIdNotFound );
                }
            }
            catch ( Exception ex )
            {
                string timeDay = ( ( EnumTimeDay )timeDayId ).GetEnumDescription( );
                string dishType = ( ( EnumDishType )dishTypeId ).GetEnumDescription( );

                m_Logger.LogError( ex, $"Error to get by timeDay [{timeDay}] and dishType [{dishType}]" );
                result = BadRequest( DishesErrors.ErrorFindDishModel );
            }

            return result;
        }

        [HttpPost( "{timeDayId}" )]
        [ServiceFilter(typeof( ValidationFilterAttribute ))]
        [ProducesResponseType( typeof( DishModelResponse ), ( int )HttpStatusCode.OK )]
        [ProducesResponseType( typeof( DishesErrors ), ( int )HttpStatusCode.BadRequest )]
        public IActionResult PostOrder( [FromRoute] int timeDayId, [FromBody] DishModelRequest p_Model )
        {
            IActionResult result;

            try
            {
                EnumTimeDay timeDay = ( EnumTimeDay )timeDayId;

                if ( m_DishesRepository.ExistsTimeDay( timeDay ) )
                {
                    result = Ok(
                        new DishModelResponse
                        {
                            Result = ResultOrder( timeDay, p_Model.Dishes )
                        }
                    );
                }
                else
                {
                    result = BadRequest( DishesErrors.TimeDayIdNotFound );
                }
            }
            catch ( Exception ex )
            {
                string timeDay = ( ( EnumTimeDay )timeDayId ).GetEnumDescription( );

                m_Logger.LogError( ex, $"Error to order by timeDayId [{timeDay}]" );
                result = BadRequest( DishesErrors.ErrorCreateNewOrder );
            }

            return result;
        }

        private string ResultOrder( EnumTimeDay p_TimeDay, List<int> p_DishesModel )
        {
            try
            {
                string result = "";
                List<DishModel> dishesValid = new List<DishModel>( );

                foreach ( int dish in p_DishesModel.OrderBy( o => o ) )
                {
                    // Realiza a validação das regras
                    if ( m_RuleDishesType.IsValidRule( p_TimeDay, dish ) )
                    {
                        DishModel dishModel = m_DishesRepository.GetByDishTypeId( p_TimeDay, ( EnumDishType )dish );

                        if ( null != dishModel )
                        {
                            dishesValid.Add( dishModel );
                        }
                        else
                        {
                            dishesValid.Add( new DishModel { Dish = "Opção inválida" } );
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                result = m_DishesRepository.NewOrder( p_TimeDay, dishesValid );

                if ( dishesValid.Count != p_DishesModel.Count )
                {
                    result = $"{result}, Não pode repetir o prato";
                }

                return result;
            }
            catch ( Exception ex )
            {
                m_Logger.LogError( ex, $"Error to verify rule and create new order {string.Join( ",", p_DishesModel )}" );
                throw new Exception( $"Error to verify rule and create new order {string.Join( ",", p_DishesModel )}", ex );
            }
        }
    }
}
