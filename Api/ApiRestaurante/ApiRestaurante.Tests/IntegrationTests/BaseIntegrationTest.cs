using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestaurante.Tests.IntegrationTests
{
    public class BaseIntegrationTest
    {
        protected readonly HttpClient m_Client;

        public BaseIntegrationTest( )
        {
            var server = new TestServer(
                new WebHostBuilder( )
                .UseEnvironment( "Development" )
                .UseStartup<Startup>( )
            );

            m_Client = server.CreateClient( );
        }

        protected StringContent GetAsJsonContent( object p_Object )
        {
            return new StringContent(
                p_Object != null ? JsonConvert.SerializeObject( p_Object ) : string.Empty,
                Encoding.UTF8,
                "application/json"
            );
        }

        protected async Task<T> GetAs<T>( HttpResponseMessage p_Response )
        {
            var content = await p_Response.Content.ReadAsStringAsync( );
            return JsonConvert.DeserializeObject<T>( content );
        }

        public string GetString( HttpContent p_HttpContent )
        {
            return p_HttpContent.ReadAsStringAsync( ).GetAwaiter( ).GetResult( );
        }
    }
}
