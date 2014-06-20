using System.Threading.Tasks;
using ShopVisibleAccess.Misc;
using ShopVisibleAccess.Models;
using ShopVisibleAccess.Models.Product;
using ShopVisibleAccess.ProductServices;

namespace ShopVisibleAccess
{
	public class ShopVisibleProductsService : IShopVisibleProductsService
	{
		private readonly ShopVisibleCredentials _credentials;
		private readonly ProductServiceSoapClient _client;

		public ShopVisibleProductsService( ShopVisibleCredentials credentials )
		{
			this._credentials = credentials;
			this._client = new ProductServiceSoapClient();
		}

		public void UpdateProducts( ShopVisibleProducts products )
		{
			var xml = XmlSerializeHelpers.Serialize( products );
			ActionPolicies.Submit.Do( () => this._client.SetProductInventory( this._credentials.ClientName, this._credentials.Guid, "false", xml ) );
		}

		public async Task UpdateProductsAsync( ShopVisibleProducts products )
		{
			var xml = XmlSerializeHelpers.Serialize( products );
			await ActionPolicies.GetAsync.Do( async () =>
			{
				await this._client.SetProductInventoryAsync( this._credentials.ClientName, this._credentials.Guid, "false", xml );
			} );
		}
	}
}