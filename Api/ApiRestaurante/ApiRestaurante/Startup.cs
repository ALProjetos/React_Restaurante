using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using ApiRestaurante.CustomExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ApiRestaurante
{
    public class Startup
    {
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddSwagger( );

            services.AddControllers( )
                    .AddJsonOptions( options =>
                    {
                        options.JsonSerializerOptions.IgnoreNullValues = true;
                        options.JsonSerializerOptions.WriteIndented = false;
                        options.JsonSerializerOptions.AllowTrailingCommas = false;
                    } )
                    .SetCompatibilityVersion( CompatibilityVersion.Latest );

            services.AddDependencyInjection( );
        }

        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if ( env.IsDevelopment( ) )
            {
                app.UseDeveloperExceptionPage( );
            }

            app.UseSwagger( );

            app.UseRouting( );

            app.UseCors( policy =>
                policy
                .AllowAnyOrigin( )
                .AllowAnyMethod( )
                .AllowAnyHeader( )
            );

            app.UseAuthorization( );

            app.UseEndpoints( endPoints =>
            {
                endPoints.MapControllers( );
            } );
        }
    }
}
