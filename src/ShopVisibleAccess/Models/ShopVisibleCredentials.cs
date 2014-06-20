using CuttingEdge.Conditions;

namespace ShopVisibleAccess.Models
{
	public class ShopVisibleCredentials
	{
		public string ClientName { get; private set; }
		public string Guid { get; private set; }

		public ShopVisibleCredentials( string clientName, string guid )
		{
			Condition.Requires( clientName, "clientName" ).IsNotNullOrWhiteSpace();
			Condition.Requires( guid, "guid" ).IsNotNullOrWhiteSpace();

			this.ClientName = clientName;
			this.Guid = guid;
		}
	}
}