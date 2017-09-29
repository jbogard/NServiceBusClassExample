using System.Data.SqlClient;
using NServiceBus;
using NServiceBus.Persistence.Sql;

namespace Shipping
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public void Customize(EndpointConfiguration configuration)
        {
            configuration.UsePersistence<SqlPersistence>()
                .ConnectionBuilder(() => new SqlConnection(@"Data Source=.\SqlExpress;Initial Catalog=NServiceBusClassExample;Integrated Security=True"));
            configuration.UseTransport<RabbitMQTransport>().ConnectionString("host=localhost");
        }
    }
}
