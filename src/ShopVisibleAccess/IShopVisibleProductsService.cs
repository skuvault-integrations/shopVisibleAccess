using System.Threading.Tasks;
using ShopVisibleAccess.Models.Product;

namespace ShopVisibleAccess
{
	public interface IShopVisibleProductsService
	{
		void UpdateProducts( ShopVisibleProducts products );
		Task UpdateProductsAsync(ShopVisibleProducts products);
	}
}