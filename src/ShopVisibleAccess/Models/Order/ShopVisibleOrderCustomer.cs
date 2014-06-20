using System.Xml.Serialization;

namespace ShopVisibleAccess.Models.Order
{
	[ XmlRoot( "OrderCustomer" ) ]
	public sealed class ShopVisibleOrderCustomer
	{
		[ XmlElement( ElementName = "CustomerCompany" ) ]
		public string CustomerCompany { get; set; }

		[ XmlElement( ElementName = "CustomerName" ) ]
		public string CustomerName { get; set; }

		[ XmlElement( ElementName = "CustomerEmail" ) ]
		public string CustomerEmail { get; set; }

		[ XmlElement( ElementName = "CustomerPhone" ) ]
		public string CustomerPhone { get; set; }
	}
}