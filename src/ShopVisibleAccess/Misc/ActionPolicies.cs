using System;
using System.Threading.Tasks;
using Netco.ActionPolicyServices;
using Netco.Utils;

namespace ShopVisibleAccess.Misc
{
	public static class ActionPolicies
	{
		public static ActionPolicy Submit
		{
			get { return _shopVisibleSumbitPolicy; }
		}

		private static readonly ActionPolicy _shopVisibleSumbitPolicy = ActionPolicy.Handle< Exception >().Retry( 10, ( ex, i ) =>
		{
			ShopVisibleLogger.Log.Trace( ex, "Retrying ShopVisible API submit call for the {0} time", i );
			SystemUtil.Sleep( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicyAsync SubmitAsync
		{
			get { return _shopVisibleSumbitAsyncPolicy; }
		}

		private static readonly ActionPolicyAsync _shopVisibleSumbitAsyncPolicy = ActionPolicyAsync.Handle< Exception >().RetryAsync( 10, async ( ex, i ) =>
		{
			ShopVisibleLogger.Log.Trace( ex, "Retrying ShopVisible API submit call for the {0} time", i );
			await Task.Delay( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicy Get
		{
			get { return _shopVisibleGetPolicy; }
		}

		private static readonly ActionPolicy _shopVisibleGetPolicy = ActionPolicy.Handle< Exception >().Retry( 10, ( ex, i ) =>
		{
			ShopVisibleLogger.Log.Trace( ex, "Retrying ShopVisible API get call for the {0} time", i );
			SystemUtil.Sleep( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicyAsync GetAsync
		{
			get { return _shopVisibleGetAsyncPolicy; }
		}

		private static readonly ActionPolicyAsync _shopVisibleGetAsyncPolicy = ActionPolicyAsync.Handle< Exception >().RetryAsync( 10, async ( ex, i ) =>
		{
			ShopVisibleLogger.Log.Trace( ex, "Retrying ShopVisible API get call for the {0} time", i );
			await Task.Delay( TimeSpan.FromSeconds( 0.5 + i ) );
		} );
	}
}