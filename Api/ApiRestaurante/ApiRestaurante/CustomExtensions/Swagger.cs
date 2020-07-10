using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ApiRestaurante.CustomExtensions
{
    public static class Swagger
    {
        public static void AddSwagger( this IServiceCollection services )
        {
            services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc( "v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Restaurante API",
                        Version = "v1",
                        Description = "Restaurante API"
                    } );
            } );
        }

        public static void UseSwagger( this IApplicationBuilder app )
        {
            SwaggerBuilderExtensions.UseSwagger( app );
            app.UseSwaggerUI( c =>
            {
                c.SwaggerEndpoint( "/swagger/v1/swagger.json",
                    "RestauranteApi" );
                c.RoutePrefix = "doc";
            } );
        }
    }
}
