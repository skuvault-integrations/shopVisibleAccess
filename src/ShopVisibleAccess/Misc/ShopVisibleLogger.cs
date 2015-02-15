using Netco.Logging;

namespace ShopVisibleAccess.Misc
{
	public class ShopVisibleLogger
	{
		public static ILogger Log{ get; private set; }

		static ShopVisibleLogger()
		{
			Log = NetcoLogger.GetLogger( "ShopVisibleLogger" );
		}
	}
}