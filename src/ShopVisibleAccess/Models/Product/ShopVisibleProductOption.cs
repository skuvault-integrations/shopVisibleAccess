using System.Xml.Serialization;

namespace ShopVisibleAccess.Models.Product
{
	[ XmlRoot( ElementName = "ProductOption" ) ]
	public class ShopVisibleProductOption
	{
		[ XmlElement( ElementName = "ProductOptionPrice" ) ]
		public string Price{ get; set; }

		[ XmlElement( ElementName = "ProductOptionPartNo" ) ]
		public string Sku{ get; set; }

		[ XmlElement( ElementName = "ProductOptionQuantity" ) ]
		public int Quantity{ get; set; }

		[ XmlElement( ElementName = "ProductOptionUPC" ) ]
		public string Upc{ get; set; }
	}
}