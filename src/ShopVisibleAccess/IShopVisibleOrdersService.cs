using System;
using System.Threading.Tasks;
using ShopVisibleAccess.Models.Order;

namespace ShopVisibleAccess
{
	public interface IShopVisibleOrdersService
	{
		ShopVisibleOrders GetOrders( DateTime startDateUtc, DateTime endDateUtc );
		Task< ShopVisibleOrders > GetOrdersAsync( DateTime startDateUtc, DateTime endDateUtc );
	}
}