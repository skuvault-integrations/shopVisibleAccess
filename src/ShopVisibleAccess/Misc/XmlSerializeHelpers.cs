using System.IO;
using System.Xml.Serialization;

namespace ShopVisibleAccess.Misc
{
	public static class XmlSerializeHelpers
	{
		public static T Deserialize< T >( string xml )
		{
			if( string.IsNullOrWhiteSpace( xml ) )
				return default( T );

			var serializer = new XmlSerializer( typeof( T ) );
			return ( T )serializer.Deserialize( new StringReader( xml ) );
		}

		public static string Serialize< T >( T obj )
		{
			var serializer = new XmlSerializer( typeof( T ) );
			using( var writer = new StringWriter() )
			{
				serializer.Serialize( writer, obj );
				return writer.ToString();
			}
		}
	}
}