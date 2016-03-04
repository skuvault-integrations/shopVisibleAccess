using CuttingEdge.Conditions;

namespace ShopVisibleAccess.Models
{
	public class ShopVisibleCredentials
	{
		public string ClientName{ get; private set; }
		public string Guid{ get; private set; }
		public ShopVisibleEndpointEnum ShopVisibleEndpoint{ get; private set; }
		public string ProductsEndpointName{ get; private set; }
		public string OrdersEndpointName{ get; private set; }

		public ShopVisibleCredentials( string clientName, string guid, ShopVisibleEndpointEnum endpoint = ShopVisibleEndpointEnum.Api4 )
		{
			Condition.Requires( clientName, "clientName" ).IsNotNullOrWhiteSpace();
			Condition.Requires( guid, "guid" ).IsNotNullOrWhiteSpace();
			Condition.Requires( endpoint, "endpoint" ).IsNotEqualTo( ShopVisibleEndpointEnum.Undefined );

			this.ClientName = clientName;
			this.Guid = guid;
			this.ShopVisibleEndpoint = endpoint;
			this.ProductsEndpointName = "ShopVisibleProductServiceSoap" + endpoint;
			this.OrdersEndpointName = "ShopVisibleOrderServiceSoap" + endpoint;
		}
	}

	public enum ShopVisibleEndpointEnum
	{
		Undefined = 0,
		Api = 1,
		Api4 = 2
	}
}