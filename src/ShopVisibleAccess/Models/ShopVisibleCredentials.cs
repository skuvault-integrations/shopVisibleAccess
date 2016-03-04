using CuttingEdge.Conditions;

namespace ShopVisibleAccess.Models
{
	public class ShopVisibleCredentials
	{
		public string ClientName{ get; private set; }
		public string Guid{ get; private set; }
		public EndpointEnum Endpoint{ get; private set; }
		public string ProductsEndpointName{ get; private set; }
		public string OrdersEndpointName{ get; private set; }

		public ShopVisibleCredentials( string clientName, string guid, EndpointEnum endpoint = EndpointEnum.Api4 )
		{
			Condition.Requires( clientName, "clientName" ).IsNotNullOrWhiteSpace();
			Condition.Requires( guid, "guid" ).IsNotNullOrWhiteSpace();
			Condition.Requires( endpoint, "endpoint" ).IsNotEqualTo( EndpointEnum.Undefined );

			this.ClientName = clientName;
			this.Guid = guid;
			this.Endpoint = endpoint;
			this.ProductsEndpointName = "ShopVisibleProductServiceSoap" + endpoint;
			this.OrdersEndpointName = "ShopVisibleOrderServiceSoap" + endpoint;
		}
	}

	public enum EndpointEnum
	{
		Undefined = 0,
		Api = 1,
		Api4 = 2
	}
}