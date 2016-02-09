using System;
using System.Linq;

namespace ShopVisibleAccess.Misc
{
	public static class Extensions
	{
		public static string ToPipedStrings( this ProcessingOptions source, AvailableExportTypes exportType, int buyersRemorse, int[] includeSupplierIds )
		{
			Func< ProcessingOptions, string > serialiserWithParameters = x =>
			{
				var result = string.Empty;
				var parameters = string.Empty;

				switch( source )
				{
					case ProcessingOptions.BuyersRemorseMinutes:
						parameters = "=" + buyersRemorse;
						break;
					case ProcessingOptions.ExportType:
						parameters = "=" + exportType;
						break;
					case ProcessingOptions.IncludeSupplierIds:
						parameters = includeSupplierIds != null && includeSupplierIds.Length > 0 ? "=" + string.Join( ",", includeSupplierIds.Select( y => y.ToString() ) ) : string.Empty;
						break;
				}

				return source.ToString() + parameters;
			};

			includeSupplierIds = includeSupplierIds ?? new int[ 0 ];

			var res = ( from object processingOption in Enum.GetValues( typeof( ProcessingOptions ) )
				where ( source & ( ProcessingOptions )processingOption ) != 0
				select serialiserWithParameters( source )
				).ToList();

			return string.Join( "|", res );
		}
	}
}