using System.Collections.Generic;
using System.Xml.Serialization;

namespace ShopVisibleAccess.Models.Product
{
	[ XmlRoot( ElementName = "ProductOptionGroups" ) ]
	public class ShopVisibleProductOptionGroups
	{
		[ XmlElement( ElementName = "ProductOptionGroup" ) ]
		public List< ShopVisibleProductOptionGroup > Groups{ get; set; }

		public ShopVisibleProductOptionGroups()
		{
			this.Groups = new List< ShopVisibleProductOptionGroup >();
		}
	}
}