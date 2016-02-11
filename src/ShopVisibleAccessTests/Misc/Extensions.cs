using FluentAssertions;
using NUnit.Framework;
using ShopVisibleAccess;
using ShopVisibleAccess.Misc;

namespace ShopVisibleAccessTests.Misc
{
	[ TestFixture ]
	public class Extensions
	{
		[ Test ]
		public void ToPipedStrings_EnumPassed_CorrectStringReturned()
		{
			//A
			const ProcessingOptions processingOptions = ProcessingOptions.ExportType | ProcessingOptions.BuyersRemorseMinutes | ProcessingOptions.IncludeSupplierIds | ProcessingOptions.ReturnOrderAddressesOnly | ProcessingOptions.IncludeCustomerTokens | ProcessingOptions.OrdersToReturn;
			const AvailableExportTypes availableExportTypes = AvailableExportTypes.Customer;
			const int buyersRemorse = 60;
			var includeSupplierIds = new[] { 1, 2, 3 };
			const bool returnAddressesOnly = true;
			const bool includeCustomerTokens = false;
			const int ordersToReturn = 100;

			//A
			var pipedStrings = processingOptions.ToPipedStrings( availableExportTypes, buyersRemorse, includeSupplierIds, returnAddressesOnly, includeCustomerTokens, ordersToReturn );

			//A
			//pipedStrings.Should().Be( "exporttype=Customer|buyersremorseminutes=60|IncludeSupplierIDs=1,2,3|returnorderaddressesonly=true|includecustomertokens=false|OrdersToReturn=100".ToLowerInvariant() );
			pipedStrings.Should().Be( "exporttype=Customer|buyersremorseminutes=60|IncludeSupplierIDs=1,2,3|OrdersToReturn=100|returnorderaddressesonly=true|includecustomertokens=false".ToLowerInvariant() );
		}
	}
}