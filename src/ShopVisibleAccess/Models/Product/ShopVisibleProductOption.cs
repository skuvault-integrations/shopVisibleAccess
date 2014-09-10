using System.Xml.Serialization;

namespace ShopVisibleAccess.Models.Product
{
	[ XmlRoot( ElementName = "ProductOption" ) ]
	public class ShopVisibleProductOption
	{
		[ XmlElement( ElementName = "ProductOptionPartNo" ) ]
		public string Sku{ get; set; }

		[ XmlElement( ElementName = "ProductOptionQuantity" ) ]
		public int Quantity{ get; set; }
	}
}