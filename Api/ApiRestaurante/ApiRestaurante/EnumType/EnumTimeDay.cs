using System.ComponentModel;

namespace ApiRestaurante.EnumType
{
    public enum EnumTimeDay
    {
        [Description( "Período da manhã" )]
        Morning = 1,
        [Description( "Período da noite" )]
        Night = 2,
    }
}
