using System.Collections.Generic;
using System.Linq;
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

		public List< ShopVisibleProductInventory > GetInventory()
		{
			var inventory = new ShopVisibleProductsInventory();
			ActionPolicies.Submit.Do( () =>
			{
				var xmlInventory = this._client.GetProductInventory( this._credentials.ClientName, this._credentials.Guid );
				inventory = XmlSerializeHelpers.Deserialize< ShopVisibleProductsInventory >( xmlInventory.OuterXml );
			} );
			return inventory.Products;
		}

		public async Task< List< ShopVisibleProductInventory > > GetInventoryAsync()
		{
			var inventory = new ShopVisibleProductsInventory();
			await ActionPolicies.GetAsync.Do( async () =>
			{
				var xmlInventory = await this._client.GetProductInventoryAsync( this._credentials.ClientName, this._credentials.Guid );
				inventory = XmlSerializeHelpers.Deserialize< ShopVisibleProductsInventory >( xmlInventory.OuterXml );
			} );
			return inventory.Products;
		}

		public void UpdateProducts( ShopVisibleProductsInventory products )
		{
			var xml = XmlSerializeHelpers.Serialize( products );
			ActionPolicies.Submit.Do( () => this._client.SetProductInventory( this._credentials.ClientName, this._credentials.Guid, "false", xml ) );
		}

		public async Task UpdateProductsAsync( ShopVisibleProductsInventory products )
		{
			var xml = XmlSerializeHelpers.Serialize( products );
			await ActionPolicies.GetAsync.Do( async () =>
			{
				await this._client.SetProductInventoryAsync( this._credentials.ClientName, this._credentials.Guid, "false", xml );
			} );
		}
	}
}