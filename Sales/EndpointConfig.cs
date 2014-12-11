


namespace Sales
{
    using NServiceBus;
    using NServiceBus.Persistence;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://nservicebus.com/GenericHost.aspx
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
	    public void Customize(BusConfiguration configuration)
	    {
	        configuration.UsePersistence<RavenDBPersistence>();
	    }
    }
}