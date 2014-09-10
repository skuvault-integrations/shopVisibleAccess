using System.Xml.Serialization;

namespace ShopVisibleAccess.Models.Product
{
	[ XmlRoot( "ProductInventory" ) ]
	public class ShopVisibleProductInventory
	{
		[ XmlElement( ElementName = "ProductPartNo" ) ]
		public string Sku{ get; set; }

		[ XmlElement( ElementName = "ProductQuantity" ) ]
		public int Quantity{ get; set; }

		[ XmlElement( ElementName = "ProductOptionGroups" ) ]
		public ShopVisibleProductOptionGroups OptionGroups{ get; set; }
	}
}