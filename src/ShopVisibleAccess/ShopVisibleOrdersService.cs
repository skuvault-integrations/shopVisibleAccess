using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Xml;
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
			XmlNode orders = new XmlDocument();
			ActionPolicies.Submit.Do( () =>
			{
				if( this.OrdersByDateRangeNeeded( startDateUtc, endDateUtc ) )
					orders = this._client.GetOrdersByDateRange( this._credentials.ClientName, this._credentials.Guid, startDateUtc.ToString( CultureInfo.InvariantCulture ), endDateUtc.ToString( CultureInfo.InvariantCulture ), "true" );
				else
					orders = this._client.GetOrdersByDateTimeRange( this._credentials.ClientName, this._credentials.Guid, startDateUtc.ToString( "G" ), endDateUtc.ToString( "G" ), "true" );
			} );

			return XmlSerializeHelpers.Deserialize< ShopVisibleOrders >( orders.OuterXml );
		}

		public async Task< ShopVisibleOrders > GetOrdersAsync( DateTime startDateUtc, DateTime endDateUtc )
		{
			XmlNode orders = new XmlDocument();
			await ActionPolicies.GetAsync.Do( async () =>
			{
				if( this.OrdersByDateRangeNeeded( startDateUtc, endDateUtc ) )
					orders = await this._client.GetOrdersByDateRangeAsync( this._credentials.ClientName, this._credentials.Guid, startDateUtc.ToString( CultureInfo.InvariantCulture ), endDateUtc.ToString( CultureInfo.InvariantCulture ), "true" );
				else
					orders = await this._client.GetOrdersByDateTimeRangeAsync( this._credentials.ClientName, this._credentials.Guid, startDateUtc.ToString( CultureInfo.InvariantCulture ), endDateUtc.ToString( CultureInfo.InvariantCulture ), "true" );
			} );

			return XmlSerializeHelpers.Deserialize< ShopVisibleOrders >( orders.OuterXml );
		}

		private bool OrdersByDateRangeNeeded( DateTime startDateUtc, DateTime endDateUtc )
		{
			return endDateUtc - startDateUtc > TimeSpan.FromHours( 1 );
		}
	}
}