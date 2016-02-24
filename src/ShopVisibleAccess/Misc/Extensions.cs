using System;
using System.Linq;

namespace ShopVisibleAccess.Misc
{
	public static class Extensions
	{
		public static string ToPipedStrings( this ProcessingOptions source, AvailableExportTypes exportType, int buyersRemorse, int[] includeSupplierIds, bool returnAddressesOnly, bool includeCustomerTokens, int ordersToReturn, int[] orderStatusOverride, int[] itemStatusOverride )
		{
			Func< ProcessingOptions, string > serialiserWithParameters = x =>
			{
				var parameters = string.Empty;

				switch( x )
				{
					case ProcessingOptions.BuyersRemorseMinutes:
						parameters = "=" + buyersRemorse;
						break;
					case ProcessingOptions.ExportType:
						parameters = "=" + exportType;
						break;
					case ProcessingOptions.IncludeSupplierIds:
						parameters = ConvertToCommaSeparatedString( includeSupplierIds );
						break;
					case ProcessingOptions.OrderStatusOverride:
						parameters = ConvertToCommaSeparatedString( orderStatusOverride );
						break;
					case ProcessingOptions.ItemStatusOverride:
						parameters = ConvertToCommaSeparatedString( itemStatusOverride );
						break;
					case ProcessingOptions.ReturnOrderAddressesOnly:
						parameters = "=" + returnAddressesOnly.ToString();
						break;
					case ProcessingOptions.IncludeCustomerTokens:
						parameters = "=" + includeCustomerTokens.ToString();
						break;
					case ProcessingOptions.OrdersToReturn:
						parameters = "=" + ordersToReturn.ToString();
						break;
				}

				return x.ToString() + parameters;
			};

			var values = Enum.GetValues( typeof( ProcessingOptions ) );

			var res = ( from object processingOption in values
				let e = source & ( ProcessingOptions )processingOption
				where e != 0
				select serialiserWithParameters( e )
				).ToList();

			return string.Join( "|", res ).ToLowerInvariant();
		}

		private static string ConvertToCommaSeparatedString( int[] orderStatusOverride )
		{
			return orderStatusOverride != null && orderStatusOverride.Length > 0 ? "=" + string.Join( ",", orderStatusOverride.Select( y => y.ToString() ) ) : string.Empty;
		}
	}
}