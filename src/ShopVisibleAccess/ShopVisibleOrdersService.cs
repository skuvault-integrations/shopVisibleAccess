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
	public sealed class ShopVisibleOrdersService: IShopVisibleOrdersService
	{
		private readonly ShopVisibleCredentials _credentials;
		private readonly OrderServiceSoapClient _client;

		public ShopVisibleOrdersService( ShopVisibleCredentials credentials )
		{
			_credentials = credentials;
			_client = new OrderServiceSoapClient();
		}

		public ShopVisibleOrders GetOrders( DateTime startDateUtc, DateTime endDateUtc )
		{
			var orders = new ShopVisibleOrders();
			ActionPolicies.Submit.Do( () =>
			{
				var currentStartDate = startDateUtc;

				while( currentStartDate < endDateUtc )
				{
					var currentEndDate = currentStartDate.AddHours( 1 );
					if( currentEndDate > endDateUtc )
						currentEndDate = endDateUtc;

					var xmlnewOrders = this._client.GetOrdersByDateTimeRange( this._credentials.ClientName, this._credentials.Guid,
						currentStartDate.ToString( CultureInfo.InvariantCulture ),
						currentEndDate.ToString( CultureInfo.InvariantCulture ),
						"true" );
					var newOrders = XmlSerializeHelpers.Deserialize< ShopVisibleOrders >( xmlnewOrders.OuterXml );

					if( newOrders.Response.ResponseHasErrors && newOrders.Response.ResponseCode != "SUCCESS" )
					{
						throw new Exception(
							string.Format(
								"Sync Orders. Client: {0}, DateRange: ({1};{2}), IterationDateRange: ({3};{4}), ErrorDescription: {5}", this._credentials.ClientName,
								startDateUtc, endDateUtc, currentStartDate, currentEndDate, newOrders.Response.ResponseDescription ) );
					}

					orders.Orders.AddRange( newOrders.Orders );

					currentStartDate = currentEndDate;
				}

				if( this.ConcatModifiedOrders( startDateUtc, endDateUtc ) )
				{
					var xmlmodifiedOrders = this._client.GetChangedOrdersByDateRange( this._credentials.ClientName, this._credentials.Guid,
						startDateUtc.ToString( CultureInfo.InvariantCulture ), endDateUtc.ToString( CultureInfo.InvariantCulture ), "true" );
					var modifiedOrders = XmlSerializeHelpers.Deserialize< ShopVisibleOrders >( xmlmodifiedOrders.OuterXml );

					if( modifiedOrders.Response.ResponseHasErrors && modifiedOrders.Response.ResponseCode != "SUCCESS" )
					{
						throw new Exception( string.Format(
							"Sync Changed Orders. Client: {0}, DateRange: ({1};{2}), ErrorDescription: {3}", this._credentials.ClientName,
							startDateUtc, endDateUtc, modifiedOrders.Response.ResponseDescription ) );
					}

					orders.Orders.AddRange( modifiedOrders.Orders );
				}
			} );

			return orders;
		}

		public async Task< ShopVisibleOrders > GetOrdersAsync( DateTime startDateUtc, DateTime endDateUtc )
		{
			var orders = new ShopVisibleOrders();
			await ActionPolicies.GetAsync.Do( async () =>
			{
				var currentStartDate = startDateUtc;

				while( currentStartDate < endDateUtc )
				{
					var currentEndDate = currentStartDate.AddHours( 1 );
					if( currentEndDate > endDateUtc )
						currentEndDate = endDateUtc;

					var xmlnewOrders = await this._client.GetOrdersByDateTimeRangeAsync( this._credentials.ClientName, this._credentials.Guid,
						currentStartDate.ToString( CultureInfo.InvariantCulture ),
						currentEndDate.ToString( CultureInfo.InvariantCulture ),
						"true" );
					var newOrders = XmlSerializeHelpers.Deserialize< ShopVisibleOrders >( xmlnewOrders.OuterXml );

					if( newOrders.Response.ResponseHasErrors && newOrders.Response.ResponseCode != "SUCCESS" )
					{
						throw new Exception(
							string.Format(
								"Sync Orders. Client: {0}, DateRange: ({1};{2}), IterationDateRange: ({3};{4}), ErrorDescription: {5}", this._credentials.ClientName,
								startDateUtc, endDateUtc, currentStartDate, currentEndDate, newOrders.Response.ResponseDescription ) );
					}

					orders.Orders.AddRange( newOrders.Orders );

					currentStartDate = currentEndDate;
				}

				if( this.ConcatModifiedOrders( startDateUtc, endDateUtc ) )
				{
					var xmlmodifiedOrders =
						await this._client.GetChangedOrdersByDateRangeAsync( this._credentials.ClientName, this._credentials.Guid,
							startDateUtc.ToString( CultureInfo.InvariantCulture ), endDateUtc.ToString( CultureInfo.InvariantCulture ), "true" );
					var modifiedOrders = XmlSerializeHelpers.Deserialize< ShopVisibleOrders >( xmlmodifiedOrders.OuterXml );

					if( modifiedOrders.Response.ResponseHasErrors && modifiedOrders.Response.ResponseCode != "SUCCESS" )
					{
						throw new Exception( string.Format(
							"Sync Changed Orders. Client: {0}, DateRange: ({1};{2}), ErrorDescription: {3}", this._credentials.ClientName,
							startDateUtc, endDateUtc, modifiedOrders.Response.ResponseDescription ) );
					}

					orders.Orders.AddRange( modifiedOrders.Orders );
				}
			} );

			return orders;
		}

		public async Task< ShopVisibleOrders > GetOrdersToExportAdvancedAsync( ProcessingOptions processingOptions, int buyersRemorse = 60, int[] includeSupplierIds = null, string exportType = null )
		{
			var orders = new ShopVisibleOrders();
			await ActionPolicies.GetAsync.Do( async () =>
			{
				var xmlnewOrders = await this._client.GetOrdersToExportAdvancedAsync(
					this._credentials.ClientName,
					this._credentials.Guid,
					processingOptions.ToPipedStrings()
					);

				var newOrders = XmlSerializeHelpers.Deserialize< ShopVisibleOrders >( xmlnewOrders.OuterXml );

				includeSupplierIds = includeSupplierIds ?? new int[0];
				exportType = exportType ?? string.Empty;

				if( newOrders.Response.ResponseHasErrors && newOrders.Response.ResponseCode != "SUCCESS" )
				{
					throw new Exception(
						string.Format(
							"Sync Orders. Client: {0}, DateRange: ({1};{2}), IterationDateRange: ({3};{4}), ErrorDescription: {5}", this._credentials.ClientName,
							processingOptions.ToPipedStrings(), buyersRemorse, includeSupplierIds, exportType, newOrders.Response.ResponseDescription));
				}

				orders.Orders.AddRange( newOrders.Orders );

			} );

			return orders;
		}

		private bool ConcatModifiedOrders( DateTime startDateUtc, DateTime endDateUtc )
		{
			return endDateUtc - startDateUtc < TimeSpan.FromDays( 2 );
		}
	}

	[ Flags ]
	public enum ProcessingOptions
	{
		ExportType = 0x1,
		BuyersRemorseMinutes = 0x2,
		IncludeSupplierIds = 0x4,
		ExcludeSupplierIds = 0x8,
		OrdersToReturn = 0x10,
		ReturnOrderAddressesOnly = 0x20,
		IncludeCustomerTokens = 0x40,
		OrderStatusOverride = 0x80,
		ItemStatusOverride = 0x100
	}

	public static class Extensions
	{
		public static string ToPipedStrings( this ProcessingOptions source )
		{
			var res = ( from object processingOption in Enum.GetValues( typeof( ProcessingOptions ) )
				where ( source & ( ProcessingOptions )processingOption ) != 0
				select source.ToString()
				).ToList();

			return string.Join( "|", res );
		}
	}
}