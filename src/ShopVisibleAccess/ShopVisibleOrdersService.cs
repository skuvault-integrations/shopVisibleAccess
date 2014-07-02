using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ShopVisibleAccess.Misc;
using ShopVisibleAccess.Models;
using ShopVisibleAccess.Models.Order;
using ShopVisibleAccess.OrderServices;

namespace ShopVisibleAccess
{
	public sealed class ShopVisibleOrdersService : IShopVisibleOrdersService
	{
		private readonly ShopVisibleCredentials _credentials;
		private readonly OrderServiceSoapClient _client;

		public ShopVisibleOrdersService( ShopVisibleCredentials credentials )
		{
			this._credentials = credentials;
			this._client = new OrderServiceSoapClient();
		}

		public ShopVisibleOrders GetOrders( DateTime startDateUtc, DateTime endDateUtc )
		{
			var orders = new ShopVisibleOrders();
			ActionPolicies.Submit.Do( () =>
			{
				if( this.OrdersByDateRangeNeeded( startDateUtc, endDateUtc ) )
				{
					var xmlnewOrders = this._client.GetOrdersByDateRange( this._credentials.ClientName, this._credentials.Guid, startDateUtc.ToString( CultureInfo.InvariantCulture ), endDateUtc.ToString( CultureInfo.InvariantCulture ), "true" );
					orders = XmlSerializeHelpers.Deserialize< ShopVisibleOrders >( xmlnewOrders.OuterXml );
				}
				else
				{
					var xmlnewOrders = this._client.GetOrdersByDateTimeRange( this._credentials.ClientName, this._credentials.Guid, startDateUtc.ToString( CultureInfo.InvariantCulture ), endDateUtc.ToString( CultureInfo.InvariantCulture ), "true" );
					var xmlmodifiedOrders = this._client.GetChangedOrdersByDateRange( this._credentials.ClientName, this._credentials.Guid, startDateUtc.ToString( CultureInfo.InvariantCulture ), endDateUtc.ToString( CultureInfo.InvariantCulture ), "true" );
					var newOrders = XmlSerializeHelpers.Deserialize< ShopVisibleOrders >( xmlnewOrders.OuterXml );
					var modifiedOrders = XmlSerializeHelpers.Deserialize< ShopVisibleOrders >( xmlmodifiedOrders.OuterXml );

					orders.Orders = newOrders.Orders.Concat( modifiedOrders.Orders ).ToList();
				}
			} );

			return orders;
		}

		public async Task< ShopVisibleOrders > GetOrdersAsync( DateTime startDateUtc, DateTime endDateUtc )
		{
			var orders = new ShopVisibleOrders();
			await ActionPolicies.GetAsync.Do( async () =>
			{
				if( this.OrdersByDateRangeNeeded( startDateUtc, endDateUtc ) )
				{
					var xmlnewOrders = await this._client.GetOrdersByDateRangeAsync( this._credentials.ClientName, this._credentials.Guid, startDateUtc.ToString( CultureInfo.InvariantCulture ), endDateUtc.ToString( CultureInfo.InvariantCulture ), "true" );
					orders = XmlSerializeHelpers.Deserialize< ShopVisibleOrders >( xmlnewOrders.OuterXml );
				}
				else
				{
					var xmlnewOrders = await this._client.GetOrdersByDateTimeRangeAsync( this._credentials.ClientName, this._credentials.Guid, startDateUtc.ToString( CultureInfo.InvariantCulture ), endDateUtc.ToString( CultureInfo.InvariantCulture ), "true" );
					var xmlmodifiedOrders = await this._client.GetChangedOrdersByDateRangeAsync( this._credentials.ClientName, this._credentials.Guid, startDateUtc.ToString( CultureInfo.InvariantCulture ), endDateUtc.ToString( CultureInfo.InvariantCulture ), "true" );
					var newOrders = XmlSerializeHelpers.Deserialize< ShopVisibleOrders >( xmlnewOrders.OuterXml );
					var modifiedOrders = XmlSerializeHelpers.Deserialize< ShopVisibleOrders >( xmlmodifiedOrders.OuterXml );

					orders.Orders = newOrders.Orders.Concat( modifiedOrders.Orders ).ToList();
				}
			} );

			return orders;
		}

		private bool OrdersByDateRangeNeeded( DateTime startDateUtc, DateTime endDateUtc )
		{
			return endDateUtc - startDateUtc > TimeSpan.FromDays( 2 );
		}
	}
}