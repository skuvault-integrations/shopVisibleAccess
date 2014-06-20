using CuttingEdge.Conditions;
using ShopVisibleAccess.Models;

namespace ShopVisibleAccess
{
	public interface IShopVisibleFactory
	{
		IShopVisibleOrdersService CreateOrdersService( ShopVisibleCredentials credentials );
		IShopVisibleProductsService CreateProductsService( ShopVisibleCredentials credentials );
	}

	public class ShopVisibleFactory : IShopVisibleFactory
	{
		public IShopVisibleOrdersService CreateOrdersService( ShopVisibleCredentials credentials )
		{
			return new ShopVisibleOrdersService( credentials );
		}

		public IShopVisibleProductsService CreateProductsService( ShopVisibleCredentials credentials )
		{
			return new ShopVisibleProductsService( credentials );
		}
	}
}