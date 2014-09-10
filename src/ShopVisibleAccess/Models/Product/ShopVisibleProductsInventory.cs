using System.Collections.Generic;
using System.Xml.Serialization;

namespace ShopVisibleAccess.Models.Product
{
	[ XmlRoot( ElementName = "ProductsInventory" ) ]
	public sealed class ShopVisibleProductsInventory
	{
		[ XmlElement( ElementName = "ProductInventory" ) ]
		public List< ShopVisibleProductInventory > Products { get; set; }
	}
}