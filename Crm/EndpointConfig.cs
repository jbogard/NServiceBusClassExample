using System.Data.SqlClient;
using NServiceBus.Persistence.Sql;

namespace Crm
{
    using NServiceBus;

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