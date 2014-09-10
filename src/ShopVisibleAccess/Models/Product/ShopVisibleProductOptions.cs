using System.Collections.Generic;
using System.Xml.Serialization;

namespace ShopVisibleAccess.Models.Product
{
	[ XmlRoot( ElementName = "ProductOptions" ) ]
	public class ShopVisibleProductOptions
	{
		[ XmlElement( ElementName = "ProductOption" ) ]
		public List< ShopVisibleProductOption > ProductOptions{ get; set; }
	}
}