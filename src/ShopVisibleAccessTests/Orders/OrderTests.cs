using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using NUnit.Framework;
using ShopVisibleAccess;
using ShopVisibleAccess.Models;

namespace ShopVisibleAccessTests.Orders
{
	public class OrderTests
	{
		private ShopVisibleCredentials _credentials;
		private ShopVisibleFactory _factory;

		[ SetUp ]
		public void Init()
		{
			const string credentialsFilePath = @"..\..\Files\ShopVisibleCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< TestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true } ).FirstOrDefault();

			if( testConfig != null )
				this._credentials = new ShopVisibleCredentials( testConfig.ClientName, testConfig.Guid );

			this._factory = new ShopVisibleFactory();
		}

		[ Test ]
		public void GetOrdersByDateRange()
		{
			var service = this._factory.CreateOrdersService( this._credentials );
			var orders = service.GetOrders( DateTime.UtcNow.AddDays( -1 ), DateTime.UtcNow );

			orders.Orders.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersByDateRangeAsync()
		{
			var service = this._factory.CreateOrdersService( this._credentials );
			var orders = await service.GetOrdersAsync( DateTime.UtcNow.AddDays( -5 ), DateTime.UtcNow );

			orders.Orders.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetOrdersByDateTimeRange()
		{
			var service = this._factory.CreateOrdersService( this._credentials );
			var orders = service.GetOrders( DateTime.UtcNow - TimeSpan.FromMinutes( 59 ), DateTime.UtcNow );

			orders.Orders.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersByDateTimeRangeAsync()
		{
			var service = this._factory.CreateOrdersService( this._credentials );
			var orders = await service.GetOrdersAsync( DateTime.UtcNow - TimeSpan.FromMinutes( 55 ), DateTime.UtcNow );

			orders.Orders.Count.Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersToExportAdvancedAsync()
		{
			var service = this._factory.CreateOrdersService( this._credentials );
			var orders = await service.GetOrdersToExportAdvancedAsync(
				ProcessingOptions.ExportType
				| ProcessingOptions.BuyersRemorseMinutes
				| ProcessingOptions.IncludeSupplierIds
				| ProcessingOptions.ReturnOrderAddressesOnly
				| ProcessingOptions.IncludeCustomerTokens
				| ProcessingOptions.OrdersToReturn
				| ProcessingOptions.ItemStatusOverride
				| ProcessingOptions.OrderStatusOverride
				,
				AvailableExportTypes.Customer, true, false, 100, new[] { 4, 8, 12, 14, 15, 20, 21, 31, 34 }, new[] { 4, 18, 23, 30 }, new[] { 1273, 1307, 1308 }, 60 );

			orders.Orders.Count.Should().BeGreaterThan( 0 );
		}
	}
}