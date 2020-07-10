using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ApiRestaurante.Code
{
    public static class Utils
    {
        public static string GetEnumDescription<T>( this T p_EnumValue )
        {
            string description = "Inválido, não é do tipo enum";

            try
            {
                if ( p_EnumValue.GetType( ).IsEnum )
                {
                    if ( Enum.GetValues( typeof( T ) ).Cast<T>( ).ToList( ).Contains( p_EnumValue ) )
                    {
                        FieldInfo fi = p_EnumValue.GetType( ).GetField( p_EnumValue.ToString( ) );

                        DescriptionAttribute[ ] attributes = ( DescriptionAttribute[ ] )fi.GetCustomAttributes( typeof( DescriptionAttribute ), false );

                        if ( attributes != null && attributes.Length > 0 )
                        {
                            description = attributes[ 0 ].Description;
                        }
                        else
                        {
                            description = p_EnumValue.ToString( );
                        }
                    }
                    else
                    {
                        description = "Não encontrado";
                    }
                }
            }
            catch ( Exception ex )
            {
                description = ex.Message;
            }

            return description;
        }
    }
}
