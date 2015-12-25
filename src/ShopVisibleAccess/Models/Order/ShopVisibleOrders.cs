using System.Collections.Generic;
using System.Xml.Serialization;

namespace ShopVisibleAccess.Models.Order
{
	[ XmlRoot( ElementName = "Orders" ) ]
	public sealed class ShopVisibleOrders
	{
		[ XmlElement( ElementName = "Order" ) ]
		public List< ShopVisibleOrder > Orders { get; set; }

		[XmlElement(ElementName = "Response")]
		public ShopVisibleResponse Response { get; set; }

		public ShopVisibleOrders()
		{
			this.Orders = new List< ShopVisibleOrder >();
		}
	}

	public class ShopVisibleResponse
	{
		public bool ResponseHasErrors { get; set; }
		public string ResponseCode { get; set; }
		public string ResponseDescription { get; set; }
	}
}