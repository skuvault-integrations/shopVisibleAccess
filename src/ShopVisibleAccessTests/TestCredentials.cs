using LINQtoCSV;

namespace ShopVisibleAccessTests
{
	internal class TestConfig
	{
		[ CsvColumn( Name = "ClientName", FieldIndex = 1 ) ]
		public string ClientName { get; set; }

		[ CsvColumn( Name = "Guid", FieldIndex = 2 ) ]
		public string Guid { get; set; }
	}
}