using ApiRestaurante.Code;
using ApiRestaurante.EnumType;

namespace ApiRestaurante.Models
{
    public class TimeDayModel
    {
        public EnumTimeDay Id { get; set; }

        public string Description
        {
            get
            {
                return Id.GetEnumDescription( );
            }
        }
    }
}
