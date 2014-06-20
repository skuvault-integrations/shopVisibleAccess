using System.Collections.Generic;
using System.Xml.Serialization;

namespace ShopVisibleAccess.Models.Order
{
	[ XmlRoot( ElementName = "Orders" ) ]
	public sealed class ShopVisibleOrders
	{
		[ XmlElement( ElementName = "Order" ) ]
		public List< ShopVisibleOrder > Orders { get; set; }
	}
}