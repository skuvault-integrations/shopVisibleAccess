using System.Xml.Serialization;

namespace ShopVisibleAccess.Models.Product
{
	[ XmlRoot( "ProductInventory" ) ]
	public class ShopVisibleProduct
	{
		[ XmlElement( ElementName = "ProductPartNo" ) ]
		public string Sku { get; set; }

		[ XmlElement( ElementName = "ProductQuantity" ) ]
		public int Quantity { get; set; }
	}
}