using ApiRestaurante.Code;
using ApiRestaurante.EnumType;
using ApiRestaurante.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ApiRestaurante.Controllers
{
    public partial class DishesTypeController
    {
        public class DishesHistoricErrors
        {
            public static readonly string ErrorToFindHistoricByTimeDay = "Ocorreu um erro ao buscar o histórico por período";
            public static readonly string TimeDayIdNotFound = "Período não encontrado";
        }

        [HttpGet( "{timeDayId}/Historic" )]
        [ProducesResponseType( typeof( List<DishHistoricModel> ), ( int )HttpStatusCode.OK )]
        [ProducesResponseType( typeof( DishesHistoricErrors ), ( int )HttpStatusCode.BadRequest )]
        public IActionResult GetHistoric( [FromRoute] int timeDayId )
        {
            IActionResult result;

            try
            {
                if ( m_DishesRepository.ExistsTimeDay( ( EnumTimeDay ) timeDayId ) )
                {
                    List<DishHistoricEntity> entity = m_DishesRepository.History( ( EnumTimeDay ) timeDayId );

                    List<DishHistoricModel> model = entity.Select( s =>
                        new DishHistoricModel
                        {
                            Date = s.Date,
                            Dishes = s.Dishes.Select( dish => new DishModel { TimeDayId = ( EnumTimeDay ) timeDayId, DishTypeId = dish } ).ToList( )
                        }
                    ).ToList( );

                    result = Ok(
                        model
                    );
                }
                else
                {
                    result = BadRequest( DishesHistoricErrors.TimeDayIdNotFound );
                }
            }
            catch ( Exception ex )
            {
                string timeDay = ( ( EnumTimeDay )timeDayId ).GetEnumDescription( );

                m_Logger.LogError( ex, $"Error to find historic by timeDay [{timeDay}]" );
                result = BadRequest( );
            }

            return result;
        }
    }
}
