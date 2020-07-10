using ApiRestaurante.Rules;
using ApiRestaurante.Rules.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ApiRestaurante.CustomExtensions
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection( this IServiceCollection p_Services )
        {
            // Adiciona a injeção para validação dos models
            p_Services.AddScoped<ValidationFilterAttribute>( );

            // Se precisar criar novos repositórios, não será necessário faze a injeção pois, já pegará todos pelo namespace
            List<Type> typeRepositories = Assembly.GetExecutingAssembly( ).GetTypes( )
                .Where( w => w.IsClass && !w.IsAbstract && w.GetInterfaces( ).Length > 0 && w.Namespace == "ApiRestaurante.Repository" ).ToList( );

            foreach ( Type type in typeRepositories )
            {
                p_Services.AddScoped( type.GetInterfaces( ).FirstOrDefault( ), type );
            }

            // Adicionado como transient para que a cada requisição na controller, seja zerado o acumulador da regra para validação
            p_Services.AddTransient( typeof( IRuleDishesType ), typeof( RuleDishesType ) );
        }
    }
}
