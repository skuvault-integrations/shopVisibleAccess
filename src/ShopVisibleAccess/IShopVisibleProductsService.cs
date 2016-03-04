using System.Collections.Generic;
using System.Threading.Tasks;
using ShopVisibleAccess.Models.Product;

namespace ShopVisibleAccess
{
	public interface IShopVisibleProductsService
	{
		bool IsInventoryReceived();

		List< ShopVisibleProductInventory > GetInventory();
		Task< List< ShopVisibleProductInventory > > GetInventoryAsync();

		void UpdateProducts( ShopVisibleProductsInventory products );
		Task UpdateProductsAsync( ShopVisibleProductsInventory products );
	}
}