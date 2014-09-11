using System.Xml.Serialization;

namespace ShopVisibleAccess.Models.Product
{
	[ XmlRoot( ElementName = "ProductOptionGroup" ) ]
	public class ShopVisibleProductOptionGroup
	{
		[ XmlElement( ElementName = "ProductOptions" ) ]
		public ShopVisibleProductOptions ProductOptions{ get; set; }

		public ShopVisibleProductOptionGroup()
		{
			this.ProductOptions = new ShopVisibleProductOptions();
		}
	}
}