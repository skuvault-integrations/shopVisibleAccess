using System.Xml.Serialization;

namespace ShopVisibleAccess.Models.Order
{
	[ XmlRoot( "Item" ) ]
	public sealed class ShopVisibleOrderItem
	{
		[ XmlElement( ElementName = "ItemQuantity" ) ]
		public int Quantity { get; set; }

		[ XmlElement( ElementName = "ItemProduct" ) ]
		public ShopVisibleOrderItemItemProduct Product { get; set; }

		[ XmlElement( ElementName = "ItemPrice" ) ]
		public decimal Price { get; set; }
	}

	[ XmlRoot( "ItemProduct" ) ]
	public sealed class ShopVisibleOrderItemItemProduct
	{
		[ XmlElement( ElementName = "ProductPartNo" ) ]
		public string Sku { get; set; }
	}
}