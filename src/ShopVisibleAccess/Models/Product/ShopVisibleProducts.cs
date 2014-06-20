using System.Collections.Generic;
using System.Xml.Serialization;

namespace ShopVisibleAccess.Models.Product
{
	[ XmlRoot( ElementName = "ProductsInventory" ) ]
	public sealed class ShopVisibleProducts
	{
		[ XmlElement( ElementName = "ProductInventory" ) ]
		public List< ShopVisibleProduct > Products { get; set; }
	}
}