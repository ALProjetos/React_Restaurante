using ApiRestaurante.EnumType;
using ApiRestaurante.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace ApiRestaurante.Controllers
{
    [Route( "api/[controller]" )]
    public class TimeDaysController: ControllerBase
    {
        [HttpGet( )]
        [ProducesResponseType( typeof( List<TimeDayModel> ), ( int )HttpStatusCode.OK )]
        public IActionResult GetAll( )
        {
            List<TimeDayModel> allTimeDays = new List<TimeDayModel>( );

            foreach( EnumTimeDay timeDay in Enum.GetValues( typeof( EnumTimeDay ) ) )
            {
                allTimeDays.Add( new TimeDayModel { Id = timeDay } );
            }

            return Ok( allTimeDays );
        }
    }
}
