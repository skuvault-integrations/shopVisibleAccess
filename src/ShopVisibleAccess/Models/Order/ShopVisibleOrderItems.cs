using System.Collections.Generic;
using System.Xml.Serialization;

namespace ShopVisibleAccess.Models.Order
{
	[ XmlRoot( "OrderItems " ) ]
	public sealed class ShopVisibleOrderItems
	{
		[ XmlElement( ElementName = "Item" ) ]
		public List< ShopVisibleOrderItem > Items { get; set; }
	}
}